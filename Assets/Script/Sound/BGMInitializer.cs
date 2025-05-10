using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMInitializer : MonoBehaviour
{
    void Awake()
    {
        if (BGMManager.Instance == null)
        {
            GameObject bgmObject = new GameObject("BGMManager");
            bgmObject.AddComponent<BGMManager>();
        }
    }
}
