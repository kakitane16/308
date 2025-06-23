using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//このスクリプトではプレイヤーの値制御を行うため基本的にはUpdateは使わないで行こうかな
public class Sasa : MonoBehaviour
{
    public float moveMultiplier = 1.0f; // 威力に掛ける倍率（調整用）
    public Vector3 moveDirection = Vector3.left; // 押し出す方向

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                //重力を無効化
                player.rb.useGravity = false;
                //プレイヤー側の威力を取得
                float power = player.GetLastShotPower();
                Debug.Log(power);
                // moveDirection を正規化して方向だけを取り出し、
                // プレイヤーの威力 (power) と調整倍率 (moveMultiplier) を掛けて
                // プレイヤーを移動させるベクトル量を計算している
                Vector3 moveVector = moveDirection.normalized * power * moveMultiplier;

                // プレイヤーの位置を移動
                player.transform.position += moveVector;

                // 必要ならRigidbodyの速度も初期化
                player.rb.velocity = Vector3.zero;

                Debug.Log($"笹でプレイヤーを移動しました（威力：{power}）");
            }
        }
    }
}
