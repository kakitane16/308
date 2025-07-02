using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputObject inputDevice = InputObject.GamePad; //�����͂Ȃɂ��ڑ�����ĂȂ�
    public int score = 0; //�_����Ԃ�
    public string SelectedStageName = "stage001"; // �X�e�[�W���i��: stage001�j
    public int Points = 10;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
