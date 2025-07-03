using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̃X�N���v�g�ł̓v���C���[�̒l������s�����ߊ�{�I�ɂ�Update�͎g��Ȃ��ōs��������
public class Sasa : MonoBehaviour
{
    public float moveMultiplier = 1.0f; // �З͂Ɋ|����{���i�����p�j
    private Vector3 moveDirection = Vector3.left; // �����o������
    public GameObject G_Target; // �\���Ώ�
    public Transform targetObject; //t
    public bool DeshiHit = false;
    public Vector3 DeshiPos;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                //�d�͂𖳌���
                player.rb.useGravity = false;
                //�v���C���[���̈З͂��擾
                float power = player.GetLastShotPower();
                player.currentPower = power;
                // moveDirection �𐳋K�����ĕ������������o���A
                // �v���C���[�̈З� (power) �ƒ����{�� (moveMultiplier) ���|����
                // �v���C���[���ړ�������x�N�g���ʂ��v�Z���Ă���
                Debug.Log($"Movevector�i�ʒu�F{moveDirection.normalized}�j");
                Vector3 moveVector = moveDirection.normalized * power * moveMultiplier;
                // ��q�̏o���ʒu��ݒ�
                DeshiPos = (power * moveMultiplier) / 2.0f *  moveDirection.normalized;
                Debug.Log($"��q�|�X�i�ʒu�F{DeshiPos}�j");
                DeshiPos.z = transform.position.z + 5.0f;
                DeshiPos.y = transform.position.y;
                // ��q���X�|�[��
                GameObject spawned = Instantiate(
            G_Target, DeshiPos, transform.rotation);

                // ��q�̕����Ɍ������Ĉړ��������Čv�Z
                if (!DeshiHit)
                {
                    Vector3 directionToSpawned = (spawned.transform.position - player.transform.position).normalized;
                    moveVector = directionToSpawned * power * moveMultiplier;

                    // �v���C���[�̈ʒu���ړ�
                    // player.transform.position += moveVector;
                    player.rb.AddForce(moveVector, ForceMode.Impulse);

                    Debug.Log($"���Ńv���C���[���ړ����܂����i�З́F{power}�j");
                }
                // �K�v�Ȃ�Rigidbody�̑��x��������
                player.rb.velocity = Vector3.zero;
            }
        }
    }
}
