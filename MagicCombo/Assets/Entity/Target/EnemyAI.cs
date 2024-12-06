using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    public Transform player;          // Ссылка на игрока
    public float speed = 3f;          // Скорость движения врага
    public float detectionRange = 10f; // Радиус обнаружения игрока

    private Rigidbody2D rb;            // Rigidbody врага
    private Vector2 movement;          // Текущее направление движения
    private bool isChasing = false;    // Флаг преследования игрока

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Игрок в зоне видимости — начинаем преследование
            isChasing = true;
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;

            // Поворот к игроку
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
        else
        {
            // Игрок вне зоны видимости — останавливаем движение
            isChasing = false;
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            // Двигаемся только при преследовании
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        else
        {
            // Остановка движения
            rb.velocity = Vector2.zero;  // Обнуляем скорость для полной остановки
        }
    }
}
