using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BackTitle : MonoBehaviour
{
    GamePadCommand command;
    public void Start()
    {
        command = new GamePadCommand();
    }
    public void Update()
    {
        command = new GamePadCommand();
        if (command.GetEscKey((int)GameManager.Instance.inputDevice))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
