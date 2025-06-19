using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour
{
    // グローバル変数
    public Transform g_Target;      // 追従対象
    public Transform g_FocusObject; // 注視対象
    public string g_TargetTag = "Player"; // 追従対象のタグ
    public Vector3 g_Offset = new Vector3(0.0f, -5.0f, -10.0f);  // カメラ相対位置
    public Vector3 g_Position = new Vector3(0.0f, 20.0f, -85.0f);  // カメラ位置
    public Quaternion g_Rotation = Quaternion.Euler(22.0f, 0.0f, 0.0f);  // カメラ回転
    public float g_FollowSpeed = 2.0f;  // 追従対象に戻るまでの時間
    public float g_MovementThreshold = 0.01f; // 処理を行うために必要な追従対象の移動量
    public float g_FocusDuration = 2.0f;   // 注視時間（秒）
    private float g_WaitTime = 5.0f;         // 待ち時間（秒）
    private float g_Timer = 0.0f;            // 経過時間
    private Vector3 lasttargetpos;
    private enum n_CameraState { Idle, Focusing, Following } // カメラの状態
    private n_CameraState state = n_CameraState.Idle;
    private float focusTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (g_Target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(g_TargetTag);
            if (player != null)
            {
                g_Target = player.transform;
            }
            else
            {
                Debug.LogWarning("オブジェクトが見つかりませんでした");
            }
        }

        if (g_Target != null)
        {   // Targetのtransform情報をlastに代入
            lasttargetpos = g_Target.position;
        }
        this.transform.position = g_Position;
        this.transform.rotation = g_Rotation;
        
    }

    // 全ての処理がおわった後に処理を行いたいので
    void LateUpdate()
    {
        // 追従対象か注視対象がいなかったら処理をしない
        if (g_Target == null || g_FocusObject == null) { return; }
        // targetの移動量を取得
        Vector3 movement = g_Target.position - lasttargetpos;

        switch (state)
        {
            case n_CameraState.Idle:

               // Transform Camera = this.transform;
               // Camera.position = g_Target.TransformPoint(g_Offset); // ネタの後方へ
               //// Camera.LookAt(g_Target);
               // Camera.rotation = g_Target.rotation;
               //     /*Quaternion.Lerp(
               //     transform.rotation, g_Target.rotation, Time.deltaTime * g_CameraSpeed);*/

                if (movement.magnitude > g_MovementThreshold) //動いたら
                {
                    state = n_CameraState.Focusing; // 状態遷移
                    focusTimer = g_FocusDuration;   // 注視時間代入
                }
                break;

            case n_CameraState.Focusing:
                transform.LookAt(g_FocusObject); // 注視
                focusTimer -= Time.deltaTime;    // 時間経過で遷移
                if (focusTimer <= 0.0f)
                {
                    state = n_CameraState.Following;
                }
                break;

            case n_CameraState.Following:
                Vector3 desiredPosition = g_Target.position + g_Offset;
                transform.position = Vector3.Lerp(transform.position,
                                     desiredPosition, Time.deltaTime * g_FollowSpeed); //滑らかに
                transform.LookAt(g_Target);
                if (movement.magnitude < g_MovementThreshold) //止まったら
                {
                    // 3秒待つ
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
