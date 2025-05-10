using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEDataHolder : MonoBehaviour
{
    [System.Serializable]
    public class SEEntry
    {
        public string key;          // 例: "Jump"
        public AudioClip clip;      // 対応する効果音
    }

    [Tooltip("このオブジェクトが使用するSEの一覧")]
    public List<SEEntry> seList = new List<SEEntry>();

    private Dictionary<string, AudioClip> seMap;

    void Awake()
    {
        // key→clip の辞書を構築
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
            Debug.LogWarning($"[SEDataHolder] 未登録のSEキー: {key}");
            return;
        }

        var clip = seMap[key];
        SEManager.Instance?.PlaySEAtPosition(clip, transform.position);
    }
}
