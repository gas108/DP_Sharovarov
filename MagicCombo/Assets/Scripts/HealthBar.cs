using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;       // Ссылка на слайдер для полоски здоровья
    public GameObject healthBarObject; // Объект, который будет следовать за врагом (или игроком)
    private Health healthScript;       // Ссылка на скрипт здоровья объекта

    void Start()
    {
        // Получаем скрипт Health, чтобы следить за здоровьем
        healthScript = healthBarObject.GetComponent<Health>();

        // Прячем полоску здоровья, если у объекта нет скрипта Health
        if (healthScript == null)
        {
            Debug.LogError("No Health script found on " + healthBarObject.name);
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            // Обновляем максимальное значение полоски здоровья
            healthSlider.maxValue = healthScript.maxHealth;
            healthSlider.value = healthScript.currentHealth;
        }
    }

    void Update()
    {
        if (healthScript != null)
        {
            // Обновляем значение полоски здоровья на основе текущего здоровья
            healthSlider.value = healthScript.currentHealth;

            // Следим за объектом и отображаем полоску здоровья над ним
            healthSlider.transform.position = Camera.main.WorldToScreenPoint(healthBarObject.transform.position + new Vector3(0, 2f, 5f));
        }
    }
}
