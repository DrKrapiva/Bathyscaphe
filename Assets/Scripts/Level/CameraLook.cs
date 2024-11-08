using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // ������ �� Transform ���������
    private Vector3 offset = new Vector3(-0.04f, 10f, -8.2f); // �������� ������ ������������ ���������. � = 7,26
    private Vector3 rotation = new Vector3(55f, 0, 0);
    [SerializeField] private float movementThreshold = 0.5f; // ����� ��� �����������
    [SerializeField] private float smoothSpeed = 3f; // �������� ����������� ��������

    private Vector3 lastPlayerPosition;

    private void Start()
    {
        // ��������� ��������� ������� ������
        lastPlayerPosition = playerTransform.position;
    }
    private void LateUpdate()
    {
        // ��������� ��������� ������� ������ ��� ������������ ��� �����������
        lastPlayerPosition = playerTransform.position;

        // ������� ������� ������
        Vector3 targetPosition = lastPlayerPosition + offset;

        // �������� �������� ���������� ������: ��� ��������� �������� ���������, ��� ������� � �����������
        float adjustedSmoothSpeed = Mathf.Lerp(1f, smoothSpeed, Vector3.Distance(transform.position, targetPosition) / smoothSpeed);

        // ������� ����������� ������ � ������� �������
        transform.position = Vector3.Lerp(transform.position, targetPosition, adjustedSmoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(rotation);
    }
    /*private void LateUpdate()
    {
        // ���������, ��������� ������ ������������ �����
        if (Vector3.Distance(lastPlayerPosition, playerTransform.position) > movementThreshold)
        {
            // ��������� ��������� ����������� ������� ������
            lastPlayerPosition = playerTransform.position;
        }

        // ������� ������� ������
        Vector3 targetPosition = lastPlayerPosition + offset;

        // ������� ����������� ������ � ������� �������
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(rotation);
    }*/
    /*SerializeField] private Transform playerTransform; // ������ �� Transform ���������
    //private Vector3 offset = new Vector3(0, 0, -5); // �������� ������ ������������ ���������
    private Vector3 offset = new Vector3(-0.04f, 7.26f, -8.2f); // �������� ������ ������������ ���������
    private Vector3 rotation = new Vector3(46f, 0, 0);
    private void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.Euler(rotation);
    }*/


}
