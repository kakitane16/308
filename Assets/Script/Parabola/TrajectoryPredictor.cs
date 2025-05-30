using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    public Transform target;       // PlayerのTransform（打ち出し位置）
    public Rigidbody rb;           // PlayerのRigidbody（質量、重力取得用）
    public int pointCount = 10;    // 表示する点の数
    public float timeStep = 0.1f;  // 点を出す時間間隔

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointCount;
    }

    public void ShowTrajectory(Vector3 initialVelocity)
    {
        Vector3 gravity = Physics.gravity;
        Vector3 currentPosition = target.position;

        for (int i = 0; i < pointCount; i++)
        {
            float time = i * timeStep;
            Vector3 pos = currentPosition + initialVelocity * time + 0.5f * gravity * time * time;
            lineRenderer.SetPosition(i, pos);
        }
    }

    public void Hide()
    {
        lineRenderer.positionCount = 0;
    }
}