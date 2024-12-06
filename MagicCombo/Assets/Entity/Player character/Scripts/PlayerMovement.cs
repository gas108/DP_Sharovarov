using UnityEngine;

public class PlayerMovementAndRotation : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ��������
    public float rotationSpeed = 10f; // �������� ��������

    private Vector2 moveDirection; // ����������� ��������

    // Update ���������� ������ ����
    void Update()
    {
        // ��������� ����� ��� �������� (WASD)
        moveDirection.x = Input.GetAxisRaw("Horizontal"); // A/D ��� ������� �����/�����
        moveDirection.y = Input.GetAxisRaw("Vertical"); // W/S ��� ������� �����/����

        // ����������� ������ �����������, ����� �������� ���� ��������� ������� � ����� �����������
        if (moveDirection.sqrMagnitude > 0)
        {
            moveDirection.Normalize();
        }

        // ������� ���������
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // ������� ��������� � ������� �������
        RotateTowardsMouse();
    }

    // ������� ��� �������� ��������� � ������� �������
    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // �������� ������� ������� � ������� �����������
        mousePosition.z = 0; // ������� ��������� �� ��� Z (�������� 2D)

        // ��������� ���� ����� ���������� � ��������
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ������������ ���������
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
