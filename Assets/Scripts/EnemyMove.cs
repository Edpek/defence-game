using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 몬스터 이동 속도
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 몬스터의 이동 방향을 아래로 설정합니다.
        rb.velocity = Vector2.down * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle1")) // 만약 충돌한 오브젝트의 태그가 "Obstacle1"이면
        {
            spriteRenderer.flipX = true;
            rb.velocity = Vector2.right * moveSpeed;
        }
        else if (collision.CompareTag("Obstacle2")) // 만약 충돌한 오브젝트의 태그가 "Obstacle1"이면
        {
            rb.velocity = Vector2.up * moveSpeed;
        }
        else if (collision.CompareTag("Obstacle3")) // 만약 충돌한 오브젝트의 태그가 "Obstacle1"이면
        {
            spriteRenderer.flipX = false;
            rb.velocity = Vector2.left * moveSpeed;
        }
        else if (collision.CompareTag("Obstacle4")) // 만약 충돌한 오브젝트의 태그가 "Obstacle1"이면
        {
            rb.velocity = Vector2.down * moveSpeed;
        }
    }
}
