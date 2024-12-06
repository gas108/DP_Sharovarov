using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    public float speed = 10f; // Скорость полета снаряда
    public float triggerRadius = 3f; // Радиус триггер-зоны
    public LayerMask collisionLayer; // Слой, с которым может столкнуться снаряд
    public GameObject waterSplashPrefab; // Префаб расплеска воды
    public float damageAmount = 20f; // Количество урона, которое наносит снаряд

    private void Start()
    {
        // Устанавливаем скорость движения снаряда в направлении, куда он был выстрелен
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.up * speed; // Направление движения вперед от объекта (по оси Y)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Если снаряд столкнулся с объектом, активируем статус "намокший" (если есть компонент StatusEffect)
        StatusEffect statusEffect = collision.gameObject.GetComponent<StatusEffect>();
        if (statusEffect != null)
        {
            statusEffect.ActivateStatus("Wet", 5f); // Устанавливаем статус "намокший" на 5 секунд
        }

        // Проверяем, если объект, с которым произошло столкновение, имеет компонент Health
        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            // Наносим урон
            targetHealth.TakeDamage(damageAmount);
            Debug.Log("Нанесен урон противнику: " + damageAmount);
        }

        // Если снаряд столкнулся с чем-то, активируем триггер-зону
        ActivateWaterSplash();
        Destroy(gameObject); // Уничтожаем снаряд после столкновения
    }

    // Функция для активации триггер-зоны (разлитой воды)
    private void ActivateWaterSplash()
    {
        // Создаем сплэш воды в месте столкновения
        GameObject waterSplash = Instantiate(waterSplashPrefab, transform.position, Quaternion.identity);

        // Устанавливаем радиус триггера (коллайдер)
        CircleCollider2D splashCollider = waterSplash.GetComponent<CircleCollider2D>();
        if (splashCollider != null)
        {
            splashCollider.radius = triggerRadius;
            splashCollider.isTrigger = true;
        }

        // Можно добавить дополнительные визуальные или звуковые эффекты
        Destroy(waterSplash, 2f); // Уничтожаем водный эффект через 2 секунды (можно настроить)
    }

    // Для отладки радиуса триггера
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}