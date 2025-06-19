using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeByMaterial : MonoBehaviour
{
    [Header("���肷��}�e���A���i���j")]
    public Material[] sourceMaterials;

    [Header("�u��������}�e���A���i��j")]
    public Material[] targetMaterials;

    [Header("�u�������郁�b�V���i��j")]
    public Mesh[] targetMeshes;

    [Header("SkinnedMeshRenderer ���g���ꍇ�̓`�F�b�N")]
    public bool applyToSkinned = false;

    private void OnTriggerEnter(Collider other)
    {
        // Renderer ���擾
        var rend = other.GetComponent<Renderer>();
        if (rend == null) return;

        // ���̃}�e���A����T��
        Material src = rend.sharedMaterial;
        int idx = System.Array.IndexOf(sourceMaterials, src);
        if (idx < 0) return; // �Ή��\�ɂȂ��}�e���A���Ȃ疳��

        // �}�e���A���������ւ�
        rend.material = targetMaterials[idx];

        // ���b�V���������ւ�
        if (applyToSkinned)
        {
            var smr = other.GetComponent<SkinnedMeshRenderer>();
            if (smr != null && idx < targetMeshes.Length)
                smr.sharedMesh = targetMeshes[idx];
        }
        else
        {
            var mf = other.GetComponent<MeshFilter>();
            if (mf != null && idx < targetMeshes.Length)
                mf.mesh = targetMeshes[idx];
        }
    }
}
