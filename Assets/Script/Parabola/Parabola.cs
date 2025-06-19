using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    public GameObject dotPrefab;         // �ۂ̃v���n�u
    public int dotCount = 30;            // �\������h�b�g��
    public float dotSpacing = 0.1f;      // �h�b�g�Ԃ̎��ԊԊu
    public Transform playerTransform;   // Player��Transform�Q�Ɓi�ʒu�A�����j
    public Rigidbody playerRb;           // Player��Rigidbody�i�����̎擾�Ɏg���j

    private List<GameObject> dots = new List<GameObject>();
    public Vector3 initialVelocity;

    private void OnEnable()
    {
        // �h�b�g�𐶐����Ĕ�A�N�e�B�u�ɂ���i�������j
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

    // �����ł͉��̏����Ƃ��ď������v�Z
    private Vector3 GetInitialVelocity()
    {
        // ��FPlayer�̌����iforward�j��forceStrength�̗͂������� + �������SAngleY�̍����Œ���
        float forceStrength = 0f;
        float angleY = 0f;

        // �����ł�Player�̃R���|�[�l���g����擾��Ꭶ
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
        // �����̏d�͉����x�iUnity�̃f�t�H���g�j
        Vector3 gravity = Physics.gravity;

        for (int i = 0; i < dotCount; i++)
        {
            float t = i * dotSpacing;

            // �e�������F�ʒu = ���� * t + 0.5 * �d�� * t^2 + ���˒n�_
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