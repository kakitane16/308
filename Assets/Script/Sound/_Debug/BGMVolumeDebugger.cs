using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMVolumeDebugger : MonoBehaviour
{
    [Tooltip("音量の増減ステップ量（0〜1）")]
    public float step = 0.05f;
            void Update()
    {
        if (BGMManager.Instance == null) return;

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            float newVolume = Mathf.Clamp(BGMManager.Instance.volume + step, 0f, 1f);
            BGMManager.Instance.SetVolume(newVolume);
            Debug.Log($"[BGMVolumeDebugger] 音量を上げました: {newVolume}");
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            float newVolume = Mathf.Clamp(BGMManager.Instance.volume - step, 0f, 1f);
            BGMManager.Instance.SetVolume(newVolume);
            Debug.Log($"[BGMVolumeDebugger] 音量を下げました: {newVolume}");
        }
    }
}
