using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        if (G_Target == null) { return; }
        // 位置変更
        Vector3 spawn = transform.position + G_Offset;
        GameObject spawned = Instantiate(
            G_Target, spawn, Quaternion.identity);
        // 大きさ指定
        spawned.transform.localScale = G_Trans;

        StartCoroutine(ToggleObject(spawned));   // コルーチン起動
        Collider collider = GetComponent<BoxCollider>();
        if (collider != null) { collider.isTrigger = true; } // IsTriggerをオン
    }
   
    IEnumerator ToggleObject(GameObject spawned)
    {
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
