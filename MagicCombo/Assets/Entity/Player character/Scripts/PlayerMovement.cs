using UnityEngine;

public class PlayerMovementAndRotation : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения
    public float rotationSpeed = 10f; // Скорость поворота

    private Vector2 moveDirection; // Направление движения

    // Update вызывается каждый кадр
    void Update()
    {
        // Обработка ввода для движения (WASD)
        moveDirection.x = Input.GetAxisRaw("Horizontal"); // A/D или стрелки влево/вправ
        moveDirection.y = Input.GetAxisRaw("Vertical"); // W/S или стрелки вверх/вниз

        // Нормализуем вектор направления, чтобы движение было одинаково быстрым в любом направлении
        if (moveDirection.sqrMagnitude > 0)
        {
            moveDirection.Normalize();
        }

        // Двигаем персонажа
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Поворот персонажа в сторону курсора
        RotateTowardsMouse();
    }

    // Функция для поворота персонажа в сторону курсора
    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Получаем позицию курсора в мировых координатах
        mousePosition.z = 0; // Убираем изменение по оси Z (персонаж 2D)

        // Вычисляем угол между персонажем и курсором
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Поворачиваем персонажа
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
