using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buner : MonoBehaviour
{
    public GameObject G_Target; // 表示対象
    public float G_Appear = 3.0f; // 出現時間
    public float G_Disappearance = 2.0f; // 消滅時間
    public float G_DistanceInRight = 2.0f;  // x方向に出す距離
    public float G_DistanceInUp = 2.0f;  // y方向に出す距離

    void Start()
    {
        Vector3 spawn = transform.position + 
           Vector3.up * G_DistanceInUp + transform.right * G_DistanceInRight;
        GameObject spawned = Instantiate(
            G_Target, spawn, Quaternion.identity);
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
