using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChange_Only : MonoBehaviour
{

    public Material ChangeMaterial;  // player�̃}�e���A����ύX����@
    private bool Check = false; // �M�~�b�N���g�p���ꂽ��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // ��������Object�̃^�O��Player�ł͂Ȃ��Ȃ珈�������Ȃ�
        if (other.gameObject.tag != "Player") { return; }

        Renderer playerren = other.GetComponent<Renderer>();
        Player player = other.GetComponent<Player>();

        // ��񂪂Ȃ��Ȃ珈�������Ȃ�
        if (playerren == null || player == null) { return; }

        // Player�̃}�e���A����ς���
        playerren.material = ChangeMaterial;
        this.Check = true;

        // ��x�M�~�b�N���g��ꂽ��
        if (!Check) { return; }
        // ���̃I�u�W�F�N�g������
        Destroy(this.gameObject);
    }
}
