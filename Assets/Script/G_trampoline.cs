using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_trampoline : MonoBehaviour
{
    // 衝突してきたオブジェクトの運動量保存
    private float SaveMoment;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 当たり判定
    private void OnCollisionEnter(Collision collision)
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        // 当たったObjectのタグがPlayerではないなら処理をしない
        if (other.gameObject.tag != "Player") { return; }

        // PlayerのRigidbodyを取得
        Rigidbody playerRb = other.attachedRigidbody;
        // 情報がないなら処理をしない
        if (playerRb == null) { return; }

        // 力の強さ取得
        Vector3 comingVelocity = playerRb.velocity;
        Vector3 comingDir = playerRb.velocity.normalized;
        float mass = playerRb.mass;
        // 力の向き取得
        float angle = Vector3.Angle(comingDir, transform.forward);

        // 正面±90度以外なら処理をしない
        if (angle < 90.0f) { return; }
        Debug.Log("前面ヒット" + angle);
        // 運動量のスカラー値を取得
        SaveMoment = mass * comingVelocity.magnitude;
        // 向いてる方向に飛ばす
        Vector3 pushDir = transform.forward.normalized;
        Vector3 newVelocity = pushDir * (SaveMoment / mass);

        playerRb.velocity = Vector3.zero;
        playerRb.AddForce(newVelocity, ForceMode.VelocityChange);
    }
}
