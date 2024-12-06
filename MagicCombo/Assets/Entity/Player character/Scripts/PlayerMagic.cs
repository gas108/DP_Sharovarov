using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public GameObject waterProjectilePrefab; // Префаб водного снаряда
    public GameObject FireProjectilePrefab;
    public GameObject LightningStrikeProjectilePrefab;
    public Transform magicPoint; // Точка, откуда будет вылетать снаряд

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
        // Создаем снаряд, вылетающий в направлении игрока
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