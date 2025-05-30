using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    public Transform target;       // Player��Transform�i�ł��o���ʒu�j
    public Rigidbody rb;           // Player��Rigidbody�i���ʁA�d�͎擾�p�j
    public int pointCount = 10;    // �\������_�̐�
    public float timeStep = 0.1f;  // �_���o�����ԊԊu

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