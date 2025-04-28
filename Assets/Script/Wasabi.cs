using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasabi : MonoBehaviour
{
    //プレイヤーがワサビに触れたら↓の三段階分のマテリアルに変わる
    //Materialを多く使うため以下からMaterial→Matにする
    public Material LargeMat;   //ワサビの初期状態プレイヤーがまだ触れていない（Large→大きい）
    public Material MediumMat;  //プレイヤーが一回触れて少し減った状態（Medium→中）
    public Material SmallMat;   //プレイヤーが二回触れてだいぶ減った状態（small→小量）

    private int level = 3;      //ワサビの残量をレベルで管理(3＝large, 2＝medium, 1＝small, 0＝消滅)
    private Renderer rend;      //Renderer→rend
    private Collider col;       //Collider→col

    //Awake(起きる、目覚める)ゲームオブジェクトがシーンに読み込まれ、
    //アクティブになった瞬間に一度だけ呼ばれる初期化用メソッド
    void Awake()
    {
        //↓を使えば後に直接見た目を変更できるようになる
        rend = GetComponent<Renderer>();
        //↓ワサビが使い切られた時当たり判定を無効化したり後から判定の有無を切り替えられる
        col = GetComponent<Collider>();
        //UpdateVisual() を呼び出して、ゲーム開始時のワサビの見た目を正しい状態にセット
        //levelに応じてセットするマテリアルを呼び出す
        //ワサビの状態を最初からLargeMatにする
        rend.material = LargeMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Playerのタグが付いているやつ以外触れても実行されない
        if (!other.CompareTag("Player")) { return; }

        // ② 触れたプレイヤーの Renderer を取ってきて… 
        Renderer playerRend = other.GetComponent<Renderer>();
        Player player = other.GetComponent<Player>();
        if (playerRend == null) return;

        //残量があれば一段階減らす
        //残量(level)2から3の時まで減らす
        if (level > 1)
        {
            level--;
            //そのレベルに対応したマテリアルに切り替え
            UpdeteVisual(playerRend);

        }
        else if (level == 1)
        {
            //残量がちょうど１の時（少量）
            level = 0;               //残量０に設定
            UpdeteVisual(playerRend);          //レンダ―をオフに
            col.enabled = false;     //Collederを無効化,以降プレイヤーが触れても反応しない
            Destroy(gameObject);     //見た目を完全に削除

        }

    }

    private void UpdeteVisual(Renderer PRend)
    {
        //levelの値をもとに該当するCaseのブロック内の処理だけを実行する
        //switchを使うのはif…else if…else を何度も書くより
        //「値がこれこれのときはこれをする」という複数の分岐をすっきり書けるから
        switch (level)
        {
            case 3:
                rend.material = LargeMat;
                //breakによってswitch分を抜けるため下のプログラムが誤って実行されない
                break;

            case 2:
                rend.material = MediumMat;
                PRend.material = LargeMat;
                break;

            case 1:
                rend.material = SmallMat;
                PRend.material = MediumMat;
                break;
            //ワサビが完全に使い切られた状態
            case 0:
                PRend.material = SmallMat;
                //マテリアルを切り替える代わりにレンダ―自体をオフにして見えなくする
                //enabled→有効になっているそれをオフ
                rend.enabled = false;
                break;
        }

    }
}
