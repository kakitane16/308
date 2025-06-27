using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Fire : MonoBehaviour
{
    [Header("���肷�錳�̃}�e���A���i�G��Ă����I�u�W�F�N�g�����̃}�e���A���������Ă�����j")]
    public Material[] sourceMaterials;
    [Header("�u��������}�e���A���isourceMaterials �Ɠ����C���f�b�N�X�őΉ��t���j")]
    public Material[] targetMaterials;
    [Header("�u�������郁�b�V���isourceMaterials �Ɠ����C���f�b�N�X�őΉ��t���j")]
    public Mesh[] targetMeshes;
    public float G_Weight = 0.7f;

    private Buner burner; // �o�[�i�[�̎Q��

    void LateUpdate()
    {
        //if (burner == null)
        //    Destroy(gameObject); // �o�[�i�[���Ȃ��ꍇ�͎������g���폜
    }

    public void SetBuner(Buner b)
    {
        burner = b; // �o�[�i�[�̎Q�Ƃ�ݒ�
    }
    // ��L2�̓v���n�u����̐������ɃI�u�W�F�N�g���c���Ă��܂������������邽�߂Ɏg�p

    //�������o�[�i�[�ɂԂ����ė������Ɏ����ŌĂ΂��֐�
    //other�͂Ԃ����Ă�������Ⴆ�Ύ��i�Ƃ���\���Ă���
    public void OnTriggerEnter(Collider other)
    {
        //�G�ꂽ���肪���i���ǂ������^�O�Ń`�F�b�N������́A���̏ꍇ����Player���Y��
        if (other.CompareTag("Player"))
        {
            //����̓o�[�i�[�ɐG�ꂽ�u��(other)��ɂ���butnedMaterial(�Ă����f��)����
            //Renderer�����o����renderer�ɕύX�������
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer == null) return;
            // �G��Ă����I�u�W�F�N�g�������Ă���u���̃}�e���A���v�𒲂ׂ�
            Material currentMat = renderer.sharedMaterial;
            if (currentMat == null) return;

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb == null) return;
            // �d�ʂ̕ύX
            rb.mass = G_Weight;
            // sourceMaterials �̒����� currentMat �Ɠ������̂�T��
            int idx = System.Array.IndexOf(sourceMaterials, currentMat);
            if (idx < 0 || idx >= targetMaterials.Length || idx >= targetMeshes.Length)
            {
                // �Ή��\�ɂȂ��}�e���A���Ȃ牽�����Ȃ�
                return;
            }

            // �@ �}�e���A����u������
            renderer.material = targetMaterials[idx];
            // �A ���b�V����u������
            MeshFilter mf = other.GetComponent<MeshFilter>();
            if (mf != null)
            {
                mf.mesh = targetMeshes[idx];
            }
        }
    }
}
