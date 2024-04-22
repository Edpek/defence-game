using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // 최대 체력
    private int currentHealth; // 현재 체력

    private void Start()
    {
        currentHealth = maxHealth; // 시작할 때 최대 체력으로 초기화
    }

    // 발사체에 의한 데미지 처리 메서드
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // 데미지를 체력에서 빼기

        if (currentHealth <= 0)
        {
            Die(); // 체력이 0 이하면 사망 처리
        }
    }

    // 사망 처리 메서드
    private void Die()
    {
        // 적을 제거하거나 필요한 처리를 수행합니다.
        Destroy(gameObject);
    }
}
