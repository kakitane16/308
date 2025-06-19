using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasabi : MonoBehaviour
{

  
    [Header("判定する元のマテリアル（触れてきたオブジェクトがこのマテリアルを持っていたら）")]
    public Material[] sourceMaterials;

    [Header("置き換えるマテリアル（sourceMaterials と同じインデックスで対応付け）")]
    public Material[] targetMaterials;

    [Header("置き換えるメッシュ（sourceMaterials と同じインデックスで対応付け）")]
    public Mesh[] targetMeshes;

    [Header("SkinnedMeshRenderer を使う場合はチェック（MeshFilter か SkinnedMeshRenderer のどちらかに合わせる）")]
    public bool applyToSkinned = false;

    public float G_Weight = 0.7f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // 触れてきたオブジェクトの Renderer を取得
        Renderer rend = other.GetComponent<Renderer>();
        if (rend == null) return;

        // 触れてきたオブジェクトが持っている「今のマテリアル」を調べる
        Material currentMat = rend.sharedMaterial;
        if (currentMat == null) return;


        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        // 重量の変更
        if (rb != null)
            rb.mass = G_Weight;

        // sourceMaterials の中から currentMat と同じものを探す
        int idx = System.Array.IndexOf(sourceMaterials, currentMat);
        if (idx < 0 || idx >= targetMaterials.Length || idx >= targetMeshes.Length)
        {
            // 対応表にないマテリアルなら何もしない
            return;
        }

        // ① マテリアルを置き換え
        rend.material = targetMaterials[idx];

        // ② メッシュを置き換え
        if (applyToSkinned)
        {
            // SkinnedMeshRenderer 用
            SkinnedMeshRenderer smr = other.GetComponent<SkinnedMeshRenderer>();
            if (smr != null)
            {
                smr.sharedMesh = targetMeshes[idx];
            }
        }
        else
        {
            // MeshFilter + MeshRenderer 用
            MeshFilter mf = other.GetComponent<MeshFilter>();
            if (mf != null)
            {
                mf.mesh = targetMeshes[idx];
            }
        }
    }
}