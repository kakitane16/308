using System;
using System.Collections;
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

    private List<Coroutine> dotCoroutines = new List<Coroutine>();
    public Color startColor = Color.white;
    public Color peakColor = Color.yellow;
    public Color endColor = Color.clear;
    public float dotLifeTime = 1.5f;  // ドットが表示されてから消えるまでの時間

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
            Vector3 pos = playerTransform.position + initialVel * t + 0.5f * gravity * t * t;

            GameObject dot = dots[i];
            dot.transform.position = pos;
            dot.SetActive(true);

            // 前のCoroutineが残っていたら止める
            if (dotCoroutines.Count > i && dotCoroutines[i] != null)
                StopCoroutine(dotCoroutines[i]);

            Coroutine c = StartCoroutine(AnimateDot(dot.GetComponent<Renderer>(),i));

            // Coroutineリストに保存
            if (dotCoroutines.Count > i)
            {
                dotCoroutines[i] = c;
            }
            else
            {
                dotCoroutines.Add(c);
            }
        }
    }

    public void HideDots()
    {
        foreach (var d in dots)
        {
            d.SetActive(false);
        }
    }

    // ドットの色をアニメーションさせてから非表示にする
    private IEnumerator AnimateDot(Renderer renderer, int index)
    {
        Material mat = new Material(renderer.material);
        renderer.material = mat;

        Color[] colors = new Color[] { Color.white, new Color(1f, 0.5f, 0.5f), Color.red, Color.white };
        int colorIndex = 0;
        float colorDuration = 0.5f;

        // indexに応じた開始ディレイ（番号が大きいほど遅れてスタート）
        float initialDelay = index * 0.1f;
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            Color from = colors[colorIndex % colors.Length];
            Color to = colors[(colorIndex + 1) % colors.Length];
            float t = 0f;

            while (t < colorDuration)
            {
                mat.color = Color.Lerp(from, to, t / colorDuration);
                t += Time.deltaTime;
                yield return null;
            }

            colorIndex++;
        }
    }
}