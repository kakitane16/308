using UnityEngine;

public class Deshi : MonoBehaviour
{
    private Vector3 bounceDirection = Vector3.left; // 反射方向
    public float bounceMultiplier = 1.0f; // 調整倍率
    public Vector3 FinishPos;
    public bool Hit = false;
    Sasa sasa;
    Player player;
    void Start()
    {

        player = FindObjectOfType<Player>();
        sasa = FindObjectOfType<Sasa>();
    }
    void Update()
    {
        if(Hit)
        {
            if (FinishPos.z > player.transform.position.z)
            {
                player.rb.useGravity = true;
                player.rb.velocity = Vector3.zero;
                
            }
           else if(FinishPos.z <= player.transform.position.z)
            {
                player.rb.useGravity = true;
            }
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (player != null )
            {
                Hit = true;
                sasa.DeshiHit = true;
                FinishPos = sasa.DeshiPos + sasa.DeshiPos;  
                FinishPos.z = sasa.transform.position.z;
                Debug.Log($"sasa.transform.position.z（威力：{sasa.transform.position.z}）");
                FinishPos.y = sasa.transform.position.y;
                float power = player.GetLastShotPower();
                Vector3 bounceVector = bounceDirection.normalized * power * bounceMultiplier;
                Debug.Log($"bounceVector（受け取った威力：{bounceVector}）");

                Vector3 directionToSpawned = (FinishPos - player.transform.position).normalized;
                Debug.Log($"FinishPos（威力：{FinishPos}）");
                Debug.Log($"directionToSpawned（威力：{directionToSpawned}）");
                Vector3 moveVector = directionToSpawned * power * bounceMultiplier;
                player.rb.AddForce(moveVector, ForceMode.Impulse);
                // プレイヤーの位置を移動
                // player.transform.position += moveVector;

                player.rb.velocity = Vector3.zero;

                Debug.Log($"弟子が跳ね返しました（受け取った威力：{power}）");
            }
        }
    }
}