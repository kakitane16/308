using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // プレイヤーのTransformをInspectorで設定

    [SerializeField] private Vector3 offset = new Vector3(0, 5, -5); // プレイヤーからの位置

    void LateUpdate()
    {
        if (player == null) return;

        // ライトの位置をプレイヤーに追従させる
        transform.position = player.position + offset;

        // ライトの向きをプレイヤーに向ける
        transform.LookAt(player.position);
    }
}
