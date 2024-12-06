using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusEffect : MonoBehaviour
{
    private Dictionary<string, float> activeStatuses = new Dictionary<string, float>(); // ������� �������� �������� � ������� �� ��������
    public GameObject wetEffectPrefab; // ������ ������� (������� ����), ������� ����� ������������ ��� ������� "Wet"
    public GameObject fireEffectPrefab;
    private GameObject currentWetEffect; // ������� �������� ������ "Wet"
    private GameObject currentFireEffect;

    // ����� ��� ��������� �������
    public void ActivateStatus(string status, float duration)
    {
        // ��������� ��� ��������� ������ � �������
        if (!activeStatuses.ContainsKey(status))
        {
            activeStatuses.Add(status, duration);
        }
        else
        {
            activeStatuses[status] = duration;
        }

        // � ����������� �� ������� ��������� ��������������� ������
        if (status == "Wet")
        {
            ApplyWetEffect();
        }

        if (status == "Burning")
        {
            ApplyFireEffect();
        }

        // ��������� �������� ��� �������� ������� ����� ��������� �����
        StartCoroutine(DeactivateStatusAfterTime(status, duration));
    }

    // ����� ��� ���������� ����������� ������� "Wet"
    private void ApplyWetEffect()
    {
        if (wetEffectPrefab != null)
        {
            // ������� ������ ������ ����� ��������� ������
            if (currentWetEffect != null)
            {
                Destroy(currentWetEffect);
            }

            // ������� ����� ������ ���������
            currentWetEffect = Instantiate(wetEffectPrefab, transform.position, Quaternion.identity, transform);
            currentWetEffect.transform.localPosition = new Vector3(0, 0, 0); // ������� ������ ������� �����
        }
    }

    private void ApplyFireEffect()
    {
        if(fireEffectPrefab != null)
        {
            if (currentFireEffect != null)
            {
                Destroy(currentFireEffect);
            }

            // ������� ����� ������ ���������
            currentFireEffect = Instantiate(wetEffectPrefab, transform.position, Quaternion.identity, transform);
            currentFireEffect.transform.localPosition = new Vector3(0, 0, 0); // ������� ������ ������� �����
        }
    }

    // ����� ��� ����������� ������� ����� ������������� �������
    private IEnumerator DeactivateStatusAfterTime(string status, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (activeStatuses.ContainsKey(status))
        {
            activeStatuses[status] = 0f;
            RemoveEffect(status);
        }
    }

    // ����� ��� �������� ����������� �������
    private void RemoveEffect(string status)
    {
        if (status == "Wet" && currentWetEffect != null)
        {
            Destroy(currentWetEffect); // ������� ������
            currentWetEffect = null;
        }
    }




    // ����� ��� ��������, ������� �� ������
    public bool IsStatusActive(string status)
    {
        return activeStatuses.ContainsKey(status) && activeStatuses[status] > 0f;
    }








}