using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̃X�N���v�g�ł̓v���C���[�̒l������s�����ߊ�{�I�ɂ�Update�͎g��Ȃ��ōs��������
public class Sasa : MonoBehaviour
{
    public float moveMultiplier = 1.0f; // �З͂Ɋ|����{���i�����p�j
    public Vector3 moveDirection = Vector3.left; // �����o������

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
                Debug.Log(power);
                // moveDirection �𐳋K�����ĕ������������o���A
                // �v���C���[�̈З� (power) �ƒ����{�� (moveMultiplier) ���|����
                // �v���C���[���ړ�������x�N�g���ʂ��v�Z���Ă���
                Vector3 moveVector = moveDirection.normalized * power * moveMultiplier;

                // �v���C���[�̈ʒu���ړ�
                player.transform.position += moveVector;

                // �K�v�Ȃ�Rigidbody�̑��x��������
                player.rb.velocity = Vector3.zero;

                Debug.Log($"���Ńv���C���[���ړ����܂����i�З́F{power}�j");
            }
        }
    }
}
