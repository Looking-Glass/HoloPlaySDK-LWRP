using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LookingGlass {
    public class InstancingRevisedTest : MonoBehaviour {
        public List<GameObject> renderObjs;
        public bool verbose;
        ComputeBuffer localToWorldBuffer;
        ComputeBuffer worldToLocalBuffer;
        ComputeBuffer argsBuffer;
        uint[] args = new uint[5] { 0, 0, 0, 0, 0 };
        int numViews { get{ return 45; } } // replace with Holoplay.numViews or the like

        void Start() {
            argsBuffer = new ComputeBuffer(
                1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        }

        void LateUpdate() {
            foreach (var obj in renderObjs) {
                var success = DrawObjInstanced(obj);
                if (verbose)
                    Debug.Log(obj.name + " success: " + success);
            }
        }

        bool DrawObjInstanced(GameObject obj) {
            // some checks before proceeding
            var objMR = obj.GetComponent<MeshRenderer>();
            if (objMR == null || objMR.material == null) {
                if (verbose)
                    Debug.Log(obj.name + ": no mesh renderer");
                return false;
            }
            var objMF = obj.GetComponent<MeshFilter>();
            if (objMF == null || objMF.sharedMesh == null) {
                if (verbose)
                    Debug.Log(obj.name + ": no mesh filter");
                return false;
            }
            // another check to see if the material exists in _holoplay form
            Shader instancedShader = Shader.Find(objMR.material.shader.name + "_holoplay");
            if (instancedShader == null) {
                if (verbose)
                    Debug.Log(obj.name + ": no instanced shader");
                return false;
            }
            Material instancedMat = new Material(objMR.material) { shader = instancedShader };
            // meh why not
            int submeshIndex = 0;
            // get the transforms and put them in the buffer
            if (localToWorldBuffer != null) localToWorldBuffer.Release();
            localToWorldBuffer = new ComputeBuffer(numViews, 64 /*size of matrix4x4?*/);
            if (worldToLocalBuffer != null) worldToLocalBuffer.Release();
            worldToLocalBuffer = new ComputeBuffer(numViews, 64 /*size of matrix4x4?*/);
            Matrix4x4[] localToWorldMatrices = new Matrix4x4[numViews];
            Matrix4x4[] worldToLocalMatrices = new Matrix4x4[numViews];
            for (int i = 0; i < numViews; i++) {
                localToWorldMatrices[i] = objMR.localToWorldMatrix;
                worldToLocalMatrices[i] = objMR.worldToLocalMatrix;
                // Debug.Log(localToWorldMatrices[i]);
                // transformMatrices[i].m00 += 0.1f;
                // transformMatrices[i].m00 += i * 1f; // no idea what this relates to
            }
            localToWorldBuffer.SetData(localToWorldMatrices);
            worldToLocalBuffer.SetData(worldToLocalMatrices);
            instancedMat.SetBuffer("localToWorldBuffer", localToWorldBuffer); // important: pass these to the material
            instancedMat.SetBuffer("worldToLocalBuffer", worldToLocalBuffer); // important: pass these to the material
            instancedMat.enableInstancing = true;
            // instancedMat.EnableKeyword("UNITY_INSTANCING_ENABLED");
            // indirect args
            args[0] = (uint)objMF.sharedMesh.GetIndexCount(submeshIndex);
            args[1] = (uint)numViews;
            args[2] = (uint)objMF.sharedMesh.GetIndexStart(submeshIndex);
            args[3] = (uint)objMF.sharedMesh.GetBaseVertex(submeshIndex);
            argsBuffer.SetData(args);
            // draw that
            Graphics.DrawMeshInstancedIndirect(
                objMF.sharedMesh, submeshIndex, instancedMat,
                new Bounds(Vector3.zero, new Vector3(100.0f, 100.0f, 100.0f)), // what the fuck are these bounds honestly
                argsBuffer, 0, null as MaterialPropertyBlock, 
                UnityEngine.Rendering.ShadowCastingMode.On, true);
            return true;
        }

        void OnDisable() {
            if (localToWorldBuffer != null) localToWorldBuffer.Release();
            localToWorldBuffer = null;
            if (argsBuffer != null) argsBuffer.Release();            
            argsBuffer = null;
        }
    }
}