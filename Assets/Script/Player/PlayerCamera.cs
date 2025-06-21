using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour
{
    // �O���[�o���ϐ�
    private Transform g_Target;      // �Ǐ]�Ώ�
    public Transform g_FocusObject; // �����Ώ�
    public string g_TargetTag = "Player"; // �Ǐ]�Ώۂ̃^�O
    public Vector3 g_Offset = new Vector3(0.0f, -5.0f, -10.0f);  // �J�������Έʒu
    public Vector3 g_Position = new Vector3(0.0f, 20.0f, -85.0f);  // �J�����ʒu
    public Quaternion g_Rotation = Quaternion.Euler(22.0f, 0.0f, 0.0f);  // �J������]
    public float g_FollowSpeed = 2.0f;  // �Ǐ]�Ώۂɖ߂�܂ł̎���
    public float g_MovementThreshold = 0.01f; // �������s�����߂ɕK�v�ȒǏ]�Ώۂ̈ړ���
    public float g_FocusDuration = 2.0f;   // �������ԁi�b�j
    private float g_WaitTime = 5.0f;         // �҂����ԁi�b�j
    private float g_Timer = 0.0f;            // �o�ߎ���
    private Vector3 lasttargetpos;
    private enum n_CameraState { Idle, Focusing, Following } // �J�����̏��
    private n_CameraState state = n_CameraState.Idle;
    private float focusTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = g_Position;
        this.transform.rotation = g_Rotation;
        // Player�^�O�����Ă���I�u�W�F�N�g��Ǐ]
        GameObject playercamera = GameObject.FindGameObjectWithTag(g_TargetTag);
        if (playercamera != null)
        {
            g_Target = playercamera.transform;
            lasttargetpos = g_Target.position;  // �Ǐ]�Ώۂ̈ʒu���L�^
        }
    }
    // �S�Ă̏��������������ɏ������s�������̂�
    void LateUpdate()
    {
            // �����Ώۂ����Ȃ������珈�������Ȃ�(�Ǐ]�Ώۂɂ������͍폜���܂����A���ɔ��肵���̂��ɂ��̏������s���邽��)
      // if (g_FocusObject == null) { return; }
       // target�̈ړ��ʂ��擾
       Vector3 movement = g_Target.position - lasttargetpos;
    
       switch (state)
       {
           case n_CameraState.Idle:
    
               // Transform Camera = this.transform;
               // Camera.position = g_Target.TransformPoint(g_Offset); // �l�^�̌����
               //// Camera.LookAt(g_Target);
               // Camera.rotation = g_Target.rotation;
               //     /*Quaternion.Lerp(
               //     transform.rotation, g_Target.rotation, Time.deltaTime * g_CameraSpeed);*/
    
               if (movement.magnitude > g_MovementThreshold) //��������
               {
                   state = n_CameraState.Focusing; // ��ԑJ��
                   focusTimer = g_FocusDuration;   // �������ԑ��
               }
               break;
    
           case n_CameraState.Focusing:
              // transform.LookAt(g_FocusObject); // ����
               focusTimer -= Time.deltaTime;    // ���Ԍo�߂őJ��
               if (focusTimer <= 0.0f)
               {
                   state = n_CameraState.Following;
               }
               break;
    
           case n_CameraState.Following:
               Vector3 desiredPosition = g_Target.position + g_Offset;
               transform.position = Vector3.Lerp(transform.position,
                                    desiredPosition, Time.deltaTime * g_FollowSpeed); //���炩��

                // �^�O "Table" �����Ă��邷�ׂĂ� GameObject ���擾
                GameObject[] tables = GameObject.FindGameObjectsWithTag("Table");
                foreach (GameObject table in tables)
                {
                    // MeshRenderer���擾����\���ɂ���
                    MeshRenderer mr = table.GetComponent<MeshRenderer>();
                    mr.enabled = false;
                }
                transform.LookAt(g_Target);
               if (movement.magnitude < g_MovementThreshold) //�~�܂�����
               {
                   // 3�b�҂�
                   g_Timer += Time.deltaTime;
                   if (g_Timer >= g_WaitTime)
                   {
                       SceneManager.LoadScene(2);
                   }
               }
               break;
       }
       lasttargetpos = g_Target.position;
    }
}
