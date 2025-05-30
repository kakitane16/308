//using static UnityEngine.GraphicsBuffer;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(StageGenerator))]
//public class StageGeneratorEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        // デフォルトのインスペクター表示
//        DrawDefaultInspector();

//        StageGenerator generator = (StageGenerator)target;

//        EditorGUILayout.Space();
//        EditorGUILayout.LabelField("エディタツール", EditorStyles.boldLabel);

//        if (GUILayout.Button("▶ ステージ再生成"))
//        {
//            generator.RegenerateFromContextMenu();
//        }
//    }
//}