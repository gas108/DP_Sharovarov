using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    public float damageNormal = 20f; // Урон для нормальных врагов
    public float damageWet = 40f;    // Урон для врагов с статусом wet
    public float radius = 5f;        // Радиус, в котором враги с статусом wet получат урон

    public void Strike(Transform target)
    {
        // Получаем компонент состояния и здоровья врага
        EnemyStatus targetStatus = target.GetComponent<EnemyStatus>();
        Health targetHealth = target.GetComponent<Health>();

        if (targetHealth == null || targetStatus == null)
            return;

        // Если враг с состоянием Wet, наносим 40 урона и создаем радиус
        if (targetStatus.currentState == EnemyState.Wet)
        {
            targetHealth.TakeDamage(damageWet);
            ApplyAoEDamage(target.position); // Применяем радиусный эффект
        }
        else
        {
            // Если враг не Wet, наносим 20 урона
            targetHealth.TakeDamage(damageNormal);
        }
    }

    private void ApplyAoEDamage(Vector3 center)
    {
        // Находим все объекты в радиусе
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);

        foreach (var collider in colliders)
        {
            EnemyStatus status = collider.GetComponent<EnemyStatus>();
            Health health = collider.GetComponent<Health>();

            if (health != null && status != null && status.currentState == EnemyState.Wet)
            {
                // Наносим 40 урона всем врагам с состоянием Wet
                health.TakeDamage(damageWet);
            }
        }
    }
}