using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Player py;
    public GameObject player;
    public GameObject ArrowUI;
    // Start is called before the first frame update
    void Start()
    {
        //***UIの初期座標をプレイヤー（ネタ側）のよこに置く***
        //　　　ネタは毎回同じ場所から始まらないから
        //playerの座標を取得
        //Vector2 playerPos = new Vector2(player.transform.position.x - 20.0f, player.transform.position.y - 10.0f);
        Vector2 playerPos = new Vector2(player.transform.position.x - 20.0f, player.transform.position.y);
        //playerの座標と被るため少しずらす
        ArrowUI.transform.position = playerPos;
    }

    // Update is called once per frame
    void Update()
    {
        //float rotateAgl = py.SAngleY + 10.0f;
        //ArrowUI.transform.Rotate(0, rotateAgl, 0);
        //ArrowRotate();
    }

    //Arrowの向きをプレイヤー側からの値を元に変換
    private void ArrowRotate()
    {
        
    }
}
