using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject player;
    public GameObject ArrowUI;
    public string ArrowTag = "Player"; // ターゲットのタグ

    public Transform T_player;

    void Start()    // 現状の開発環境での動作確認用の仮置きです、プレハブ生成版に開発が切り替わった段階で削除してください
    {
        if (T_player == null) return;

        //***UIの初期座標をプレイヤー（ネタ側）のよこに置く***
        //　　　ネタは毎回同じ場所から始まらないから
        //playerの座標を取得
        Vector3 playerPos = T_player.position + new Vector3(-7, 8, 0);
        //playerの座標と被るため少しずらす
        ArrowUI.transform.position = playerPos;
    }
    // Start is called before the first frame update
    void LateUpdate()
    {
        if (T_player != null) return;

        // ***プレイヤーのTransformを取得***
        GameObject player = GameObject.FindGameObjectWithTag(ArrowTag);
        if (player != null)
        {
            player = player.transform.gameObject;
            T_player = player.transform;
        }

        if (T_player == null) return;

        //***UIの初期座標をプレイヤー（ネタ側）のよこに置く***
        //　　　ネタは毎回同じ場所から始まらないから
        //playerの座標を取得
        Vector3 playerPos = T_player.position + new Vector3(-7,8,0);
        //playerの座標と被るため少しずらす
        ArrowUI.transform.position = playerPos;
        // (こちらに処理を移動しました、T_playerがnullでは正常に取得できないため)
    }
}
