using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 적 프리팹
    public float spawnInterval = 2f; // 스폰 간격
    public int numberOfEnemiesToSpawn = 20; // 스폰할 적의 수

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // 일정 간격으로 적을 스폰하는 코루틴
    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            // 스폰 위치를 Enemy Spawner의 위치로 설정합니다.
            Vector3 spawnPosition = transform.position;
            // 스폰 위치에 적을 생성합니다.
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            // 다음 스폰을 위해 일정 시간을 기다립니다.
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
