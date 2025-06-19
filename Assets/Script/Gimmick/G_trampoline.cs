using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_trampoline : MonoBehaviour
{
    private bool IsDown;
    public float SpinPower = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        // istrigger���I����
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // �����蔻��
    private void OnTriggerEnter(Collider other)
    {

        // ��������Object�̃^�O��Player�ł͂Ȃ��Ȃ珈�������Ȃ�
        if (other.gameObject.tag != "Player") { return; }

        // Player��Rigidbody���擾
        Rigidbody playerRb = other.attachedRigidbody;
        // ��񂪂Ȃ��Ȃ珈�������Ȃ�
        if (playerRb == null) { return; }

        // ���˂̌v�Z
        Vector3 closetPoint = GetComponent<Collider>().ClosestPoint(playerRb.position);
        Vector3 normal = (playerRb.position - closetPoint).normalized;
        Vector3 reflectDir = Vector3.Reflect(playerRb.velocity.normalized, normal);
        Vector3 Pvelocity = reflectDir * playerRb.velocity.magnitude;
        Debug.Log(reflectDir);
        // �������̔��˂Ȃ珈�������Ȃ�
        if (reflectDir.y > -0.75f) 
        {
            IsDown = true;
            return;
        }
        // ������G��Ă��Ȃ��Ȃ珈��
        if (!IsDown)
        {
            // ���̃I�u�W�F�N�g��G�ꂽ�I�u�W�F�N�g�̈ʒu�ֈړ�
            transform.position = closetPoint;
            // �G��Ă����I�u�W�F�N�g�ɒǏ]
            transform.SetParent(other.transform);

            // Y����]
            Vector3 spin = new Vector3 (0.0f, -300.0f, 0.0f);
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(-Pvelocity, ForceMode.VelocityChange);
            playerRb.AddTorque(spin * SpinPower, ForceMode.Impulse);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // ������
        IsDown = false;
    }
}
