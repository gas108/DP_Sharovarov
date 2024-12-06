using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // ������������ ��������
    public float currentHealth;    // ������� ��������
    public bool isDead = false;    // ����, ������������, ����� �� ��������

    // �������, ������� ����� �������, ����� �������� ���������
    public event System.Action<float> OnHealthChanged;
    public event System.Action OnDeath;

    private Renderer renderer; // ������ �� ��������� Renderer
    private EnemyAI2D enemyAI;  // ������ �� ��������� EnemyAI
    private PlayerMovementAndRotation PlayerAI;
    public GameObject rightHand; // ������ �� ������ ������ ����
    public GameObject leftHand;  // ������ �� ������ ����� ����

    void Start()
    {
        currentHealth = maxHealth; // ������������� �������� ��������
        renderer = GetComponent<Renderer>(); // �������� ��������� Renderer (����� �������� �� SpriteRenderer ��� ������)
        enemyAI = GetComponent<EnemyAI2D>(); // �������� ��������� EnemyAI
        PlayerAI = GetComponent<PlayerMovementAndRotation>();
    }

    // ����� ��� ��������� �����
    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount; // ��������� ��������

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            OnHealthChanged?.Invoke(currentHealth / maxHealth); // ��������� UI ��� ������ ��������
        }
    }

    // ����� ��� ���������
    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // �������� �� ���������� ������������� ��������
        }

        OnHealthChanged?.Invoke(currentHealth / maxHealth);
    }

    // ����� ��� ������
    private void Die()
    {
        isDead = true;
        OnDeath?.Invoke(); // �������� ������� ������

        // ��������� ��������� EnemyAI (����)
        if (enemyAI != null)
        {
            enemyAI.enabled = false; // ��������� �� �����
        }

        if (PlayerAI != null)
        {
            PlayerAI.enabled = false;
        }

        // ������ �������� �����
        if (renderer != null)
        {
            if (renderer.material != null)
            {
                renderer.material.color = Color.gray; // ������ ���� �� �����
            }
        }

        // ������� ������ � ����� ����
        if (rightHand != null)
        {
            Destroy(rightHand); // ������� ������ ����
        }

        if (leftHand != null)
        {
            Destroy(leftHand); // ������� ����� ����
        }

        // ����� ����� �������� �������� ������ ��� ������ ��������
        Debug.Log(gameObject.name + " died!");
    }
}