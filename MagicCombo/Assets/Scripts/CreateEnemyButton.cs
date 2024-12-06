using UnityEngine;

public class EnemySpawnerOnButtonPress : MonoBehaviour
{
    public GameObject enemyPrefab;      // ������ �����, ������� ����� ����������
    public Transform spawnPoint;        // �����, � ������� ����� ���������� ����
    public float respawnDelay = 0f;     // �������� ����� ������� (���� �����)
    private bool playerInZone = false;  // ����, ������������, ��� ����� � ���� ������

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ���� � ���� ������ �����
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ���������, ���� ����� ������� �� ����
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private void Update()
    {
        // ���� ����� � ���� � ������ ������������ (����� ��������� �� ������ �������)
        if (playerInZone && Input.GetKeyDown(KeyCode.E))  // ��������, ��� ������� ������� E
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            // ������� ����� � ��������� �����
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}