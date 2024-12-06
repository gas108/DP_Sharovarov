using System.Collections;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    public float contactDamage = 10f; // ����, ������� ��������� ��� ��������
    public float damageInterval = 1f; // �������� ����� ������������� ������
    private Health targetHealth; // ������ �� ������ ��������, ������� �������� ����

    private bool isContacting = false; // ����, ������������, ��� ������� ������������
    private Coroutine damageCoroutine; // ������ �� �������� ��� �������������� �����

    // �����, ���������� ��� ������������ � ��������
    private void OnCollisionStay2D(Collision2D collision)
    {
        // ���������, ��� ������, � ������� �� ������������, ����� ��������� Health
        if (collision.gameObject.CompareTag("Player"))
        {
            targetHealth = collision.gameObject.GetComponent<Health>();

            if (targetHealth != null && !isContacting)
            {
                // ���� ������� ������ �������, ��������� �������� ��� �������������� �����
                isContacting = true;
                damageCoroutine = StartCoroutine(ApplyDamageOverTime());
            }
        }
    }

    // �����, ����������, ����� ������� ������������
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���������� ��������, ���� ������� � ������� ����������
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                isContacting = false;
            }
        }
    }

    // ������� ��� �������������� �����
    private IEnumerator ApplyDamageOverTime()
    {
        while (isContacting)
        {
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(contactDamage); // ������� ����
            }
            yield return new WaitForSeconds(damageInterval); // ���� �������� ����� ��������� ������
        }
    }
}
