using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEDataHolder : MonoBehaviour
{
    [System.Serializable]
    public class SEEntry
    {
        public string key;          // ��: "Jump"
        public AudioClip clip;      // �Ή�������ʉ�
    }

    [Tooltip("���̃I�u�W�F�N�g���g�p����SE�̈ꗗ")]
    public List<SEEntry> seList = new List<SEEntry>();

    private Dictionary<string, AudioClip> seMap;

    void Awake()
    {
        // key��clip �̎������\�z
        seMap = new Dictionary<string, AudioClip>();
        foreach (var entry in seList)
        {
            if (!seMap.ContainsKey(entry.key))
                seMap.Add(entry.key, entry.clip);
        }
    }

    public void PlaySE(string key)
    {
        if (seMap == null || !seMap.ContainsKey(key))
        {
            Debug.LogWarning($"[SEDataHolder] ���o�^��SE�L�[: {key}");
            return;
        }

        var clip = seMap[key];
        SEManager.Instance?.PlaySEAtPosition(clip, transform.position);
    }
}
