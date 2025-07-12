using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Buner : MonoBehaviour
{
    public GameObject G_Target; // 表示対象
    public float G_Appear = 3.0f; // 出現時間
    public Vector3 G_Trans = new Vector3(); // 炎の長さ
    public Vector3 G_Offset = new Vector3(); // 炎の長さ
    public float G_Disappearance = 2.0f; // 消滅時間
    public float G_DistanceInRight = 2.0f;  // x方向に出す距離
    public float G_DistanceInUp = 2.0f;  // y方向に出す距離
    private float delayTime = 3.0f; // ディレイ

    void OnEnable()
    {
        if (G_Target == null) { return; }
        // 位置変更
        Vector3 spawn = transform.position + G_Offset;
        GameObject spawned = Instantiate(
            G_Target, spawn, transform.rotation);
        spawned.GetComponent<G_Fire>().SetBuner(this); // バーナーの参照を設定(残骸対策で必要)
        // 大きさ指定
        spawned.transform.localScale = G_Trans;

        // 場に出ている全ワサビを取得
        Buner[] buners = FindObjectsOfType<Buner>();
        // X座標でソート
        var sort = buners.OrderBy(obj => obj.transform.position.x).ToArray();
        // 自身は何番目か
        int index = System.Array.IndexOf(sort, this);
        // 番数に応じて起動時間変更
        float delay = index * delayTime;

        Collider collider = GetComponent<BoxCollider>();
        if (collider != null) { collider.isTrigger = true; } // IsTriggerをオン

        StartCoroutine(ToggleObject(spawned, delay));   // コルーチン起動
    }
   
    IEnumerator ToggleObject(GameObject spawned, float delay)
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            // 出現
            spawned.SetActive(true);
            yield return new WaitForSeconds(G_Appear);

            // 消滅
            spawned.SetActive(false);
            yield return new WaitForSeconds(G_Disappearance);
        }
    }
}
