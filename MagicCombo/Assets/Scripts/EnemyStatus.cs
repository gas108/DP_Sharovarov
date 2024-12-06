using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Normal;
    public GameObject wetIcon;
    public GameObject burningIcon;
    public GameObject lightningIcon;
    public float aoeRadius = 4f;
    private Health enemyHealth;
    private FireDamage fireDamage;

    private LightningStrike lightningEffect; // Ссылка на объект молнии для AOE

    private void Start()
    {
        wetIcon.SetActive(false); // Изначально иконка скрыта
        burningIcon.SetActive(false);
        lightningEffect = FindObjectOfType<LightningStrike>(); // Найти объект молнии
    }

    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
        fireDamage = GetComponent<FireDamage>(); // Получаем компонент FireDamage
    }

    void Update()
    {
        // Проверяем текущий статус противника и применяем соответствующий эффект
        switch (currentState)
        {
            case EnemyState.Wet:
                // Применяем эффект замедления от воды...
                break;
            case EnemyState.Burning:
                fireDamage.ApplyFireDamage(); // Применяем эффект горения
                break;
            case EnemyState.Frozen:
                // Применяем эффект заморозки...
                break;
            case EnemyState.Normal:
                // Возвращаемся к обычному состоянию...
                break;
            default:
                break;
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            Debug.Log("Изменено состояние: " + currentState);
        }

        // Обработка иконок и состояний
        switch (currentState)
        {
            case EnemyState.Wet:
                wetIcon.SetActive(true);
                break;
            default:
                wetIcon.SetActive(false);
                break;
        }

        switch (currentState)
        {
            case EnemyState.Burning:
                burningIcon.SetActive(true);
                break;
            default:
                burningIcon.SetActive(false);
                break;
        }

        switch (currentState)
        {
            case EnemyState.Shocked:
                lightningIcon.SetActive(true);  // Показываем иконку молнии
                StartCoroutine(ResetStateAfterDelay(1f));  // Меняем статус через 1 секунду
                break;
            default:
                lightningIcon.SetActive(false);
                break;
        }
    }

    // Метод для применения электрического урона
    public void ApplyElectricDamage()
    {

        if (currentState == EnemyState.Wet) // Если враг в статусе "Wet"
        {
            ApplyShockToNearbyEnemies();

            ChangeState(EnemyState.Shocked);  // Меняем статус на "Shocked"
        }
        else
        {
            Health enemyHealth = GetComponent<Health>();  // Получаем компонент Health
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(20f);
                ChangeState(EnemyState.Shocked); // Наносим 40 урона
            }
        }
    }

    private void ApplyShockToNearbyEnemies()
    {
        // Радиус в котором молния будет искать врагов с состоянием "Wet"
        float aoeRadius = 5f; // Задайте подходящий радиус

        // Получаем все коллайдеры в радиусе вокруг этого объекта
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, aoeRadius);

        // Проходим по всем найденным коллайдерам
        foreach (var collider in colliders)
        {
            // Проверяем, есть ли у объекта компонент EnemyStatus
            EnemyStatus enemyStatus = collider.GetComponent<EnemyStatus>();

            if (enemyStatus != null)
            {
                // Если у врага статус Wet, меняем его на Shocking и наносим 40 урона
                if (enemyStatus.currentState == EnemyState.Wet)
                {
                    // Наносим 40 урона
                    Health enemyHealth = collider.GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(40f);  // Наносим 40 урона
                    }

                    // Меняем статус на Shocking
                    enemyStatus.ChangeState(EnemyState.Shocked);
                }
            }
        }
    }

    public void ApplyFireDamage()
    {
        if (currentState == EnemyState.Burning)
        {
            fireDamage.ApplyFireDamage(); // Вызываем метод из FireDamage
        }

        if (currentState == EnemyState.Wet)
        {
            ChangeState(EnemyState.Normal);
        }
        else if (currentState == EnemyState.Frozen)
        {
            ChangeState(EnemyState.Wet);
        }
        else if (currentState == EnemyState.Normal)
        {
            ChangeState(EnemyState.Burning);
        }

        // Можем добавить другую логику при необходимости
    }

    public void ApplyIceDamage()
    {
        if (currentState == EnemyState.Wet)
        {
            ChangeState(EnemyState.Frozen); // Переход в замороженное состояние
        }
        else if (currentState == EnemyState.Normal)
        {
            ChangeState(EnemyState.Wet); // Переход в мокрое состояние
        }
        else if (currentState == EnemyState.Burning)
        {
            ChangeState(EnemyState.Wet); // Переход в мокрое состояние из горящего
        }
    }

    public void ApplyWaterEffect()
    {
        if (currentState == EnemyState.Normal)
        {
            ChangeState(EnemyState.Wet); // Применяем эффект мокроты
        }
        else if (currentState == EnemyState.Burning)
        {
            // Логика для того, чтобы потушить огонь
            ChangeState(EnemyState.Normal); // Убираем эффект горения
        }
    }

    IEnumerator ResetStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Задержка на 1 секунду
        ChangeState(EnemyState.Normal);         // Возвращаем состояние в Normal
    }
}
