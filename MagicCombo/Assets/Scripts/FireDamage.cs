using UnityEngine;
using System.Collections;

public class FireDamage : MonoBehaviour
{
    public int damagePerSecond = 1;
    public float duration = 5f;

    private Health health;
    private bool isBurning = false;
    private Coroutine burnCoroutine;

    // Добавляем ссылку на EnemyStatus
    private EnemyStatus enemyStatus;

    void Start()
    {
        health = GetComponent<Health>();
        enemyStatus = GetComponent<EnemyStatus>(); // Получаем компонент EnemyStatus

        if (health == null)
        {
            Debug.LogError("На объекте нет компонента Health.");
        }

        if (enemyStatus == null)
        {
            Debug.LogError("На объекте нет компонента EnemyStatus.");
        }
    }

    public void ApplyFireDamage()
    {
        if (!isBurning && health != null)
        {
            isBurning = true;
            burnCoroutine = StartCoroutine(Burn());
        }
    }

    public void StopFireDamage()
    {
        if (isBurning)
        {
            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
            }
            isBurning = false;
        }
    }

    private IEnumerator Burn()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            // Проверяем текущее состояние из EnemyStatus
            if (enemyStatus.currentState != EnemyState.Burning)
            {
                Debug.Log("Горение прекращено, так как статус изменился.");
                break; // Прерываем корутину, если статус больше не Burning
            }

            health.TakeDamage(damagePerSecond);
            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
        isBurning = false;
    }
}