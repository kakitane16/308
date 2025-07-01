using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �I�����ꂽ�{�^����傫����ς���X�N���v�g�B
/// </summary>
public class UI_S_Button : MonoBehaviour
{
    [SerializeField] private Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1.2f); // �I����̃T�C�Y(�C���X�y�N�^�[�ŃT�C�Y��ς����悤��)
    [SerializeField] private float _scaleSpeed = 10f; // �傫�����ς�鑬�x(���炩�ɕς��悤��)
    [SerializeField] private float _pulse = 0.05f; // �g�k�̐U��
    [SerializeField] private float _pulseSpeed = 4f;     // �g�k�̑���
    // �����ɃV�[����������
    //[SerializeField] private string sceneToLoad;

    private float _pulseTimer = 0f;


    void Start()
    {
    }

    // =======�I�����ꂽ��g�k�ő傫����ς���========
    void Update()
    {
            _pulseTimer += Time.deltaTime * _pulseSpeed;
            float scaleOffset = Mathf.Sin(_pulseTimer) * _pulse;
            Vector3 pulsatingScale = _selectedScale + Vector3.one * scaleOffset;
            transform.localScale = Vector3.Lerp(transform.localScale, pulsatingScale, Time.deltaTime * _scaleSpeed);
    }

}
