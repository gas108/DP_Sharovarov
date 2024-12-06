using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public float speed = 10f; // �������� ������ �������

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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // �������� ��������� EnemyStatus
                EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();

                if (enemyStatus != null)
                {
                    enemyStatus.ApplyFireDamage();
                }
            }
        }
        Destroy(gameObject);
    }

}
