using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 選択されたボタンを大きさを変えるスクリプト。
/// </summary>
public class UI_S_Button : MonoBehaviour
{
    [SerializeField] private Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1.2f); // 選択後のサイズ(インスペクターでサイズを変えれるように)
    [SerializeField] private float _scaleSpeed = 10f; // 大きさが変わる速度(滑らかに変わるように)
    [SerializeField] private float _pulse = 0.05f; // 拡縮の振幅
    [SerializeField] private float _pulseSpeed = 4f;     // 拡縮の速さ
    // ここにシーン名を入れる
    //[SerializeField] private string sceneToLoad;

    private float _pulseTimer = 0f;


    void Start()
    {
    }

    // =======選択されたら拡縮で大きさを変える========
    void Update()
    {
            _pulseTimer += Time.deltaTime * _pulseSpeed;
            float scaleOffset = Mathf.Sin(_pulseTimer) * _pulse;
            Vector3 pulsatingScale = _selectedScale + Vector3.one * scaleOffset;
            transform.localScale = Vector3.Lerp(transform.localScale, pulsatingScale, Time.deltaTime * _scaleSpeed);
    }

}
