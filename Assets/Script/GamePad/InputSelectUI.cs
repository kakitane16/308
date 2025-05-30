using UnityEngine;
using UnityEngine.SceneManagement;

public class InputSelectUI : MonoBehaviour
{
    public void SelectGamePad()
    {
        GameManager.Instance.inputDevice = InputObject.GamePad;
    }

    public void SelectKeyboard()
    {
        GameManager.Instance.inputDevice = InputObject.KeyBoad;
    }
}