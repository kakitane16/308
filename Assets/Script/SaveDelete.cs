using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveDelete : MonoBehaviour
{
    private GamePadCommand command;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha3) &&
        Input.GetKey(KeyCode.Alpha8) &&
        Input.GetKey(KeyCode.Alpha0))
        {
            GameManager.Instance.DeleteClearState();
        }
    }
}
