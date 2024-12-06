using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    public Transform player;          // ������ �� ������
    public float speed = 3f;          // �������� �������� �����
    public float detectionRange = 10f; // ������ ����������� ������

    private Rigidbody2D rb;            // Rigidbody �����
    private Vector2 movement;          // ������� ����������� ��������
    private bool isChasing = false;    // ���� ������������� ������

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
            // ����� � ���� ��������� � �������� �������������
            isChasing = true;
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;

            // ������� � ������
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
        else
        {
            // ����� ��� ���� ��������� � ������������� ��������
            isChasing = false;
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            // ��������� ������ ��� �������������
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        else
        {
            // ��������� ��������
            rb.velocity = Vector2.zero;  // �������� �������� ��� ������ ���������
        }
    }
}
