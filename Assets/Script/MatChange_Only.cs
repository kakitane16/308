using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChange_Only : MonoBehaviour
{

    public Material ChangeMaterial;  // playerのマテリアルを変更する　
    private bool Check = false; // ギミックが使用されたか

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

        Renderer playerren = other.GetComponent<Renderer>();
        Player player = other.GetComponent<Player>();

        // 情報がないなら処理をしない
        if (playerren == null || player == null) { return; }

        // Playerのマテリアルを変える
        playerren.material = ChangeMaterial;
        this.Check = true;

        // 一度ギミックが使われたら
        if (!Check) { return; }
        // このオブジェクトを消す
        Destroy(this.gameObject);
    }
}
