using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Fan : MonoBehaviour
{
    public float Force = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

        // 自分の向いている方向を取得
        Vector3 forward = transform.forward;
        // playerをその方向に飛ばす
       // playerRb.velocity = Vector3.zero;
        playerRb.AddForce(forward.normalized * Force, ForceMode.VelocityChange);
    }
}
