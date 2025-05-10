using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneLoader : MonoBehaviour
{
    [System.Serializable]
    public class DebugSceneBinding
    {
        public KeyCode key;
        public string sceneName;
    }

    [Header("シーン切り替えキーと対応シーン名")]
    public DebugSceneBinding[] debugScenes;

    public static GameObject DebugController { get; private set; }
    void Awake()
    {
        if (DebugController == null)
        {
            DebugController = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        foreach (var binding in debugScenes)
        {
            if (Input.GetKeyDown(binding.key))
            {
                Debug.Log($"[DebugController] キー {binding.key} が押されました。シーン {binding.sceneName} に遷移します。");
                SceneManager.LoadScene(binding.sceneName);
            }
        }
    }
}
