using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_trampoline : MonoBehaviour
{
    private bool IsDown;
    public float SpinPower = 20.0f;
    private Collider collider; 

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        // istriggerをオンに
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 当たり判定
    private void OnTriggerEnter(Collider other)
    {

        // 当たったObjectのタグがPlayerではないなら処理をしない
        if (other.gameObject.tag != "Player") { return; }

        // PlayerのRigidbodyを取得
        Rigidbody playerRb = other.attachedRigidbody;
        // 情報がないなら処理をしない
        if (playerRb == null) { return; }

        // 反射の計算
        Vector3 closetPoint = GetComponent<Collider>().ClosestPoint(playerRb.position); // ネタの触れた位置
        Vector3 normal = (playerRb.position - closetPoint).normalized;  // ベクトル取得
        Vector3 reflectDir = Vector3.Reflect(playerRb.velocity.normalized, normal); // 反射
        Vector3 Pvelocity = reflectDir * playerRb.velocity.magnitude; // 新しいベクトル　×　発射の強さ
        Debug.Log(reflectDir);
        Debug.Log(closetPoint);

        // 竹串の上面以外の反射なら処理をしない
        if (other.transform.position.y + 0.7f < transform.position.y) 
        {
            IsDown = true;
            return;
        }
        // 下から触れていないなら処理
        if (!IsDown)
        {
            // このオブジェクトを触れたオブジェクトの位置へ移動
            transform.position = closetPoint;
            // 触れてきたオブジェクトに追従
            transform.SetParent(other.transform);
            // istriggerをオフに
            collider.isTrigger = false;

            // Y軸回転
            Vector3 spin = new Vector3 (0.0f, -300.0f, 0.0f);
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(-Pvelocity, ForceMode.VelocityChange);
            playerRb.AddTorque(spin * SpinPower, ForceMode.Impulse);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // 初期化
        IsDown = false;
    }
}
