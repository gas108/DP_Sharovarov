using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public GameObject waterProjectilePrefab; // ������ ������� �������
    public GameObject FireProjectilePrefab;
    public GameObject LightningStrikeProjectilePrefab;
    public Transform magicPoint; // �����, ������ ����� �������� ������

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShootWaterProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShootFireProjectilePrefab();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShootLightningStrikeProjectilePrefab();
        }
    }

    void ShootWaterProjectile()
    {
        // ������� ������, ���������� � ����������� ������
        Instantiate(waterProjectilePrefab, magicPoint.position, magicPoint.rotation);
    }

    void ShootFireProjectilePrefab()
    {
        Instantiate(FireProjectilePrefab, magicPoint.position, magicPoint.rotation);
    }

    void ShootLightningStrikeProjectilePrefab()
    {
        Instantiate(LightningStrikeProjectilePrefab, magicPoint.position, magicPoint.rotation);
    }
}