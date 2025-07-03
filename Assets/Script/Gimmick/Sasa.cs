using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//このスクリプトではプレイヤーの値制御を行うため基本的にはUpdateは使わないで行こうかな
public class Sasa : MonoBehaviour
{
    public float moveMultiplier = 1.0f; // 威力に掛ける倍率（調整用）
    private Vector3 moveDirection = Vector3.left; // 押し出す方向
    public GameObject G_Target; // 表示対象
    public Transform targetObject; //t
    public bool DeshiHit = false;
    public Vector3 DeshiPos;


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
                player.currentPower = power;
                // moveDirection を正規化して方向だけを取り出し、
                // プレイヤーの威力 (power) と調整倍率 (moveMultiplier) を掛けて
                // プレイヤーを移動させるベクトル量を計算している
                Debug.Log($"Movevector（位置：{moveDirection.normalized}）");
                Vector3 moveVector = moveDirection.normalized * power * moveMultiplier;
                // 弟子の出現位置を設定
                DeshiPos = (power * moveMultiplier) / 2.0f *  moveDirection.normalized;
                Debug.Log($"弟子ポス（位置：{DeshiPos}）");
                DeshiPos.z = transform.position.z + 5.0f;
                DeshiPos.y = transform.position.y;
                // 弟子をスポーン
                GameObject spawned = Instantiate(
            G_Target, DeshiPos, transform.rotation);

                // 弟子の方向に向かって移動方向を再計算
                if (!DeshiHit)
                {
                    Vector3 directionToSpawned = (spawned.transform.position - player.transform.position).normalized;
                    moveVector = directionToSpawned * power * moveMultiplier;

                    // プレイヤーの位置を移動
                    // player.transform.position += moveVector;
                    player.rb.AddForce(moveVector, ForceMode.Impulse);

                    Debug.Log($"笹でプレイヤーを移動しました（威力：{power}）");
                }
                // 必要ならRigidbodyの速度も初期化
                player.rb.velocity = Vector3.zero;
            }
        }
    }
}
