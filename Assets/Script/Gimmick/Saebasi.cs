using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saebasi : MonoBehaviour
{

    bool used = false;

    //何かがぶつかって来た時に動く処理を書く場所
    private void OnTriggerEnter(Collider other)
    {
        if (used) return;
        //このPlayerってついてる名前（タグ）にだけ反応して動く処理
        if (other.CompareTag("Player"))
        {
            //otherはぶつかって来たobjectを指す
            //Rigitbody動きのデータをとる、落ちる、跳ねる、飛ぶなど　Rigitbodyがないとできない
            Rigidbody rb = other.GetComponent<Rigidbody>();

            //rbにRigitbodyが入っているならその後の処理を実行(null)
            if (rb != null)
            {
                //velocity(速度)をゼロにして止める
                rb.velocity = Vector3.zero;
                //kinematic(物理or物体)を止めて固定
                rb.isKinematic = true;
                //Coroutine(時間経過)を使う処理をする関数
                //ReleaseAfterDelay(rb)コルーチンをスタートさせて実行する命令
                StartCoroutine(ReleaseAfterDelay(rb));
            }
        }
    }

    //IEnumerator.リストや配列の要素を順番に取り出して処理する関数
    private System.Collections.IEnumerator ReleaseAfterDelay(Rigidbody rb)
    {
        // yield returnここでいったん止まるっている合図
        //new WaitForSeconds(3f)3秒待つという指示
        yield return new WaitForSeconds(3f);

        //Rigidbodyの.isKinematicはtureの場合動かない、falseの場合重力とか力を受けて動けるようになる
        //↓このオブジェクトはもう動いていいよという処理
        rb.isKinematic = false;
        //真下に向かって速さ３で落ちる処理
        rb.velocity = Vector3.down * 10f;

        used = true;
    }



}
