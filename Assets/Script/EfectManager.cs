using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectManager : MonoBehaviour
{

    [SerializeField] GameObject explosionPrefab;
    void OnCollisionEnter(Collision collision)
    {
        //衝突したオブジェクトが""をタグを持っていいたら
        if(collision.gameObject.CompareTag(""))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameObject explosion = Instantiate(explosionPrefab,
                transform.position,Quaternion.identity);
            Destroy(explosion, 2.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
