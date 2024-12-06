using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusEffect : MonoBehaviour
{
    private Dictionary<string, float> activeStatuses = new Dictionary<string, float>(); // Словарь активных статусов и времени их действия
    public GameObject wetEffectPrefab; // Префаб эффекта (спрайта воды), который будет отображаться при статусе "Wet"
    public GameObject fireEffectPrefab;
    private GameObject currentWetEffect; // Текущий активный эффект "Wet"
    private GameObject currentFireEffect;

    // Метод для активации статуса
    public void ActivateStatus(string status, float duration)
    {
        // Добавляем или обновляем статус в словаре
        if (!activeStatuses.ContainsKey(status))
        {
            activeStatuses.Add(status, duration);
        }
        else
        {
            activeStatuses[status] = duration;
        }

        // В зависимости от статуса применяем соответствующий эффект
        if (status == "Wet")
        {
            ApplyWetEffect();
        }

        if (status == "Burning")
        {
            ApplyFireEffect();
        }

        // Запускаем корутину для удаления статуса через некоторое время
        StartCoroutine(DeactivateStatusAfterTime(status, duration));
    }

    // Метод для применения визуального эффекта "Wet"
    private void ApplyWetEffect()
    {
        if (wetEffectPrefab != null)
        {
            // Удаляем старый эффект перед созданием нового
            if (currentWetEffect != null)
            {
                Destroy(currentWetEffect);
            }

            // Создаем новый эффект намокания
            currentWetEffect = Instantiate(wetEffectPrefab, transform.position, Quaternion.identity, transform);
            currentWetEffect.transform.localPosition = new Vector3(0, 0, 0); // Смещаем эффект немного вверх
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

            // Создаем новый эффект намокания
            currentFireEffect = Instantiate(wetEffectPrefab, transform.position, Quaternion.identity, transform);
            currentFireEffect.transform.localPosition = new Vector3(0, 0, 0); // Смещаем эффект немного вверх
        }
    }

    // Метод для деактивации статуса после определенного времени
    private IEnumerator DeactivateStatusAfterTime(string status, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (activeStatuses.ContainsKey(status))
        {
            activeStatuses[status] = 0f;
            RemoveEffect(status);
        }
    }

    // Метод для удаления визуального эффекта
    private void RemoveEffect(string status)
    {
        if (status == "Wet" && currentWetEffect != null)
        {
            Destroy(currentWetEffect); // Удаляем эффект
            currentWetEffect = null;
        }
    }




    // Метод для проверки, активен ли статус
    public bool IsStatusActive(string status)
    {
        return activeStatuses.ContainsKey(status) && activeStatuses[status] > 0f;
    }








}