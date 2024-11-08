using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ArrowPointer : MonoBehaviour
{
    [SerializeField] private Transform player; // Ссылка на персонажа
    [SerializeField] private Image arrowPrefab; // Префаб стрелки
    [SerializeField] private RectTransform panelWithCircle; // Панель с кругом
    private Dictionary<GameObject, Coroutine> arrowCoroutines = new Dictionary<GameObject, Coroutine>(); // Словарь для хранения корутин
    private Dictionary<GameObject, Image> arrowInstances = new Dictionary<GameObject, Image>(); // Словарь для хранения экземпляров стрелок

    public static ArrowPointer Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    static private ArrowPointer _instance;

    // Функция для запуска корутины
    public void StartArrowCoroutine(GameObject target, Sprite arrowSprite)
    {
        Debug.Log("Запуск корутины для объекта: " + target.name);

        if (arrowCoroutines.ContainsKey(target))
        {
            StopCoroutine(arrowCoroutines[target]);
            arrowCoroutines.Remove(target);
            Destroy(arrowInstances[target].gameObject);
            arrowInstances.Remove(target);
        }

        Coroutine newCoroutine = StartCoroutine(UpdateArrow(target, arrowSprite));
        arrowCoroutines[target] = newCoroutine;
    }

    // Функция для остановки корутины
    public void StopArrowCoroutine(GameObject target)
    {
        Debug.Log("Остановка корутины для объекта: " + target.name);

        if (arrowCoroutines.ContainsKey(target))
        {
            StopCoroutine(arrowCoroutines[target]);
            arrowCoroutines.Remove(target);
            Destroy(arrowInstances[target].gameObject);
            arrowInstances.Remove(target);
        }
    }

    // Корутин для обновления положения стрелки
    private IEnumerator UpdateArrow(GameObject target, Sprite arrowSprite)
    {
        Image arrowInstance = Instantiate(arrowPrefab, panelWithCircle);
        arrowInstances[target] = arrowInstance;

        // Установка нужного изображения для стрелки
        arrowInstance.sprite = arrowSprite;

        while (true)
        {
            // Вычисляем направление от игрока до цели
            Vector3 direction = (target.transform.position - player.position).normalized;

            // Преобразуем направление в 2D угол
            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            // Устанавливаем угол поворота стрелки
            arrowInstance.rectTransform.localRotation = Quaternion.Euler(0, 0, angle);

            // Устанавливаем позицию стрелки на орбите круга
            float radius = panelWithCircle.rect.width / 2;
            Vector2 arrowPos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
            arrowInstance.rectTransform.anchoredPosition = arrowPos;

            yield return null;
        }
    }
    /*[SerializeField] private Transform player; // Ссылка на персонажа
    [SerializeField] private Image arrowPrefab; // Префаб стрелки
    //[SerializeField] private RectTransform canvas; // Ссылка на Canvas
    [SerializeField] private RectTransform panelWithCircle; // Панель с кругом
    //[SerializeField] private Image circleImage; // Изображение круга
    private List<GameObject> chests; // Список сундуков
    private List<Image> arrows; // Список стрелок

    void Start()
    {
        chests = new List<GameObject>();
        arrows = new List<Image>();

        StartCoroutine(CheckChestPositions());
    }

    IEnumerator CheckChestPositions()
    {
        while (true)
        {
            // Поиск всех объектов с тегом "Chest"
            GameObject[] foundChests = GameObject.FindGameObjectsWithTag("Chest");

            // Удаляем лишние стрелки, если сундуков стало меньше
            while (arrows.Count > foundChests.Length)
            {
                Image arrow = arrows[arrows.Count - 1];
                arrows.RemoveAt(arrows.Count - 1);
                Destroy(arrow.gameObject);
            }

            // Добавляем новые стрелки, если сундуков стало больше
            while (arrows.Count < foundChests.Length)
            {
                Image arrow = Instantiate(arrowPrefab, panelWithCircle);
                arrows.Add(arrow);
            }

            chests.Clear();
            chests.AddRange(foundChests);

            for (int i = 0; i < chests.Count; i++)
            {
                GameObject chest = chests[i];
                Image arrow = arrows[i];

                // Вычисляем направление от игрока до сундука
                Vector3 direction = (chest.transform.position - player.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Устанавливаем позицию стрелки на орбите круга
                float radius = panelWithCircle.rect.width / 2;
                Vector2 arrowPos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
                arrow.rectTransform.anchoredPosition = arrowPos;

                // Устанавливаем угол поворота стрелки так, чтобы она указывала на сундук
                //arrow.rectTransform.localRotation = Quaternion.Euler(0, 0, angle - 90);

                arrow.enabled = true;
            }

            yield return new WaitForSeconds(0.1f); // Проверяем каждые 0.1 секунды (настраиваемый интервал)
        }
    }*/

}
