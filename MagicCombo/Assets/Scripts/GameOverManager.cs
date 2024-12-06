using UnityEngine;
using UnityEngine.SceneManagement; // ��� ���������� �������
using UnityEngine.UI; // ��� ������ � UI

public class GameOverManager : MonoBehaviour
{
    public GameObject restartButton;  // ������ �� ������ ��������
    public Health playerHealth;       // ������ �� ��������� Health ������

    private void Start()
    {
        // �������� ������ ��� ������ ����
        restartButton.SetActive(false);
    }

    private void Update()
    {
        // ���������, ���� ����� �����
        if (playerHealth != null && playerHealth.isDead)
        {
            ShowRestartButton();
        }
    }

    // ����� ��� ����������� ������ ��������
    private void ShowRestartButton()
    {
        restartButton.SetActive(true); // ���������� ������
    }

    // ����� ��� ����������� �����
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ������������� ������� �����
    }
}
