using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject player;
    public GameObject ArrowUI;
    public string ArrowTag = "Player"; // ターゲットのタグ

    public Transform T_player;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(ArrowTag);
            if (player != null)
            {
                player = player.transform.gameObject;
                T_player = player.transform;
            }
            else
            {
                Debug.LogWarning("オブジェクトが見つかりませんでした");
            }
        }

        //***UIの初期座標をプレイヤー（ネタ側）のよこに置く***
        //　　　ネタは毎回同じ場所から始まらないから
        //playerの座標を取得
        Vector3 playerPos = T_player.position + new Vector3(-7,8,0);
        //playerの座標と被るため少しずらす
        ArrowUI.transform.position = playerPos;
    }
}
