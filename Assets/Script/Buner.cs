using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buner : MonoBehaviour
{
    //例焼けた見た目の素材(マテリアル)をInspectorで設定できるように公開してる物
    //unityエディター上で寿司が焼けたりする時に表示させたいマテリアルをここにドラック＆ドロップするだけ
    public Material butnedMaterial;       //一個目の焼き色
    public Material OverButnedMaterial;  //二つ目の焦げ色
    private int burnCount = 0; //何回焼かれたか記録する

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
            Player  player = other.GetComponent<Player>();
            //見た目を変える前に、必要なものがちゃんとあるか確認している項目
            //&&は両方がちゃんとtrue(ある)のときだけ下の処理を実行するという意味
            if (renderer != null && butnedMaterial != null && player != null )
            {

                this.burnCount++; //触れたらカウントが上がる

                if(this.burnCount == 1) 
                {
                    //どちらもちゃんとあるときだけ焼けた見た目になるbutnedMaterialを実行できる
                    //一回目の焼き色
                    renderer.material = butnedMaterial;
                }
                else if(this.burnCount == 2) 
                {
                    //二回目の焦げ色
                    renderer.material = OverButnedMaterial;
                }
                else
                {
                    //三回目以降何もしないため書かない
                }
            }
        }
    }
}
