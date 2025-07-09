using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputObject inputDevice = InputObject.GamePad; //初期はなにも接続されてない
    public int score = 0; //点数を返す
    public int stageIndex = 0;
    public int clearstate = 0;
    public int MaxPage = 15;

    public string SelectedStageName = "stage001"; // ステージ名（例: stage001）
    
    //10が乗るだけ 11がワサビ付き 12が炙り　13が炙りワサビ付き
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
    [System.Serializable]
    private class SaveData
    {
        public int clearstate;
    }

    public void SaveClearState()
    {
        SaveData data = new SaveData();
        data.clearstate = this.clearstate;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/clearstate.json";
        File.WriteAllText(path, json);

        Debug.Log("clearstate saved: " + data.clearstate + " at " + path);
    }

    public void LoadClearState()
    {
        string path = Application.persistentDataPath + "/clearstate.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            this.clearstate = data.clearstate;

            Debug.Log("clearstate loaded: " + this.clearstate);
        }
        else
        {
            Debug.Log("No clearstate save file found.");
        }
    }
}
