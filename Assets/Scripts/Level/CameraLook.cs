using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // ссылка на Transform персонажа
    private Vector3 offset = new Vector3(-0.04f, 10f, -8.2f); // смещение камеры относительно персонажа. у = 7,26
    private Vector3 rotation = new Vector3(55f, 0, 0);
    [SerializeField] private float movementThreshold = 0.5f; // порог для перемещения
    [SerializeField] private float smoothSpeed = 3f; // скорость сглаживания движения

    private Vector3 lastPlayerPosition;

    private void Start()
    {
        // Сохраняем начальную позицию игрока
        lastPlayerPosition = playerTransform.position;
    }
    private void LateUpdate()
    {
        // Постоянно обновляем позицию игрока для отслеживания его перемещений
        lastPlayerPosition = playerTransform.position;

        // Целевая позиция камеры
        Vector3 targetPosition = lastPlayerPosition + offset;

        // Скорость плавного следования камеры: при медленном движении уменьшаем, при быстром — увеличиваем
        float adjustedSmoothSpeed = Mathf.Lerp(1f, smoothSpeed, Vector3.Distance(transform.position, targetPosition) / smoothSpeed);

        // Плавное перемещение камеры к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, adjustedSmoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(rotation);
    }
    /*private void LateUpdate()
    {
        // Проверяем, насколько далеко переместился игрок
        if (Vector3.Distance(lastPlayerPosition, playerTransform.position) > movementThreshold)
        {
            // Обновляем последнюю сохраненную позицию игрока
            lastPlayerPosition = playerTransform.position;
        }

        // Целевая позиция камеры
        Vector3 targetPosition = lastPlayerPosition + offset;

        // Плавное перемещение камеры к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(rotation);
    }*/
    /*SerializeField] private Transform playerTransform; // ссылка на Transform персонажа
    //private Vector3 offset = new Vector3(0, 0, -5); // смещение камеры относительно персонажа
    private Vector3 offset = new Vector3(-0.04f, 7.26f, -8.2f); // смещение камеры относительно персонажа
    private Vector3 rotation = new Vector3(46f, 0, 0);
    private void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.Euler(rotation);
    }*/


}
