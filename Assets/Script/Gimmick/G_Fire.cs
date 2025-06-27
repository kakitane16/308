using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Fire : MonoBehaviour
{
    [Header("判定する元のマテリアル（触れてきたオブジェクトがこのマテリアルを持っていたら）")]
    public Material[] sourceMaterials;
    [Header("置き換えるマテリアル（sourceMaterials と同じインデックスで対応付け）")]
    public Material[] targetMaterials;
    [Header("置き換えるメッシュ（sourceMaterials と同じインデックスで対応付け）")]
    public Mesh[] targetMeshes;
    public float G_Weight = 0.7f;

    private Buner burner; // バーナーの参照

    void LateUpdate()
    {
        //if (burner == null)
        //    Destroy(gameObject); // バーナーがない場合は自分自身を削除
    }

    public void SetBuner(Buner b)
    {
        burner = b; // バーナーの参照を設定
    }
    // 上記2つはプレハブからの生成時にオブジェクトが残ってしまう問題を解決するために使用

    //何かがバーナーにぶつかって来た時に自動で呼ばれる関数
    //otherはぶつかってきた相手例えば寿司とかを表している
    public void OnTriggerEnter(Collider other)
    {
        //触れた相手が寿司かどうかをタグでチェックするもの、この場合だとPlayerが該当
        if (other.CompareTag("Player"))
        {
            //これはバーナーに触れた瞬間(other)上にあるbutnedMaterial(焼けた素材)から
            //Rendererを取り出してrendererに変更するもの
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer == null) return;
            // 触れてきたオブジェクトが持っている「今のマテリアル」を調べる
            Material currentMat = renderer.sharedMaterial;
            if (currentMat == null) return;

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb == null) return;
            // 重量の変更
            rb.mass = G_Weight;
            // sourceMaterials の中から currentMat と同じものを探す
            int idx = System.Array.IndexOf(sourceMaterials, currentMat);
            if (idx < 0 || idx >= targetMaterials.Length || idx >= targetMeshes.Length)
            {
                // 対応表にないマテリアルなら何もしない
                return;
            }

            // ① マテリアルを置き換え
            renderer.material = targetMaterials[idx];
            // ② メッシュを置き換え
            MeshFilter mf = other.GetComponent<MeshFilter>();
            if (mf != null)
            {
                mf.mesh = targetMeshes[idx];
            }
        }
    }
}
