using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // グローバル変数
    public Transform g_Target;      // 追従対象
    public Transform g_FocusObject; // 注視対象
    public Vector3 g_Offset = new Vector3(0, 5, -10);  // カメラ相対位置
    public float g_FollowSpeed = 2.0f;  // 追従対象に戻るまでの時間
    public float g_MovementThreshold = 0.01f; // 処理を行うために必要な追従対象の移動量
    public float g_FocusDuration = 2.0f;   // 注視時間（秒）

    private Vector3 lasttargetpos;
    private enum n_CameraState { Idle, Focusing, Following } // カメラの状態
    private n_CameraState state = n_CameraState.Idle;
    private float focusTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (g_Target != null)
        {   // Targetのtransform情報をlastに代入
            lasttargetpos = g_Target.position;
        }
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
                                     desiredPosition, Time.deltaTime * g_FollowSpeed); //ゆっくりと
                transform.LookAt(g_Target);
                break;
        }
        lasttargetpos = g_Target.position;
    }
}
