using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_trampoline : MonoBehaviour
{
    private bool IsDown;
    public float SpinPower = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        // istriggerをオンに
        Collider collider = GetComponent<Collider>();
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
        Vector3 closetPoint = GetComponent<Collider>().ClosestPoint(playerRb.position);
        Vector3 normal = (playerRb.position - closetPoint).normalized;
        Vector3 reflectDir = Vector3.Reflect(playerRb.velocity.normalized, normal);
        Vector3 Pvelocity = reflectDir * playerRb.velocity.magnitude;
        Debug.Log(reflectDir);
        // 下方向の反射なら処理をしない
        if (reflectDir.y > -0.75f) 
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
