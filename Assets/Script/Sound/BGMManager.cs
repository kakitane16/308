using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    private AudioSource audioSource;
    public float volume = 1f;
    private string currentBGMName = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f;
            audioSource.volume = volume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        audioSource.volume = volume;
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null || audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
    }

    public IEnumerator FadeIn(AudioClip clip, float duration = 1.0f)
    {
        PlayBGM(clip);
        audioSource.volume = 0;
        float time = 0f;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(0, volume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = volume;
    }

    public IEnumerator FadeOut(float duration = 1.0f)
    {
        float startVolume = audioSource.volume;
        float time = 0f;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
    }
}
