using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class TitleChange : MonoBehaviour
{

    public void ClickStartButton()
    {
        SceneManager.LoadScene("Select");
    }
}
