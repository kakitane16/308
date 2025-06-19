using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBGMSwitcher : MonoBehaviour
{
    private SceneBGMSetter sceneBGMSetter;

    public AudioClip[] resultBGMClips; // BGM�N���b�v�̔z��

    // Start is called before the first frame update
    void Start()
    {
        sceneBGMSetter = GetComponent<SceneBGMSetter>();
        if (sceneBGMSetter == null)
        {
            Debug.LogError("SceneBGMSetter�R���|�[�l���g��������܂���B");
            return;
        }
        // �X�R�A�ɉ�����BGM��؂�ւ���
        int score = GameManager.Instance.score; // GameManager����X�R�A���擾

        sceneBGMSetter.bgmClip = resultBGMClips[score]; // �X�R�A�ɉ�����BGM��ݒ�

        sceneBGMSetter.enabled = true; // SceneBGMSetter��L���ɂ���BGM���Đ�
    }
}
