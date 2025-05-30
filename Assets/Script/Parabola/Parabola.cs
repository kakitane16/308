using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    Player player;
    public GameObject prefab;                 // �z�u�������I�u�W�F�N�g�i�v���n�u�j
    public int count = 10;                    // �z�u�����
    public float timeStep = 0.1f;             // �V�~�����[�V�����̎��ԃX�e�b�v�i���Ԋu�j
    public Vector3 gravity = new Vector3(0, -9.81f, 0); // �d��

    private Vector3 launchVelocity;
    private List<GameObject> markers = new List<GameObject>();

    // �O��̓��͒l
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

        // ����Ɉ�x�X�V
        UpdateMarkers();
    }

    void Update()
    {
        if (player == null) return;

        // �� or �p�x���ς���������m�F
        if (player.forceStrength != prevForceStrength || player.SAngleY != prevAngleY)
        {
            UpdateMarkers();
        }
    }

    void UpdateMarkers()
    {
        if (player == null || markers.Count == 0) return;

        Vector3 startPos = player.transform.position;

        // Y���̊p�x�����W�A���ɕϊ����ĕ����������ɔ��f�i��: ��ɑł��グ��p�x�j
        float angleRad = player.SAngleY * Mathf.Deg2Rad;
        Vector3 direction = player.transform.forward;
        launchVelocity = Quaternion.AngleAxis(player.SAngleY, player.transform.right) * direction * player.forceStrength;

        for (int i = 0; i < count; i++)
        {
            float time = timeStep * i;
            Vector3 pos = SimulatePosition(startPos, launchVelocity, time, gravity);
            markers[i].transform.position = pos;
        }

        // ���݂̐ݒ��ۑ�
        prevForceStrength = player.forceStrength;
        prevAngleY = player.SAngleY;
    }

    // �������̈ʒu���v�Z
    Vector3 SimulatePosition(Vector3 initialPosition, Vector3 initialVelocity, float time, Vector3 gravity)
    {
        return initialPosition + initialVelocity * time + 0.5f * gravity * time * time;
    }
}