using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_trampoline : MonoBehaviour
{
    // �Փ˂��Ă����I�u�W�F�N�g�̉^���ʕۑ�
    private float SaveMoment;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // �����蔻��
    private void OnCollisionEnter(Collision collision)
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        // ��������Object�̃^�O��Player�ł͂Ȃ��Ȃ珈�������Ȃ�
        if (other.gameObject.tag != "Player") { return; }

        // Player��Rigidbody���擾
        Rigidbody playerRb = other.attachedRigidbody;
        // ��񂪂Ȃ��Ȃ珈�������Ȃ�
        if (playerRb == null) { return; }

        // �͂̋����擾
        Vector3 comingVelocity = playerRb.velocity;
        Vector3 comingDir = playerRb.velocity.normalized;
        float mass = playerRb.mass;
        // �͂̌����擾
        float angle = Vector3.Angle(comingDir, transform.forward);

        // ���ʁ}90�x�ȊO�Ȃ珈�������Ȃ�
        if (angle < 90.0f) { return; }
        Debug.Log("�O�ʃq�b�g" + angle);
        // �^���ʂ̃X�J���[�l���擾
        SaveMoment = mass * comingVelocity.magnitude;
        // �����Ă�����ɔ�΂�
        Vector3 pushDir = transform.forward.normalized;
        Vector3 newVelocity = pushDir * (SaveMoment / mass);

        playerRb.velocity = Vector3.zero;
        playerRb.AddForce(newVelocity, ForceMode.VelocityChange);
    }
}
