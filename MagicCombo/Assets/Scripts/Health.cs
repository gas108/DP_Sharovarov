using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // Максимальное здоровье
    public float currentHealth;    // Текущее здоровье
    public bool isDead = false;    // Флаг, показывающий, мертв ли персонаж

    // Событие, которое будет вызвано, когда здоровье изменится
    public event System.Action<float> OnHealthChanged;
    public event System.Action OnDeath;

    private Renderer renderer; // Ссылка на компонент Renderer
    private EnemyAI2D enemyAI;  // Ссылка на компонент EnemyAI
    private PlayerMovementAndRotation PlayerAI;
    public GameObject rightHand; // Ссылка на объект правой руки
    public GameObject leftHand;  // Ссылка на объект левой руки

    void Start()
    {
        currentHealth = maxHealth; // Инициализация текущего здоровья
        renderer = GetComponent<Renderer>(); // Получаем компонент Renderer (можно заменить на SpriteRenderer или другие)
        enemyAI = GetComponent<EnemyAI2D>(); // Получаем компонент EnemyAI
        PlayerAI = GetComponent<PlayerMovementAndRotation>();
    }

    // Метод для получения урона
    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount; // Уменьшаем здоровье

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            OnHealthChanged?.Invoke(currentHealth / maxHealth); // Обновляем UI или другие элементы
        }
    }

    // Метод для исцеления
    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Защищаем от превышения максимального здоровья
        }

        OnHealthChanged?.Invoke(currentHealth / maxHealth);
    }

    // Метод для смерти
    private void Die()
    {
        isDead = true;
        OnDeath?.Invoke(); // Вызываем событие смерти

        // Отключаем компонент EnemyAI (мозг)
        if (enemyAI != null)
        {
            enemyAI.enabled = false; // Отключаем ИИ врага
        }

        if (PlayerAI != null)
        {
            PlayerAI.enabled = false;
        }

        // Делаем сущность серой
        if (renderer != null)
        {
            if (renderer.material != null)
            {
                renderer.material.color = Color.gray; // Меняем цвет на серый
            }
        }

        // Удаляем правую и левую руку
        if (rightHand != null)
        {
            Destroy(rightHand); // Удаляем правую руку
        }

        if (leftHand != null)
        {
            Destroy(leftHand); // Удаляем левую руку
        }

        // Здесь можно добавить анимацию смерти или другие действия
        Debug.Log(gameObject.name + " died!");
    }
}