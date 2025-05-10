using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // �O���[�o���ϐ�
    public Transform g_Target;      // �Ǐ]�Ώ�
    public Transform g_FocusObject; // �����Ώ�
    public Vector3 g_Offset = new Vector3(0, 5, -10);  // �J�������Έʒu
    public float g_FollowSpeed = 2.0f;  // �Ǐ]�Ώۂɖ߂�܂ł̎���
    public float g_MovementThreshold = 0.01f; // �������s�����߂ɕK�v�ȒǏ]�Ώۂ̈ړ���
    public float g_FocusDuration = 2.0f;   // �������ԁi�b�j

    private Vector3 lasttargetpos;
    private enum n_CameraState { Idle, Focusing, Following } // �J�����̏��
    private n_CameraState state = n_CameraState.Idle;
    private float focusTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (g_Target != null)
        {   // Target��transform����last�ɑ��
            lasttargetpos = g_Target.position;
        }
    }

    // �S�Ă̏��������������ɏ������s�������̂�
    void LateUpdate()
    {
        // �Ǐ]�Ώۂ������Ώۂ����Ȃ������珈�������Ȃ�
        if (g_Target == null || g_FocusObject == null) { return; }
        // target�̈ړ��ʂ��擾
        Vector3 movement = g_Target.position - lasttargetpos;

        switch (state)
        {
            case n_CameraState.Idle:
                if (movement.magnitude > g_MovementThreshold) //��������
                {
                    state = n_CameraState.Focusing; // ��ԑJ��
                    focusTimer = g_FocusDuration;   // �������ԑ��
                }
                break;

            case n_CameraState.Focusing:
                transform.LookAt(g_FocusObject); // ����
                focusTimer -= Time.deltaTime;    // ���Ԍo�߂őJ��
                if (focusTimer <= 0.0f)
                {
                    state = n_CameraState.Following;
                }
                break;

            case n_CameraState.Following:
                Vector3 desiredPosition = g_Target.position + g_Offset;
                transform.position = Vector3.Lerp(transform.position,
                                     desiredPosition, Time.deltaTime * g_FollowSpeed); //��������
                transform.LookAt(g_Target);
                break;
        }
        lasttargetpos = g_Target.position;
    }
}
