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

    [Header("�V�[���؂�ւ��L�[�ƑΉ��V�[����")]
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
                Debug.Log($"[DebugController] �L�[ {binding.key} ��������܂����B�V�[�� {binding.sceneName} �ɑJ�ڂ��܂��B");
                SceneManager.LoadScene(binding.sceneName);
            }
        }
    }
}
