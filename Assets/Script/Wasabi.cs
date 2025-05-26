using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Wasabi : MonoBehaviour
{
    [Header("ワサビモデル（プレハブ）を３種類設定")]
    public Material Sushi;
    public Mesh SushiMesh;

    void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Wasabi] OnTriggerEnter: other.tag={other.tag}, this={gameObject.name}");
        if (!other.CompareTag("Player")) return;
        Debug.Log("[Wasabi] 触れた相手は Player です！");
        // 触れてきたオブジェクトのrendererを取得
        Renderer renderer = other.gameObject.GetComponent<Renderer>();
        // 触れてきたオブジェクトのMeshFilterを取得
        MeshFilter meshfilter = other.gameObject.GetComponent<MeshFilter>();

        renderer.material = Sushi;
        meshfilter.mesh = SushiMesh;
    }
}