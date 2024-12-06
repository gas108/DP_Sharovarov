using UnityEngine;

public class PuddleTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ��� ������ ����� ��� "Enemy" � ��������� EnemyStatus
        if (other.gameObject.CompareTag("Enemy"))
        {
            // �������� ��������� EnemyStatus
            EnemyStatus enemyStatus = other.gameObject.GetComponent<EnemyStatus>();

            if (enemyStatus != null)
            {
                // ����� ������ ������ � ����, ��������� ������ ����
                enemyStatus.ApplyWaterEffect(); // ���������� ������� ����
            }
        }
    }
}

