using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public GameObject impactEffectPrefab; // 충돌 시 파편 효과 프리팹

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 태그가 "Wall"일 때 파편 효과 재생
        if (collision.gameObject.CompareTag("Wall"))
        {
            ContactPoint contact = collision.contacts[0]; // 충돌 지점 정보 가져오기
            Vector3 impactDirection = contact.normal; // 충돌 지점의 법선 벡터의 반대 방향

            GameObject[] effects = GameObject.FindGameObjectsWithTag("Effect");
            foreach (GameObject effect in effects)
            {
                Destroy(effect);
            }

            // 파편 효과 생성 및 방향 설정
            GameObject impactEffect = Instantiate(impactEffectPrefab, contact.point, Quaternion.identity);
            impactEffect.transform.forward = impactDirection;
        }
       

        Destroy(gameObject);
    }
    

}
