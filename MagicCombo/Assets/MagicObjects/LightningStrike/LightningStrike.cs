using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    public float speed = 10f; // Скорость полета снаряда

    private void Start()
    {
        // Устанавливаем скорость движения снаряда в направлении, куда он был выстрелен
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.up * speed; // Направление движения вперед от объекта (по оси Y)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();
            if (enemyStatus != null)
            {
                enemyStatus.ApplyElectricDamage();
            }
        }
        Destroy(gameObject);
    }

}
