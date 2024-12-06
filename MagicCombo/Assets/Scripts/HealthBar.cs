using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;       // ������ �� ������� ��� ������� ��������
    public GameObject healthBarObject; // ������, ������� ����� ��������� �� ������ (��� �������)
    private Health healthScript;       // ������ �� ������ �������� �������

    void Start()
    {
        // �������� ������ Health, ����� ������� �� ���������
        healthScript = healthBarObject.GetComponent<Health>();

        // ������ ������� ��������, ���� � ������� ��� ������� Health
        if (healthScript == null)
        {
            Debug.LogError("No Health script found on " + healthBarObject.name);
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            // ��������� ������������ �������� ������� ��������
            healthSlider.maxValue = healthScript.maxHealth;
            healthSlider.value = healthScript.currentHealth;
        }
    }

    void Update()
    {
        if (healthScript != null)
        {
            // ��������� �������� ������� �������� �� ������ �������� ��������
            healthSlider.value = healthScript.currentHealth;

            // ������ �� �������� � ���������� ������� �������� ��� ���
            healthSlider.transform.position = Camera.main.WorldToScreenPoint(healthBarObject.transform.position + new Vector3(0, 2f, 5f));
        }
    }
}
