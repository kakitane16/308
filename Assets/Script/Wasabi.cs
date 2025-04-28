using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasabi : MonoBehaviour
{
    //�v���C���[�����T�r�ɐG�ꂽ�火�̎O�i�K���̃}�e���A���ɕς��
    //Material�𑽂��g�����߈ȉ�����Material��Mat�ɂ���
    public Material LargeMat;   //���T�r�̏�����ԃv���C���[���܂��G��Ă��Ȃ��iLarge���傫���j
    public Material MediumMat;  //�v���C���[�����G��ď�����������ԁiMedium�����j
    public Material SmallMat;   //�v���C���[�����G��Ă����Ԍ�������ԁismall�����ʁj

    private int level = 3;      //���T�r�̎c�ʂ����x���ŊǗ�(3��large, 2��medium, 1��small, 0������)
    private Renderer rend;      //Renderer��rend
    private Collider col;       //Collider��col

    //Awake(�N����A�ڊo�߂�)�Q�[���I�u�W�F�N�g���V�[���ɓǂݍ��܂�A
    //�A�N�e�B�u�ɂȂ����u�ԂɈ�x�����Ă΂�鏉�����p���\�b�h
    void Awake()
    {
        //�����g���Ό�ɒ��ڌ����ڂ�ύX�ł���悤�ɂȂ�
        rend = GetComponent<Renderer>();
        //�����T�r���g���؂�ꂽ�������蔻��𖳌���������ォ�画��̗L����؂�ւ�����
        col = GetComponent<Collider>();
        //UpdateVisual() ���Ăяo���āA�Q�[���J�n���̃��T�r�̌����ڂ𐳂�����ԂɃZ�b�g
        //level�ɉ����ăZ�b�g����}�e���A�����Ăяo��
        //���T�r�̏�Ԃ��ŏ�����LargeMat�ɂ���
        rend.material = LargeMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Player�̃^�O���t���Ă����ȊO�G��Ă����s����Ȃ�
        if (!other.CompareTag("Player")) { return; }

        // �A �G�ꂽ�v���C���[�� Renderer ������Ă��āc 
        Renderer playerRend = other.GetComponent<Renderer>();
        Player player = other.GetComponent<Player>();
        if (playerRend == null) return;

        //�c�ʂ�����Έ�i�K���炷
        //�c��(level)2����3�̎��܂Ō��炷
        if (level > 1)
        {
            level--;
            //���̃��x���ɑΉ������}�e���A���ɐ؂�ւ�
            UpdeteVisual(playerRend);

        }
        else if (level == 1)
        {
            //�c�ʂ����傤�ǂP�̎��i���ʁj
            level = 0;               //�c�ʂO�ɐݒ�
            UpdeteVisual(playerRend);          //�����_�\���I�t��
            col.enabled = false;     //Colleder�𖳌���,�ȍ~�v���C���[���G��Ă��������Ȃ�
            Destroy(gameObject);     //�����ڂ����S�ɍ폜

        }

    }

    private void UpdeteVisual(Renderer PRend)
    {
        //level�̒l�����ƂɊY������Case�̃u���b�N���̏������������s����
        //switch���g���̂�if�celse if�celse �����x���������
        //�u�l�����ꂱ��̂Ƃ��͂��������v�Ƃ��������̕�����������菑���邩��
        switch (level)
        {
            case 3:
                rend.material = LargeMat;
                //break�ɂ����switch���𔲂��邽�߉��̃v���O����������Ď��s����Ȃ�
                break;

            case 2:
                rend.material = MediumMat;
                PRend.material = LargeMat;
                break;

            case 1:
                rend.material = SmallMat;
                PRend.material = MediumMat;
                break;
            //���T�r�����S�Ɏg���؂�ꂽ���
            case 0:
                PRend.material = SmallMat;
                //�}�e���A����؂�ւ������Ƀ����_�\���̂��I�t�ɂ��Č����Ȃ�����
                //enabled���L���ɂȂ��Ă��邻����I�t
                rend.enabled = false;
                break;
        }

    }
}
