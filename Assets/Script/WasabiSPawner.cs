using System.Collections;
using System.Collections.Generic;
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

    [Header("ワサビが落下してから消えるまでの時間 (秒)")]
    //「ワサビが生まれてから自動で消えるまでの秒数」を表す
    //inspectorで秒数変更可能
    public float lifeTime = 3f;

    private void Start()
    {
        //起動と同時にループを開始
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            //ワサビを吐き出し
            //Instantiate → ワサビ生成
            GameObject w = Instantiate(wasabiPrefab
                                       , spawnPoint.position + offset
                                       , spawnPoint.rotation);
            //lifeTime数秒に自動で消す
            Destroy(w, lifeTime);

            //指定時間だけを一時停止、次を同じ間隔で繰り返し
            yield return new WaitForSeconds(lifeTime);
        }
    }
}
