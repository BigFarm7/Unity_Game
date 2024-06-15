using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 태그가 "Wall"일 때 파편 효과 재생
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth _healthBar = collision.gameObject.GetComponentInChildren<PlayerHealth>();
            _healthBar.UpdateHealthBar(5, Vector3.up);


            // 파편 효과 생성 및 방향 설정

        }

        if (collision.gameObject.CompareTag("Tower"))
        {
            

            HealthBar _healthBar = collision.gameObject.GetComponentInChildren<HealthBar>();
            _healthBar.UpdateHealthBar(5, Vector3.up);


            // 파편 효과 생성 및 방향 설정
            
        }
        if (collision.gameObject.CompareTag("Lotte"))
        {

            PlayerHealth _healthBar = collision.gameObject.GetComponentInChildren<PlayerHealth>();
            _healthBar.UpdateHealthBar(5, Vector3.up);


            // 파편 효과 생성 및 방향 설정

        }
        GameObject[] effects = GameObject.FindGameObjectsWithTag("Effect");
        foreach (GameObject effect in effects)
        {
            Destroy(effect);
        }

        Destroy(gameObject);
    }
}
