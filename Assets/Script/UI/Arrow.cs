using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject player;
    public GameObject ArrowUI;

    public Transform T_player;
    // Start is called before the first frame update
    void Start()
    {
        //***UIの初期座標をプレイヤー（ネタ側）のよこに置く***
        //　　　ネタは毎回同じ場所から始まらないから
        //playerの座標を取得
        Vector3 playerPos = T_player.position + new Vector3(-8,7,0);
        //playerの座標と被るため少しずらす
        ArrowUI.transform.position = playerPos;
    }
}
