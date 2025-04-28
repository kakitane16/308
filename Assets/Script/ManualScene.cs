using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualScene : MonoBehaviour
{
    public void ClickManualButton()
    {
        SceneManager.LoadScene("Manual");
    }
}
