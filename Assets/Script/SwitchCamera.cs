using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _subCamera;
    // Update is called once per frame
    private void Start()
    {
        //ゲーム開始時はサブカメラをオフにしておく
        _subCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //メインカメラとサブカメラを切り替える
            _mainCamera.gameObject.SetActive(!_mainCamera.gameObject.activeSelf);
            _subCamera.gameObject.SetActive(!_subCamera.gameObject.activeSelf);
        }
    }
}
