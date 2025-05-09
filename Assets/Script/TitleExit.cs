using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleExit : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        // Unity�G�f�B�^�[�ł̓���
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���ۂ̃Q�[���I������
        Application.Quit();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
}
