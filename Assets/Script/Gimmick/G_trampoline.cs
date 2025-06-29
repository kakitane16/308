using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_trampoline : MonoBehaviour
{
    private bool IsDown;
    public float SpinPower = 20.0f;
    private Collider collider; 

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        // istrigger���I����
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
        Vector3 closetPoint = GetComponent<Collider>().ClosestPoint(playerRb.position); // �l�^�̐G�ꂽ�ʒu
        Vector3 normal = (playerRb.position - closetPoint).normalized;  // �x�N�g���擾
        Vector3 reflectDir = Vector3.Reflect(playerRb.velocity.normalized, normal); // ����
        Vector3 Pvelocity = reflectDir * playerRb.velocity.magnitude; // �V�����x�N�g���@�~�@���˂̋���
        Debug.Log(reflectDir);
        Debug.Log(closetPoint);

        // �|���̏�ʈȊO�̔��˂Ȃ珈�������Ȃ�
        if (other.transform.position.y + 0.7f < transform.position.y) 
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
            // istrigger���I�t��
            collider.isTrigger = false;

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
