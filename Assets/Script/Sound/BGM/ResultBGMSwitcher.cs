using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBGMSwitcher : MonoBehaviour
{
    private SceneBGMSetter sceneBGMSetter;

    public AudioClip[] resultBGMClips; // BGMクリップの配列

    // Start is called before the first frame update
    void Start()
    {
        sceneBGMSetter = GetComponent<SceneBGMSetter>();
        if (sceneBGMSetter == null)
        {
            Debug.LogError("SceneBGMSetterコンポーネントが見つかりません。");
            return;
        }
        // スコアに応じてBGMを切り替える
        int score = GameManager.Instance.score; // GameManagerからスコアを取得

        sceneBGMSetter.bgmClip = resultBGMClips[score]; // スコアに応じたBGMを設定

        sceneBGMSetter.enabled = true; // SceneBGMSetterを有効にしてBGMを再生
    }
}
