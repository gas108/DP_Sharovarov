using UnityEngine;

public class EnemySpawnerOnButtonPress : MonoBehaviour
{
    public GameObject enemyPrefab;      // Префаб врага, который будет спавниться
    public Transform spawnPoint;        // Точка, в которой будет спавниться враг
    public float respawnDelay = 0f;     // Задержка перед спауном (если нужна)
    private bool playerInZone = false;  // Флаг, показывающий, что игрок в зоне кнопки

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, если в зону входит игрок
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Проверяем, если игрок выходит из зоны
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private void Update()
    {
        // Если игрок в зоне и кнопка активирована (можно настроить по вашему желанию)
        if (playerInZone && Input.GetKeyDown(KeyCode.E))  // Например, при нажатии клавиши E
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            // Спавним врага в указанной точке
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}