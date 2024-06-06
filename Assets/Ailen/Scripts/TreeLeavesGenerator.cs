using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLeavesGenerator : MonoBehaviour
{
    public Mesh quad; // ���� �޽� (������ ����)
    public Material leafMaterial; // ������ ����
    public int leafCount = 5; // ������ ������ ����

    void Start()
    {
        // �׷����� ũ�� ��������
        Terrain terrain = Terrain.activeTerrain;
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;

        // �׷��� ���� ������ ��ġ�� ������ ����
        for (int i = 0; i < leafCount; i++)
        {
            // �׷��� �� ������ ��ġ ���
            float randomX = Random.Range(0f, terrainWidth);
            float randomZ = Random.Range(0f, terrainLength);
            Vector3 randomPosition = new Vector3(randomX, 0.01f, randomZ); // ���� ���� 0.01�� ����

            // ������ ����
            CreateLeaf(randomPosition);
        }
    }

    // ������ ���� �Լ�
    void CreateLeaf(Vector3 position)
    {
        // ������ ��ġ�� ���� ����
        GameObject leaf = new GameObject("Leaf");
        leaf.transform.position = position;
        leaf.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // ȸ���� ����

        // ���� �޽��� ������ ���� �Ҵ�
        MeshFilter meshFilter = leaf.AddComponent<MeshFilter>();
        meshFilter.mesh = quad;
        MeshRenderer meshRenderer = leaf.AddComponent<MeshRenderer>();
        meshRenderer.material = leafMaterial;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; // �׸��� ĳ���� off
    }

}
