using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInitializer : MonoBehaviour
{
    void Awake()
    {
        if (BGMManager.Instance == null)
        {
            GameObject bgmObject = new GameObject("BGMManager");
            bgmObject.AddComponent<BGMManager>();
        }
        if (SEManager.Instance == null)
        {
            GameObject seObject = new GameObject("SEManager");
            seObject.AddComponent<SEManager>();
        }
    }
}
