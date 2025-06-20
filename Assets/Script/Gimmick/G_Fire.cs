using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Fire : MonoBehaviour
{
    //例焼けた見た目の素材(マテリアル)をInspectorで設定できるように公開してる物
    //unityエディター上で寿司が焼けたりする時に表示させたいマテリアルをここにドラック＆ドロップするだけ
    public Color G_Color = new Color(0.0f, 0.0f, 0.0f);       // 焼き色マテリアル
    public float G_Weight = 0.7f;

    private Buner burner; // バーナーの参照

    void LateUpdate()
    {
        if (burner == null)
            Destroy(gameObject); // バーナーがない場合は自分自身を削除
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
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            // 重量の変更
            if (rb != null)
                rb.mass = G_Weight;
            //見た目を変える前に、必要なものがちゃんとあるか確認している項目
            //&&は両方がちゃんとtrue(ある)のときだけ下の処理を実行するという意味
            if (renderer != null && G_Color != null )
            {
                renderer.material.color = G_Color;
            }
        }
    }
}
