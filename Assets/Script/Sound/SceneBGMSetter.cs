using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBGMSetter : MonoBehaviour
{
    public AudioClip bgmClip;
    public float fadeDuration = 1.0f;

    void Start()
    {
        if (BGMManager.Instance != null)
        {
            StartCoroutine(BGMManager.Instance.FadeIn(bgmClip, fadeDuration));
        }
    }
}
