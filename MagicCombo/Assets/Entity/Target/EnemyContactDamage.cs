using System.Collections;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    public float contactDamage = 10f; // Урон, который наносится при контакте
    public float damageInterval = 1f; // Интервал между повторяющимся уроном
    private Health targetHealth; // Ссылка на объект здоровья, который получает урон

    private bool isContacting = false; // Флаг, показывающий, что контакт продолжается
    private Coroutine damageCoroutine; // Ссылка на корутину для повторяющегося урона

    // Метод, вызываемый при столкновении с объектом
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Проверяем, что объект, с которым мы сталкиваемся, имеет компонент Health
        if (collision.gameObject.CompareTag("Player"))
        {
            targetHealth = collision.gameObject.GetComponent<Health>();

            if (targetHealth != null && !isContacting)
            {
                // Если контакт только начался, запускаем корутину для повторяющегося урона
                isContacting = true;
                damageCoroutine = StartCoroutine(ApplyDamageOverTime());
            }
        }
    }

    // Метод, вызываемый, когда контакт прекращается
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Прекращаем корутину, если контакт с игроком закончился
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                isContacting = false;
            }
        }
    }

    // Корутин для повторяющегося урона
    private IEnumerator ApplyDamageOverTime()
    {
        while (isContacting)
        {
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(contactDamage); // Наносим урон
            }
            yield return new WaitForSeconds(damageInterval); // Ждем интервал перед повторным уроном
        }
    }
}
