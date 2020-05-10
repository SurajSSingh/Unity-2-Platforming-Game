using UnityEngine;
using System.Collections;
using UnityEditor;

namespace NinjaController {
  [CustomEditor(typeof(PhysicsParamsContainer))]
  public class PhysicsParamsContainerEditor : Editor {

    private bool importExportFoldout = false;

    public override void OnInspectorGUI() {

      var physicsParamsContainer = target as PhysicsParamsContainer;

      DrawDefaultInspector();

      importExportFoldout = EditorGUILayout.Foldout(importExportFoldout, "Import/Export Physics Params");

      if(importExportFoldout == true) {
        string jsonString = JsonUtility.ToJson(physicsParamsContainer.physicsParams);
        jsonString = EditorGUILayout.TextField("Physics Params Json", jsonString);

        try {
          var physicsParams = JsonUtility.FromJson<PhysicsParams>(jsonString);
          physicsParamsContainer.physicsParams = physicsParams;
        } catch(System.Exception e) {
          Debug.LogError(e.Message);
        }
      }
    }
  }
}
