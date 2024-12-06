using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    public float damageNormal = 20f; // ���� ��� ���������� ������
    public float damageWet = 40f;    // ���� ��� ������ � �������� wet
    public float radius = 5f;        // ������, � ������� ����� � �������� wet ������� ����

    public void Strike(Transform target)
    {
        // �������� ��������� ��������� � �������� �����
        EnemyStatus targetStatus = target.GetComponent<EnemyStatus>();
        Health targetHealth = target.GetComponent<Health>();

        if (targetHealth == null || targetStatus == null)
            return;

        // ���� ���� � ���������� Wet, ������� 40 ����� � ������� ������
        if (targetStatus.currentState == EnemyState.Wet)
        {
            targetHealth.TakeDamage(damageWet);
            ApplyAoEDamage(target.position); // ��������� ��������� ������
        }
        else
        {
            // ���� ���� �� Wet, ������� 20 �����
            targetHealth.TakeDamage(damageNormal);
        }
    }

    private void ApplyAoEDamage(Vector3 center)
    {
        // ������� ��� ������� � �������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);

        foreach (var collider in colliders)
        {
            EnemyStatus status = collider.GetComponent<EnemyStatus>();
            Health health = collider.GetComponent<Health>();

            if (health != null && status != null && status.currentState == EnemyState.Wet)
            {
                // ������� 40 ����� ���� ������ � ���������� Wet
                health.TakeDamage(damageWet);
            }
        }
    }
}