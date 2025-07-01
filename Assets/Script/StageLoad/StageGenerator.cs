#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif
using UnityEngine;

//[ExecuteInEditMode]
public class StageGenerator : MonoBehaviour
{
    [Header("CSVファイル")]
    public TextAsset csvFile;

    [Header("Prefab設定")]
    public GameObject startPrefab;
    public GameObject goalPrefab;
    public GameObject[] gimmickPrefabs;

    [Header("配置調整")]
    public Vector3 cellSize = new Vector3(1, 1, 0);
    public Vector3 positionOffset = Vector3.zero;
    public Vector3 centerTarget = new Vector3(11f, 5f, 0);

    private int[,] cachedData;
    private Transform stageContainer;

    void Start()
    {
        ClearStage();

        string stageName = GameManager.Instance?.SelectedStageName ?? "stage001";
        csvFile = Resources.Load<TextAsset>($"CSV/{stageName}");

        if (csvFile != null)
        {
            GenerateFromCSV();
        }
        else
        {
            Debug.LogWarning($"CSVファイル '{stageName}' が見つかりませんでした");
        }
    }

    //[ContextMenu("ステージ再生成")]
    //public void RegenerateFromContextMenu()
    //{
    //    if (csvFile != null)
    //    {
    //        cachedData = ParseCSV(csvFile);
    //        ClearStage();
    //        GenerateStage();
    //    }
    //    else
    //    {
    //        Debug.LogWarning("CSVファイルが指定されていません");
    //    }
    //}

    public void GenerateFromCSV()
    {
        if (csvFile != null)
        {
            cachedData = ParseCSV(csvFile);
            GenerateStage();
        }
        else
        {
            Debug.LogWarning("CSVファイルが指定されていません");
        }
    }

    private void ClearStage()
    {
#if UNITY_EDITOR
        // transform の子から "StageRoot" を名前で全削除（確実）
        var children = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.name == "StageRoot")
            {
                children.Add(child);
            }
        }

        foreach (var child in children)
        {
            if (Application.isPlaying)
                Destroy(child.gameObject);
            else
                DestroyImmediate(child.gameObject);
        }
#endif
    }

    void GenerateStage()
    {
        stageContainer = new GameObject("StageRoot").transform;
        stageContainer.SetParent(this.transform); // 子として管理

        int height = cachedData.GetLength(0);
        int width = cachedData.GetLength(1);

        float mapWidth = width * cellSize.x;
        float mapHeight = height * cellSize.y;
        Vector3 currentCenter = new Vector3(mapWidth / 2f, mapHeight / 2f, 0);
        Vector3 finalOffset = (centerTarget - currentCenter) + positionOffset;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int cell = cachedData[y, x];
                Vector3 pos = new Vector3(x * cellSize.x, y * cellSize.y, 0) + finalOffset;

                GameObject obj = null;
                switch (cell)
                {
                    case 1:
                        obj = Instantiate(startPrefab, pos, Quaternion.identity, stageContainer);
                        break;
                    case 2:
                        obj = Instantiate(goalPrefab, pos, Quaternion.identity, stageContainer);
                        break;
                    default:
                        if (cell >= 3 && (cell - 3) < gimmickPrefabs.Length)
                        {
                            obj = Instantiate(gimmickPrefabs[cell - 3], pos, Quaternion.identity, stageContainer);
                        }
                        break;
                }
            }
        }
    }

    int[,] ParseCSV(TextAsset csv)
    {
        string[] lines = csv.text.Trim().Split('\n');
        int height = lines.Length;
        int width = lines[0].Trim().Split(',').Length;

        int[,] data = new int[height, width];
        for (int y = 0; y < height; y++)
        {
            string[] values = lines[y].Trim().Split(',');
            for (int x = 0; x < width; x++)
            {
                int.TryParse(values[x], out data[y, x]);
            }
        }
        return data;
    }
}
