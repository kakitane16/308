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

            Coroutine c = StartCoroutine(AnimateDot(dot.GetComponent<Renderer>(), i));

            // Coroutineリストに保存
            if (dotCoroutines.Count > i)
                dotCoroutines[i] = c;
            else
                dotCoroutines.Add(c);
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
        float delay = index * 0.05f; // ドットが順に光る演出（後のほど遅く）
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        Material mat = renderer.material;

        while (elapsed < dotLifeTime)
        {
            float t = elapsed / dotLifeTime;
            mat.color = Color.Lerp(startColor, peakColor, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mat.color = endColor;
        dots[index].SetActive(false);
    }
}