using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buner : MonoBehaviour
{
    public GameObject G_Target; // �\���Ώ�
    public float G_Appear = 3.0f; // �o������
    public Vector3 G_Trans = new Vector3(); // ���̒���
    public Vector3 G_Offset = new Vector3(); // ���̒���
    public float G_Disappearance = 2.0f; // ���Ŏ���
    public float G_DistanceInRight = 2.0f;  // x�����ɏo������
    public float G_DistanceInUp = 2.0f;  // y�����ɏo������

    void Start()
    {
        if (G_Target == null) { return; }
        // �ʒu�ύX
        Vector3 spawn = transform.position + G_Offset;
        GameObject spawned = Instantiate(
            G_Target, spawn, Quaternion.identity);
        // �傫���w��
        spawned.transform.localScale = G_Trans;

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
