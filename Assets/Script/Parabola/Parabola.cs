using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    GameObject _netaPrefab;                        //�\������\�����邽�߂̃I�u�W�F�N�g
    private int SIMULATE_COUNT = 20;               //�\�������I�u�W�F�N�g��������܂ŕ\�����邩
    private Vector3 _startPosition;                //�ŏ��̃l�^�̈ʒu���擾
    private List<GameObject> _simuratePointList; �@//�V�~�����[�g����Q�[���I�u�W�F�N�g    
    // Start is called before the first frame update
    void Start()
    {
        //�V�~�����[�g�Ŏg���Ă���Q�[���I�u�W�F�N�g���폜
        if(_simuratePointList != null && _simuratePointList.Count > 0)
        {
            foreach(var go in _simuratePointList)
            {
                Destroy(go.gameObject);
            }
        }

        //�ʒu��\������I�u�W�F�N�g�����
        if (_netaPrefab != null)
        {
            _simuratePointList = new List<GameObject>();
            for(int i = 0;i < SIMULATE_COUNT;i++)
            {
                var go  = Instantiate(_netaPrefab);
                go.transform.SetParent(this.transform);
                go.transform.position = Vector3.zero;
                _simuratePointList.Add(go);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //��������`��
        if (_simuratePointList != null && _simuratePointList.Count > 0)
        {
            for (int i = 0; i < SIMULATE_COUNT; i++)
            {
                if(i == 0)
                {
                    Debug.DrawLine(_startPosition, _simuratePointList[i].transform.position);
                }
                else if(i < SIMULATE_COUNT)
                {
                    Debug.DrawLine(_simuratePointList[i - 1].transform.position, _simuratePointList[i].transform.position);
                }
            }
        }
    }

    public void Simulate(GameObject target, Vector3 _vec)
    {
        if (_simuratePointList != null && _simuratePointList.Count > 0)
        {
            // ���ˈʒu��ۑ�����
            _startPosition = target.transform.position;
            var r = target.GetComponent<Rigidbody>();
            if (r != null)
            {
                // �x�N�g����mass�Ŋ���
                Vector3 force = (_vec / r.mass);

                //�e���\���̈ʒu�ɓ_���ړ�
                for (int i = 0; i < SIMULATE_COUNT; i++)
                {
                    var t = (i * 0.5f); // 0.5�b���Ƃ̈ʒu��\���B
                    var x = t * force.x;
                    var y = (force.y * t) - 0.5f * (-Physics.gravity.y) * Mathf.Pow(t, 2.0f);
                    var z = t * force.z;

                    _simuratePointList[i].transform.position = _startPosition + new Vector3(x, y, z);
                }
            }
        }
    }
}
