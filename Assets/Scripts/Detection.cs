using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public GameObject projectilePrefab; // 발사체 프리팹
    public Transform firePoint; // 발사 위치
    public float projectileSpeed = 10f; // 발사체 속도

    private Animator animator;

    public float detectionRange = 2f; // 적 감지 범위
    public LayerMask enemyLayer; // 적 레이어

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ScanForEnemies());
    }

    // 2 범위 내의 적을 스캔하고 isAttacking을 변경하는 코루틴
    private IEnumerator ScanForEnemies()
    {
        while (true)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);

            if (hitEnemies.Length > 0)
            {
                // 범위 내에 적이 있으면 isAttacking을 true로 변경
                animator.SetBool("IsAttacking", true);
                // 발사체 발사
                ShootProjectile(hitEnemies[0].transform.position);
            }
            else
            {
                // 범위 내에 적이 없으면 isAttacking을 false로 변경
                animator.SetBool("IsAttacking", false);
            }

            yield return new WaitForSeconds(0.5f); // 일정 간격으로 스캔
        }
    }

    // 발사체를 생성하고 적에게 발사하는 메서드
    private void ShootProjectile(Vector3 targetPosition)
    {
        // 발사체를 생성합니다.
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        // 발사체의 방향을 적의 위치를 향해 설정합니다.
        Vector2 direction = ((Vector2)targetPosition - (Vector2)firePoint.position).normalized;
        // 발사체에 속도를 적용합니다.
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    // 디버깅을 위해 감지 범위를 그려주는 메서드
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
