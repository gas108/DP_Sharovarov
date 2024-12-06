/*

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // —корость движени€ персонажа
    private Vector3 targetPosition; // ÷елева€ позици€ (курсора мыши)

    // Update вызываетс€ каждый кадр
    void Update()
    {
        // ѕолучаем позицию мыши в мировых координатах
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0; // ”бираем изменение по оси Z, чтобы движение было только по X и Y

        // ƒвигаем персонажа к позиции мыши
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize(); // Ќормализуем вектор, чтобы движение было одинаково быстрым в любом направлении

        // ѕередвигаем персонажа с заданной скоростью
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}

*/