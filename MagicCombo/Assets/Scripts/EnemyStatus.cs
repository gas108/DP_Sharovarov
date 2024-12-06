using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Normal;
    public GameObject wetIcon;
    public GameObject burningIcon;
    public GameObject lightningIcon;
    public float aoeRadius = 4f;
    private Health enemyHealth;
    private FireDamage fireDamage;

    private LightningStrike lightningEffect; // ������ �� ������ ������ ��� AOE

    private void Start()
    {
        wetIcon.SetActive(false); // ���������� ������ ������
        burningIcon.SetActive(false);
        lightningEffect = FindObjectOfType<LightningStrike>(); // ����� ������ ������
    }

    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
        fireDamage = GetComponent<FireDamage>(); // �������� ��������� FireDamage
    }

    void Update()
    {
        // ��������� ������� ������ ���������� � ��������� ��������������� ������
        switch (currentState)
        {
            case EnemyState.Wet:
                // ��������� ������ ���������� �� ����...
                break;
            case EnemyState.Burning:
                fireDamage.ApplyFireDamage(); // ��������� ������ �������
                break;
            case EnemyState.Frozen:
                // ��������� ������ ���������...
                break;
            case EnemyState.Normal:
                // ������������ � �������� ���������...
                break;
            default:
                break;
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            Debug.Log("�������� ���������: " + currentState);
        }

        // ��������� ������ � ���������
        switch (currentState)
        {
            case EnemyState.Wet:
                wetIcon.SetActive(true);
                break;
            default:
                wetIcon.SetActive(false);
                break;
        }

        switch (currentState)
        {
            case EnemyState.Burning:
                burningIcon.SetActive(true);
                break;
            default:
                burningIcon.SetActive(false);
                break;
        }

        switch (currentState)
        {
            case EnemyState.Shocked:
                lightningIcon.SetActive(true);  // ���������� ������ ������
                StartCoroutine(ResetStateAfterDelay(1f));  // ������ ������ ����� 1 �������
                break;
            default:
                lightningIcon.SetActive(false);
                break;
        }
    }

    // ����� ��� ���������� �������������� �����
    public void ApplyElectricDamage()
    {

        if (currentState == EnemyState.Wet) // ���� ���� � ������� "Wet"
        {
            ApplyShockToNearbyEnemies();

            ChangeState(EnemyState.Shocked);  // ������ ������ �� "Shocked"
        }
        else
        {
            Health enemyHealth = GetComponent<Health>();  // �������� ��������� Health
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(20f);
                ChangeState(EnemyState.Shocked); // ������� 40 �����
            }
        }
    }

    private void ApplyShockToNearbyEnemies()
    {
        // ������ � ������� ������ ����� ������ ������ � ���������� "Wet"
        float aoeRadius = 5f; // ������� ���������� ������

        // �������� ��� ���������� � ������� ������ ����� �������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, aoeRadius);

        // �������� �� ���� ��������� �����������
        foreach (var collider in colliders)
        {
            // ���������, ���� �� � ������� ��������� EnemyStatus
            EnemyStatus enemyStatus = collider.GetComponent<EnemyStatus>();

            if (enemyStatus != null)
            {
                // ���� � ����� ������ Wet, ������ ��� �� Shocking � ������� 40 �����
                if (enemyStatus.currentState == EnemyState.Wet)
                {
                    // ������� 40 �����
                    Health enemyHealth = collider.GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(40f);  // ������� 40 �����
                    }

                    // ������ ������ �� Shocking
                    enemyStatus.ChangeState(EnemyState.Shocked);
                }
            }
        }
    }

    public void ApplyFireDamage()
    {
        if (currentState == EnemyState.Burning)
        {
            fireDamage.ApplyFireDamage(); // �������� ����� �� FireDamage
        }

        if (currentState == EnemyState.Wet)
        {
            ChangeState(EnemyState.Normal);
        }
        else if (currentState == EnemyState.Frozen)
        {
            ChangeState(EnemyState.Wet);
        }
        else if (currentState == EnemyState.Normal)
        {
            ChangeState(EnemyState.Burning);
        }

        // ����� �������� ������ ������ ��� �������������
    }

    public void ApplyIceDamage()
    {
        if (currentState == EnemyState.Wet)
        {
            ChangeState(EnemyState.Frozen); // ������� � ������������ ���������
        }
        else if (currentState == EnemyState.Normal)
        {
            ChangeState(EnemyState.Wet); // ������� � ������ ���������
        }
        else if (currentState == EnemyState.Burning)
        {
            ChangeState(EnemyState.Wet); // ������� � ������ ��������� �� ��������
        }
    }

    public void ApplyWaterEffect()
    {
        if (currentState == EnemyState.Normal)
        {
            ChangeState(EnemyState.Wet); // ��������� ������ �������
        }
        else if (currentState == EnemyState.Burning)
        {
            // ������ ��� ����, ����� �������� �����
            ChangeState(EnemyState.Normal); // ������� ������ �������
        }
    }

    IEnumerator ResetStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �������� �� 1 �������
        ChangeState(EnemyState.Normal);         // ���������� ��������� � Normal
    }
}
