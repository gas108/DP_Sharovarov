using UnityEngine;
using UnityEngine.SceneManagement; // Для управления сценами
using UnityEngine.UI; // Для работы с UI

public class GameOverManager : MonoBehaviour
{
    public GameObject restartButton;  // Ссылка на кнопку рестарта
    public Health playerHealth;       // Ссылка на компонент Health игрока

    private void Start()
    {
        // Скрываем кнопку при старте игры
        restartButton.SetActive(false);
    }

    private void Update()
    {
        // Проверяем, если игрок мертв
        if (playerHealth != null && playerHealth.isDead)
        {
            ShowRestartButton();
        }
    }

    // Метод для отображения кнопки рестарта
    private void ShowRestartButton()
    {
        restartButton.SetActive(true); // Показываем кнопку
    }

    // Метод для перезапуска сцены
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагружаем текущую сцену
    }
}
