using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    Player player;
    public GameObject prefab;                 // 配置したいオブジェクト（プレハブ）
    public int count = 10;                    // 配置する個数
    public float timeStep = 0.1f;             // シミュレーションの時間ステップ（等間隔）
    public Vector3 gravity = new Vector3(0, -9.81f, 0); // 重力

    private Vector3 launchVelocity;
    private List<GameObject> markers = new List<GameObject>();

    // 前回の入力値
    private float prevForceStrength;
    private float prevAngleY;

    void Start()
    {
        player = GetComponent<Player>();

        for (int i = 0; i < count; i++)
        {
            GameObject marker = Instantiate(prefab);
            markers.Add(marker);
        }

        // 初回に一度更新
        UpdateMarkers();
    }

    void Update()
    {
        if (player == null) return;

        // 力 or 角度が変わったかを確認
        if (player.forceStrength != prevForceStrength || player.SAngleY != prevAngleY)
        {
            UpdateMarkers();
        }
    }

    void UpdateMarkers()
    {
        if (player == null || markers.Count == 0) return;

        Vector3 startPos = player.transform.position;

        // Y軸の角度をラジアンに変換して放物線方向に反映（例: 上に打ち上げる角度）
        float angleRad = player.SAngleY * Mathf.Deg2Rad;
        Vector3 direction = player.transform.forward;
        launchVelocity = Quaternion.AngleAxis(player.SAngleY, player.transform.right) * direction * player.forceStrength;

        for (int i = 0; i < count; i++)
        {
            float time = timeStep * i;
            Vector3 pos = SimulatePosition(startPos, launchVelocity, time, gravity);
            markers[i].transform.position = pos;
        }

        // 現在の設定を保存
        prevForceStrength = player.forceStrength;
        prevAngleY = player.SAngleY;
    }

    // 放物線の位置を計算
    Vector3 SimulatePosition(Vector3 initialPosition, Vector3 initialVelocity, float time, Vector3 gravity)
    {
        return initialPosition + initialVelocity * time + 0.5f * gravity * time * time;
    }
}