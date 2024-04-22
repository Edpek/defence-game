using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Detection : MonoBehaviour
{
    public GameObject projectilePrefab; // 발사체 프리팹
    public Transform firePoint; // 발사 위치
    public float projectileSpeed = 10f; // 발사체 속도

    private Animator animator;

    public Vector2 detectionSize = new Vector2(4f, 2f); // 감지 범위 크기 (가로, 세로)
    public LayerMask enemyLayer; // 적 레이어

    private Transform targetEnemy; // 현재 공격 대상 적

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ScanForEnemies());
    }

    // 사각형 범위 내의 적을 스캔하고 isAttacking을 변경하는 코루틴
    private IEnumerator ScanForEnemies()
    {
        while (true)
        {
            // 사각형 영역 내에 있는 적을 검출합니다.
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.position, detectionSize, 0f, enemyLayer);

            if (hitEnemies.Length > 0)
            {
                // 범위 내에 적이 있으면 isAttacking을 true로 변경
                animator.SetBool("IsAttacking", true);

                // 현재 공격 대상이 감지 범위를 벗어나거나 새로운 공격 대상이 필요한 경우에만 선택합니다.
                if (targetEnemy == null || !Array.Exists(hitEnemies, enemyCollider => enemyCollider.transform == targetEnemy))
                {
                    // 가장 가까운 적을 선택합니다.
                    targetEnemy = GetClosestEnemy(hitEnemies);
                }

                // 발사체 발사
                if (targetEnemy != null)
                {
                    Vector3 enemyPosition = targetEnemy.position;
                    Vector2 direction = ((Vector2)enemyPosition - (Vector2)firePoint.position).normalized;
                    ShootProjectile(enemyPosition, direction);
                }
            }
            else
            {
                // 범위 내에 적이 없으면 isAttacking을 false로 변경
                animator.SetBool("IsAttacking", false);
            }

            yield return new WaitForSeconds(0.5f); // 일정 간격으로 스캔
        }
    }

    // 가장 가까운 적을 찾아서 반환하는 메서드
    private Transform GetClosestEnemy(Collider2D[] enemies)
    {
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    // 발사체를 생성하고 적에게 발사하는 메서드
    private void ShootProjectile(Vector3 targetPosition, Vector2 direction)
    {
        // 발사체를 생성합니다.
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        // 발사체의 방향을 적의 위치를 향해 설정합니다.
        projectile.transform.up = direction;
        // 발사체에 속도를 적용합니다.
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    // 디버깅을 위해 감지 범위를 그려주는 메서드
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // 감지 범위를 사각형 모양으로 그립니다.
        Gizmos.DrawWireCube(transform.position, detectionSize);
    }
}
