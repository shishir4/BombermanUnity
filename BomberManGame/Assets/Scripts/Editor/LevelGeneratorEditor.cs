using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor {

    public override void OnInspectorGUI ()
    {
       
//        base.OnInspectorGUI ();
        DrawDefaultInspector ();
        LevelGenerator myScript = (LevelGenerator)target;
        if(GUILayout.Button("Create Level")){
            myScript.CreateLevel ();
        }

//        public void override
    }
}

