using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEVolumeDebugger : MonoBehaviour
{
    [Tooltip("音量の増減ステップ量（0〜1）")]
    public float step = 0.05f;
            void Update()
    {
        if (SEManager.Instance == null) return;

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            float newVolume = Mathf.Clamp(SEManager.Instance.seVolume + step, 0f, 1f);
            SEManager.Instance.SetVolume(newVolume);
            Debug.Log($"[SEVolumeDebugger] 音量を上げました: {newVolume}");
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            float newVolume = Mathf.Clamp(SEManager.Instance.seVolume - step, 0f, 1f);
            SEManager.Instance.SetVolume(newVolume);
            Debug.Log($"[SEVolumeDebugger] 音量を下げました: {newVolume}");
        }
    }
}
