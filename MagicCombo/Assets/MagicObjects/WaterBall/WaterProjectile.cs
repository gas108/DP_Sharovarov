using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    public float speed = 10f; // �������� ������ �������
    public float triggerRadius = 3f; // ������ �������-����
    public LayerMask collisionLayer; // ����, � ������� ����� ����������� ������
    public GameObject waterSplashPrefab; // ������ ��������� ����
    public float damageAmount = 20f; // ���������� �����, ������� ������� ������

    private void Start()
    {
        // ������������� �������� �������� ������� � �����������, ���� �� ��� ���������
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.up * speed; // ����������� �������� ������ �� ������� (�� ��� Y)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ������ ���������� � ��������, ���������� ������ "��������" (���� ���� ��������� StatusEffect)
        StatusEffect statusEffect = collision.gameObject.GetComponent<StatusEffect>();
        if (statusEffect != null)
        {
            statusEffect.ActivateStatus("Wet", 5f); // ������������� ������ "��������" �� 5 ������
        }

        // ���������, ���� ������, � ������� ��������� ������������, ����� ��������� Health
        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            // ������� ����
            targetHealth.TakeDamage(damageAmount);
            Debug.Log("������� ���� ����������: " + damageAmount);
        }

        // ���� ������ ���������� � ���-��, ���������� �������-����
        ActivateWaterSplash();
        Destroy(gameObject); // ���������� ������ ����� ������������
    }

    // ������� ��� ��������� �������-���� (�������� ����)
    private void ActivateWaterSplash()
    {
        // ������� ����� ���� � ����� ������������
        GameObject waterSplash = Instantiate(waterSplashPrefab, transform.position, Quaternion.identity);

        // ������������� ������ �������� (���������)
        CircleCollider2D splashCollider = waterSplash.GetComponent<CircleCollider2D>();
        if (splashCollider != null)
        {
            splashCollider.radius = triggerRadius;
            splashCollider.isTrigger = true;
        }

        // ����� �������� �������������� ���������� ��� �������� �������
        Destroy(waterSplash, 2f); // ���������� ������ ������ ����� 2 ������� (����� ���������)
    }

    // ��� ������� ������� ��������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}