using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 20; // 발사체 데미지

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // 충돌한 적의 EnemyHealth 컴포넌트를 가져옵니다.
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // 적에게 데미지를 가합니다.
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // 발사체를 소멸시킵니다.
        }
    }
}
