using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputObject inputDevice = InputObject.GamePad; //‰Šú‚Í‚È‚É‚àÚ‘±‚³‚ê‚Ä‚È‚¢
    public int score = 0; //“_”‚ğ•Ô‚·

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
