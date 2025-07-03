using UnityEngine;

public class Deshi : MonoBehaviour
{
    private Vector3 bounceDirection = Vector3.left; // ���˕���
    public float bounceMultiplier = 1.0f; // �����{��
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
                Debug.Log($"sasa.transform.position.z�i�З́F{sasa.transform.position.z}�j");
                FinishPos.y = sasa.transform.position.y;
                float power = player.GetLastShotPower();
                Vector3 bounceVector = bounceDirection.normalized * power * bounceMultiplier;
                Debug.Log($"bounceVector�i�󂯎�����З́F{bounceVector}�j");

                Vector3 directionToSpawned = (FinishPos - player.transform.position).normalized;
                Debug.Log($"FinishPos�i�З́F{FinishPos}�j");
                Debug.Log($"directionToSpawned�i�З́F{directionToSpawned}�j");
                Vector3 moveVector = directionToSpawned * power * bounceMultiplier;
                player.rb.AddForce(moveVector, ForceMode.Impulse);
                // �v���C���[�̈ʒu���ړ�
                // player.transform.position += moveVector;

                player.rb.velocity = Vector3.zero;

                Debug.Log($"��q�����˕Ԃ��܂����i�󂯎�����З́F{power}�j");
            }
        }
    }
}