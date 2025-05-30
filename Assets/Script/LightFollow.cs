using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // �v���C���[��Transform��Inspector�Őݒ�

    [SerializeField] private Vector3 offset = new Vector3(0, 5, -5); // �v���C���[����̈ʒu

    void LateUpdate()
    {
        if (player == null) return;

        // ���C�g�̈ʒu���v���C���[�ɒǏ]������
        transform.position = player.position + offset;

        // ���C�g�̌������v���C���[�Ɍ�����
        transform.LookAt(player.position);
    }
}
