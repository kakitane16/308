using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeByMaterial : MonoBehaviour
{
    [Header("判定するマテリアル（元）")]
    public Material[] sourceMaterials;

    [Header("置き換えるマテリアル（先）")]
    public Material[] targetMaterials;

    [Header("置き換えるメッシュ（先）")]
    public Mesh[] targetMeshes;

    [Header("SkinnedMeshRenderer を使う場合はチェック")]
    public bool applyToSkinned = false;

    private void OnTriggerEnter(Collider other)
    {
        // Renderer を取得
        var rend = other.GetComponent<Renderer>();
        if (rend == null) return;

        // 元のマテリアルを探す
        Material src = rend.sharedMaterial;
        int idx = System.Array.IndexOf(sourceMaterials, src);
        if (idx < 0) return; // 対応表にないマテリアルなら無視

        // マテリアルを差し替え
        rend.material = targetMaterials[idx];

        // メッシュを差し替え
        if (applyToSkinned)
        {
            var smr = other.GetComponent<SkinnedMeshRenderer>();
            if (smr != null && idx < targetMeshes.Length)
                smr.sharedMesh = targetMeshes[idx];
        }
        else
        {
            var mf = other.GetComponent<MeshFilter>();
            if (mf != null && idx < targetMeshes.Length)
                mf.mesh = targetMeshes[idx];
        }
    }
}
