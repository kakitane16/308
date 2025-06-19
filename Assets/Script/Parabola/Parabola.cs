using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    public GameObject dotPrefab;         // 丸のプレハブ
    public int dotCount = 30;            // 表示するドット数
    public float dotSpacing = 0.1f;      // ドット間の時間間隔
    public Transform playerTransform;   // PlayerのTransform参照（位置、向き）
    public Rigidbody playerRb;           // PlayerのRigidbody（初速の取得に使う）

    private List<GameObject> dots = new List<GameObject>();
    public Vector3 initialVelocity;

    private void OnEnable()
    {
        // ドットを生成して非アクティブにする（初期化）
        for (int i = 0; i < dotCount; i++)
        {
            GameObject dot = Instantiate(dotPrefab);
            dot.SetActive(false);
            dots.Add(dot);
        }
    }

    public void ShowParabora()
    {
        initialVelocity = GetInitialVelocity();
        ShowPredictionDots(initialVelocity);
    }

    // ここでは仮の処理として初速を計算
    private Vector3 GetInitialVelocity()
    {
        // 例：Playerの向き（forward）にforceStrengthの力をかける + 上方向にSAngleYの高さで調整
        float forceStrength = 0f;
        float angleY = 0f;

        // ここではPlayerのコンポーネントから取得を例示
        Player player = playerTransform.GetComponent<Player>();
        if (player != null)
        {
            forceStrength = player.forceStrength;
            angleY = player.SAngleY;
        }

        Vector3 forward = playerTransform.forward;
        Vector3 velocity = forward * forceStrength + Vector3.up * angleY;
        return velocity;
    }

    private void ShowPredictionDots(Vector3 initialVel)
    {
        // 物理の重力加速度（Unityのデフォルト）
        Vector3 gravity = Physics.gravity;

        for (int i = 0; i < dotCount; i++)
        {
            float t = i * dotSpacing;

            // 弾道公式：位置 = 初速 * t + 0.5 * 重力 * t^2 + 発射地点
            Vector3 pos = playerTransform.position + initialVel * t + 0.5f * gravity * t * t;

            dots[i].SetActive(true);
            dots[i].transform.position = pos;
        }
    }

    public void HideDots()
    {
        foreach (var d in dots)
        {
            d.SetActive(false);
        }
    }
}