using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buner : MonoBehaviour
{
    public GameObject G_Target; // �\���Ώ�
    public float G_Appear = 3.0f; // �o������
    public float G_Disappearance = 2.0f; // ���Ŏ���
    public float G_DistanceInRight = 2.0f;  // x�����ɏo������
    public float G_DistanceInUp = 2.0f;  // y�����ɏo������

    void Start()
    {
        Vector3 spawn = transform.position + 
           Vector3.up * G_DistanceInUp + transform.right * G_DistanceInRight;
        GameObject spawned = Instantiate(
            G_Target, spawn, Quaternion.identity);
        StartCoroutine(ToggleObject(spawned));   // �R���[�`���N��
        Collider collider = GetComponent<BoxCollider>();
        if (collider != null) { collider.isTrigger = true; } // IsTrigger���I��
    }
   
    IEnumerator ToggleObject(GameObject spawned)
    {
        while (true)
        {
            // �o��
            spawned.SetActive(true);
            yield return new WaitForSeconds(G_Appear);

            // ����
            spawned.SetActive(false);
            yield return new WaitForSeconds(G_Disappearance);
        }
    }
}
