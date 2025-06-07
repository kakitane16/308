using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Fire : MonoBehaviour
{
    //��Ă��������ڂ̑f��(�}�e���A��)��Inspector�Őݒ�ł���悤�Ɍ��J���Ă镨
    //unity�G�f�B�^�[��Ŏ��i���Ă����肷�鎞�ɕ\�����������}�e���A���������Ƀh���b�N���h���b�v���邾��
    public Color G_Color = new Color(0.0f, 0.0f, 0.0f);       // �Ă��F�}�e���A��
    public float G_Weight = 0.7f;

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
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            // �d�ʂ̕ύX
            if (rb != null)
                rb.mass = G_Weight;
            //�����ڂ�ς���O�ɁA�K�v�Ȃ��̂������Ƃ��邩�m�F���Ă��鍀��
            //&&�͗�����������true(����)�̂Ƃ��������̏��������s����Ƃ����Ӗ�
            if (renderer != null && G_Color != null )
            {
                renderer.material.color = G_Color;
                //this.burnCount++; //�G�ꂽ��J�E���g���オ��

                //if(this.burnCount == 1) 
                //{
                //    //�ǂ���������Ƃ���Ƃ������Ă��������ڂɂȂ�butnedMaterial�����s�ł���
                //    //���ڂ̏Ă��F
                //    renderer.material = butnedMaterial;
                //}
                //else if(this.burnCount == 2) 
                //{
                //    //���ڂ̏ł��F
                //    renderer.material = OverButnedMaterial;
                //}
                //else
                //{
                //    //�O��ڈȍ~�������Ȃ����ߏ����Ȃ�
                //}
            }
        }
    }
}
