using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WasabiSPawner : MonoBehaviour
{
    //Inspector の該当フィールドの上に "吐き出すワサビのプレハブ" という見出し（見やすいラベル）を表示
    [Header("吐き出すワサビのプレハブ")]
    public GameObject wasabiPrefab;
    //Inspector上に "ワサビが出る位置" という見出しを表示
    [Header("ワサビが出る位置")]
    //Inspector に「ドラッグ＆ドロップ可能なスロット」が現れるので、
    //そこに「吐き出し位置を示す空の GameObject」をアサイン
    public Transform spawnPoint;
    public Vector3 offset = new Vector3();
    public Vector3 offset_rotate = new Vector3();

    [Header("ワサビが落下してから消えるまでの時間 (秒)")]
    //「ワサビが生まれてから自動で消えるまでの秒数」を表す
    //inspectorで秒数変更可能
    public float lifeTime = 3.0f;

    private float delayTime = 1.0f;
    float Timer;

    private void Start()
    {
        // 場に出ている全ワサビを取得
        WasabiSPawner[] wasabies = FindObjectsOfType<WasabiSPawner>();
        // X座標でソート
        var sort = wasabies.OrderBy(obj => obj.transform.position.x).ToArray();
        // 自身は何番目か
        int index = System.Array.IndexOf(sort, this);
        // 番数に応じて起動時間変更
        float delay = index * delayTime;
        // ループ開始
        StartCoroutine(SpawnLoop(delay));
    }

    private void Update()
    {
        
    }

    private IEnumerator SpawnLoop(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            // ログ：何秒時点で吐き出し始めるか
            Debug.Log($"[WasabiSpawner] SpawnLoop: Spawning wasabi at time {Time.time:F2}");
            //ワサビを吐き出し

            Quaternion rot = Quaternion.Euler(spawnPoint.rotation.eulerAngles + offset_rotate);
            //Instantiate → ワサビ生成
            GameObject w = Instantiate(wasabiPrefab
                                       , spawnPoint.position + offset
                                       , rot);
            // ログ：生成されたインスタンス名
            Debug.Log($"[WasabiSpawner] Spawned: {w.name}");
            //lifeTime数秒に自動で消す
            Destroy(w, lifeTime);

            //指定時間だけを一時停止、次を同じ間隔で繰り返し
            yield return new WaitForSeconds(lifeTime);
        }
    }
}
