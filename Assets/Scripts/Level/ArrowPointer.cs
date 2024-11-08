using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ArrowPointer : MonoBehaviour
{
    [SerializeField] private Transform player; // ������ �� ���������
    [SerializeField] private Image arrowPrefab; // ������ �������
    [SerializeField] private RectTransform panelWithCircle; // ������ � ������
    private Dictionary<GameObject, Coroutine> arrowCoroutines = new Dictionary<GameObject, Coroutine>(); // ������� ��� �������� �������
    private Dictionary<GameObject, Image> arrowInstances = new Dictionary<GameObject, Image>(); // ������� ��� �������� ����������� �������

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

    // ������� ��� ������� ��������
    public void StartArrowCoroutine(GameObject target, Sprite arrowSprite)
    {
        Debug.Log("������ �������� ��� �������: " + target.name);

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

    // ������� ��� ��������� ��������
    public void StopArrowCoroutine(GameObject target)
    {
        Debug.Log("��������� �������� ��� �������: " + target.name);

        if (arrowCoroutines.ContainsKey(target))
        {
            StopCoroutine(arrowCoroutines[target]);
            arrowCoroutines.Remove(target);
            Destroy(arrowInstances[target].gameObject);
            arrowInstances.Remove(target);
        }
    }

    // ������� ��� ���������� ��������� �������
    private IEnumerator UpdateArrow(GameObject target, Sprite arrowSprite)
    {
        Image arrowInstance = Instantiate(arrowPrefab, panelWithCircle);
        arrowInstances[target] = arrowInstance;

        // ��������� ������� ����������� ��� �������
        arrowInstance.sprite = arrowSprite;

        while (true)
        {
            // ��������� ����������� �� ������ �� ����
            Vector3 direction = (target.transform.position - player.position).normalized;

            // ����������� ����������� � 2D ����
            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            // ������������� ���� �������� �������
            arrowInstance.rectTransform.localRotation = Quaternion.Euler(0, 0, angle);

            // ������������� ������� ������� �� ������ �����
            float radius = panelWithCircle.rect.width / 2;
            Vector2 arrowPos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
            arrowInstance.rectTransform.anchoredPosition = arrowPos;

            yield return null;
        }
    }
    /*[SerializeField] private Transform player; // ������ �� ���������
    [SerializeField] private Image arrowPrefab; // ������ �������
    //[SerializeField] private RectTransform canvas; // ������ �� Canvas
    [SerializeField] private RectTransform panelWithCircle; // ������ � ������
    //[SerializeField] private Image circleImage; // ����������� �����
    private List<GameObject> chests; // ������ ��������
    private List<Image> arrows; // ������ �������

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
            // ����� ���� �������� � ����� "Chest"
            GameObject[] foundChests = GameObject.FindGameObjectsWithTag("Chest");

            // ������� ������ �������, ���� �������� ����� ������
            while (arrows.Count > foundChests.Length)
            {
                Image arrow = arrows[arrows.Count - 1];
                arrows.RemoveAt(arrows.Count - 1);
                Destroy(arrow.gameObject);
            }

            // ��������� ����� �������, ���� �������� ����� ������
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

                // ��������� ����������� �� ������ �� �������
                Vector3 direction = (chest.transform.position - player.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // ������������� ������� ������� �� ������ �����
                float radius = panelWithCircle.rect.width / 2;
                Vector2 arrowPos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
                arrow.rectTransform.anchoredPosition = arrowPos;

                // ������������� ���� �������� ������� ���, ����� ��� ��������� �� ������
                //arrow.rectTransform.localRotation = Quaternion.Euler(0, 0, angle - 90);

                arrow.enabled = true;
            }

            yield return new WaitForSeconds(0.1f); // ��������� ������ 0.1 ������� (������������� ��������)
        }
    }*/

}
