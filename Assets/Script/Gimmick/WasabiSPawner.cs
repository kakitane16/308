using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasabiSPawner : MonoBehaviour
{
    //Inspector �̊Y���t�B�[���h�̏�� "�f���o�����T�r�̃v���n�u" �Ƃ������o���i���₷�����x���j��\��
    [Header("�f���o�����T�r�̃v���n�u")]
    public GameObject wasabiPrefab;
    //Inspector��� "���T�r���o��ʒu" �Ƃ������o����\��
    [Header("���T�r���o��ʒu")]
    //Inspector �Ɂu�h���b�O���h���b�v�\�ȃX���b�g�v�������̂ŁA
    //�����Ɂu�f���o���ʒu��������� GameObject�v���A�T�C��
    public Transform spawnPoint;
    public Vector3 offset = new Vector3();
    public Vector3 offset_rotate = new Vector3();

    [Header("���T�r���������Ă��������܂ł̎��� (�b)")]
    //�u���T�r�����܂�Ă��玩���ŏ�����܂ł̕b���v��\��
    //inspector�ŕb���ύX�\
    public float lifeTime = 3f;

    private void Start()
    {
        //�N���Ɠ����Ƀ��[�v���J�n
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            // ���O�F���b���_�œf���o���n�߂邩
            Debug.Log($"[WasabiSpawner] SpawnLoop: Spawning wasabi at time {Time.time:F2}");
            //���T�r��f���o��

            Quaternion rot = Quaternion.Euler(spawnPoint.rotation.eulerAngles + offset_rotate);
            //Instantiate �� ���T�r����
            GameObject w = Instantiate(wasabiPrefab
                                       , spawnPoint.position + offset
                                       , rot);
            // ���O�F�������ꂽ�C���X�^���X��
            Debug.Log($"[WasabiSpawner] Spawned: {w.name}");
            //lifeTime���b�Ɏ����ŏ���
            Destroy(w, lifeTime);

            //�w�莞�Ԃ������ꎞ��~�A���𓯂��Ԋu�ŌJ��Ԃ�
            yield return new WaitForSeconds(lifeTime);
        }
    }
}
