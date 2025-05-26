using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Wasabi : MonoBehaviour
{
    [Header("���T�r���f���i�v���n�u�j���R��ސݒ�")]
    public Material Sushi;
    public Mesh SushiMesh;

    void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Wasabi] OnTriggerEnter: other.tag={other.tag}, this={gameObject.name}");
        if (!other.CompareTag("Player")) return;
        Debug.Log("[Wasabi] �G�ꂽ����� Player �ł��I");
        // �G��Ă����I�u�W�F�N�g��renderer���擾
        Renderer renderer = other.gameObject.GetComponent<Renderer>();
        // �G��Ă����I�u�W�F�N�g��MeshFilter���擾
        MeshFilter meshfilter = other.gameObject.GetComponent<MeshFilter>();

        renderer.material = Sushi;
        meshfilter.mesh = SushiMesh;
    }
}