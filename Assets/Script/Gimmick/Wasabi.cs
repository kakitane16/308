using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasabi : MonoBehaviour
{

  
    [Header("���肷�錳�̃}�e���A���i�G��Ă����I�u�W�F�N�g�����̃}�e���A���������Ă�����j")]
    public Material[] sourceMaterials;

    [Header("�u��������}�e���A���isourceMaterials �Ɠ����C���f�b�N�X�őΉ��t���j")]
    public Material[] targetMaterials;

    [Header("�u�������郁�b�V���isourceMaterials �Ɠ����C���f�b�N�X�őΉ��t���j")]
    public Mesh[] targetMeshes;

    [Header("SkinnedMeshRenderer ���g���ꍇ�̓`�F�b�N�iMeshFilter �� SkinnedMeshRenderer �̂ǂ��炩�ɍ��킹��j")]
    public bool applyToSkinned = false;

    public float G_Weight = 0.7f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // �G��Ă����I�u�W�F�N�g�� Renderer ���擾
        Renderer rend = other.GetComponent<Renderer>();
        if (rend == null) return;

        // �G��Ă����I�u�W�F�N�g�������Ă���u���̃}�e���A���v�𒲂ׂ�
        Material currentMat = rend.sharedMaterial;
        if (currentMat == null) return;


        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        // �d�ʂ̕ύX
        if (rb != null)
            rb.mass = G_Weight;

        // sourceMaterials �̒����� currentMat �Ɠ������̂�T��
        int idx = System.Array.IndexOf(sourceMaterials, currentMat);
        if (idx < 0 || idx >= targetMaterials.Length || idx >= targetMeshes.Length)
        {
            // �Ή��\�ɂȂ��}�e���A���Ȃ牽�����Ȃ�
            return;
        }

        // �@ �}�e���A����u������
        rend.material = targetMaterials[idx];

        // �A ���b�V����u������
        if (applyToSkinned)
        {
            // SkinnedMeshRenderer �p
            SkinnedMeshRenderer smr = other.GetComponent<SkinnedMeshRenderer>();
            if (smr != null)
            {
                smr.sharedMesh = targetMeshes[idx];
            }
        }
        else
        {
            // MeshFilter + MeshRenderer �p
            MeshFilter mf = other.GetComponent<MeshFilter>();
            if (mf != null)
            {
                mf.mesh = targetMeshes[idx];
            }
        }
    }
}