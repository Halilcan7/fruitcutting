using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FastMesh_Example
{
    [ExecuteInEditMode]
    public class SceneViewText : MonoBehaviour
    {
        public bool isShow = true;
        string text2 = "These 3D models, all created with \"Fast Mesh - 3D Asset Creation Tool\" (click)"; 
        Color backgroundColor = Color.white;
        Color textColor = Color.black; 

        private void OnEnable()
        {
            // Eğer kod editörde çalışıyorsa, SceneView işlevselliğini etkinleştir
#if UNITY_EDITOR
            SceneView.duringSceneGui += OnSceneGUI;
#endif
        }

        private void OnDisable()
        {
            // Eğer kod editörde çalışıyorsa, SceneView işlevselliğini devre dışı bırak
#if UNITY_EDITOR
            SceneView.duringSceneGui -= OnSceneGUI;
#endif
        }

#if UNITY_EDITOR
        private void OnSceneGUI(SceneView sceneView)
        {
            if (isShow == false) return;

            Handles.BeginGUI();
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 20;
            style.normal.textColor = textColor;
            style.wordWrap = true;

            float width = 420f;
            float height = 50f;
            float x = (sceneView.position.width - width) / 2f;
            // float y = (sceneView.position.height - height) / 2f;
            float y = 10f;

            GUI.color = backgroundColor;
            GUI.DrawTexture(new Rect(x - 10, y - 10, width + 20, height + 20), Texture2D.whiteTexture);
            GUI.color = Color.white;

            if (GUI.Button(new Rect(x, y, width, height), text2, style))
            {
                Application.OpenURL("https://assetstore.unity.com/packages/slug/288711");
            }

            Handles.EndGUI();
        }
#endif
    }
}