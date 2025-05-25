using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    GameObject _netaPrefab;                        //予測線を表示するためのオブジェクト
    private int SIMULATE_COUNT = 20;               //表示したオブジェクトをいくつ先まで表示するか
    private Vector3 _startPosition;                //最初のネタの位置を取得
    private List<GameObject> _simuratePointList; 　//シミュレートするゲームオブジェクト    
    // Start is called before the first frame update
    void Start()
    {
        //シミュレートで使われているゲームオブジェクトを削除
        if(_simuratePointList != null && _simuratePointList.Count > 0)
        {
            foreach(var go in _simuratePointList)
            {
                Destroy(go.gameObject);
            }
        }

        //位置を表示するオブジェクトを作る
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
        //放物線を描く
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
            // 発射位置を保存する
            _startPosition = target.transform.position;
            var r = target.GetComponent<Rigidbody>();
            if (r != null)
            {
                // ベクトルはmassで割る
                Vector3 force = (_vec / r.mass);

                //弾道予測の位置に点を移動
                for (int i = 0; i < SIMULATE_COUNT; i++)
                {
                    var t = (i * 0.5f); // 0.5秒ごとの位置を予測。
                    var x = t * force.x;
                    var y = (force.y * t) - 0.5f * (-Physics.gravity.y) * Mathf.Pow(t, 2.0f);
                    var z = t * force.z;

                    _simuratePointList[i].transform.position = _startPosition + new Vector3(x, y, z);
                }
            }
        }
    }
}
