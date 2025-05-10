using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SETest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.shiftKey.isPressed && Keyboard.current.altKey.wasPressedThisFrame)
            GetComponent<SEDataHolder>()?.PlaySE("Shift+Alt‚ª‰Ÿ‚³‚ê‚½");
    }
}
