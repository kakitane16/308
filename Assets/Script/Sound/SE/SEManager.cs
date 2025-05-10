using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance;

    [Tooltip("�����Đ��ł���SE�̍ő吔")]
    public int poolSize = 10;

    [Range(0f, 1f)]
    public float seVolume = 1f;

    private List<AudioSource> audioSources;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitPool()
    {
        audioSources = new List<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 0f; // 2D���B3D�ɂ��������1�ɕύX
            audioSources.Add(source);
        }
    }

    public void PlaySEAtPosition(AudioClip clip, Vector3 position)
    {
        if (clip == null)
        {
            Debug.LogWarning("[SEManager] AudioClip��null�ł�");
            return;
        }

        AudioSource.PlayClipAtPoint(clip, position, seVolume);
    }

    public void SetVolume(float volume)
    {
        seVolume = Mathf.Clamp01(volume);
    }
}
