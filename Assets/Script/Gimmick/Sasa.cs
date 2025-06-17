using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//このスクリプトではプレイヤーの値制御を行うため基本的にはUpdateは使わないで行こうかな
public class Sasa : MonoBehaviour
{
    private Player player;
    private float ShotPower;

    //playerオブジェクトをどれくらい動かしたいか
    //もしかしたらプレイヤー側に渡すかもなのでpublic(使わないならprivateに切り替えといて
    public float MoveX; 
    public float MoveZ;

    private void Start()
    {
        player = GetComponent<Player>();

        //毎回更新をかけるが誤作動しないように初期化をかけて置く
        ShotPower = 0.0f; 
        MoveX = 0.0f;
        MoveZ = 0.0f;
    }

    private void OnCollisionEnter(Collision p_collision)
    {
        ChangePos();
    }

    public void ChangePos()
    {
        //プレイヤーのforceStrengthは値的に最大値20fで持ってきているため
        //マップ半分までの座標ということは20fに2.5fを書ければなる
        //ただし、最小値が0.0f or　2.5fになるためそれよりしたい場合は再設計が必要
        ShotPower = player.forceStrength * 2.5f;

        //どれぐらいに値が動くか
        MoveX = ShotPower;

        //プレイヤーの座標をギミック側で更新をかける
        //最初は最大値の座標にだすことから
        player.rb.position = new Vector3(MoveX, 0.0f, MoveZ);
        
        //笹の挙動では基本的には重力がないため消去
        player.rb.useGravity = false;
    }
}
