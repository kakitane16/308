using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputObject inputDevice = InputObject.GamePad; //初期はなにも接続されてない
    public int score = 0; //点数を返す
    public string SelectedStageName = "stage001"; // ステージ名（例: stage001）
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
