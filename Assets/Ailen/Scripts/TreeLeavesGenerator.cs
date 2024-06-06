using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLeavesGenerator : MonoBehaviour
{
    public Mesh quad; // 쿼드 메쉬 (나뭇잎 형태)
    public Material leafMaterial; // 나뭇잎 재질
    public int leafCount = 5; // 생성할 나뭇잎 개수

    void Start()
    {
        // 테레인의 크기 가져오기
        Terrain terrain = Terrain.activeTerrain;
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;

        // 테레인 위에 랜덤한 위치에 나뭇잎 생성
        for (int i = 0; i < leafCount; i++)
        {
            // 테레인 위 랜덤한 위치 계산
            float randomX = Random.Range(0f, terrainWidth);
            float randomZ = Random.Range(0f, terrainLength);
            Vector3 randomPosition = new Vector3(randomX, 0.01f, randomZ); // 높이 값을 0.01로 설정

            // 나뭇잎 생성
            CreateLeaf(randomPosition);
        }
    }

    // 나뭇잎 생성 함수
    void CreateLeaf(Vector3 position)
    {
        // 나뭇잎 위치에 쿼드 생성
        GameObject leaf = new GameObject("Leaf");
        leaf.transform.position = position;
        leaf.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // 회전값 설정

        // 쿼드 메쉬와 나뭇잎 재질 할당
        MeshFilter meshFilter = leaf.AddComponent<MeshFilter>();
        meshFilter.mesh = quad;
        MeshRenderer meshRenderer = leaf.AddComponent<MeshRenderer>();
        meshRenderer.material = leafMaterial;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; // 그림자 캐스팅 off
    }

}
