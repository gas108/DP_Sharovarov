/*

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� �������� ���������
    private Vector3 targetPosition; // ������� ������� (������� ����)

    // Update ���������� ������ ����
    void Update()
    {
        // �������� ������� ���� � ������� �����������
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0; // ������� ��������� �� ��� Z, ����� �������� ���� ������ �� X � Y

        // ������� ��������� � ������� ����
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize(); // ����������� ������, ����� �������� ���� ��������� ������� � ����� �����������

        // ����������� ��������� � �������� ���������
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}

*/