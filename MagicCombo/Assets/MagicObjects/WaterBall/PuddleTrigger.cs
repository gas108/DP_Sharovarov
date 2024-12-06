using UnityEngine;

public class PuddleTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что объект имеет тег "Enemy" и компонент EnemyStatus
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Получаем компонент EnemyStatus
            EnemyStatus enemyStatus = other.gameObject.GetComponent<EnemyStatus>();

            if (enemyStatus != null)
            {
                // Когда объект входит в лужу, применяем эффект воды
                enemyStatus.ApplyWaterEffect(); // Применение эффекта воды
            }
        }
    }
}

