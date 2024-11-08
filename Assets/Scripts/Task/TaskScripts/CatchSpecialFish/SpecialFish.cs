using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFish : MonoBehaviour
{
    [SerializeField] private GameObject effectPickUp;
    private float speed = 5f; // �������� ������������
    private float changeDirectionInterval = 2f; // �������� ����� �����������
    private float rotationSpeed = 5f; // �������� ��������

    private Vector3 direction;
    private float timer;
    private bool isDead = false;

    private Animator animator;

    private void Start()
    {
        ChangeDirection();

        animator = GetComponentInChildren<Animator>();
        Debug.Log(animator);
    }

     private void Update()
    {
        if (!isDead)
        {
            MoveFish();
            timer += Time.deltaTime;
            if (timer >= changeDirectionInterval)
            {
                ChangeDirection();
                timer = 0f;
            }
        }
    }
    private  void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<Harpoon>() != null) 
        {

            DeadFish();
        }
        if (other.gameObject.GetComponent<NetObject>() != null) 
        {

            StopAndDestroyFish();
        }
        if(other.gameObject.GetComponent<CharacterLocomotion>() != null && isDead )
        {
            PickUpFish();
        }
    }
    private void PickUpFish()
    {
        //Debug.Log("PickUpFish");
        MissionCatchSpecialFish.Instance.FishCounter();
        ArrowPointer.Instance.StopArrowCoroutine(gameObject);

        Destroy(gameObject);
    }
    private void StopAndDestroyFish()
    {
        ArrowPointer.Instance.StopArrowCoroutine(gameObject);

        Destroy(gameObject);

    }
    private void DeadFish()
    {
        isDead = true;

        // ������������� ������ �� ��� Y �� 180 ��������
        transform.Rotate(180, 0, 0);

        // ��������� ��������, ���� ���� ��������� Animator
        if (animator != null)
        {
            animator.enabled = false;
        }

        effectPickUp.SetActive(true);
    }
    private void MoveFish()
    {
        Vector3 movement = direction * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
        RotateFish();
    }

    private void ChangeDirection()
    {
        // ���������� ��������� ����������� ��������
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        direction = new Vector3(randomX, 0, randomZ).normalized; // �����������, ����� ��������� ���������� ��������
    }

    private void RotateFish()
    {
        // ������� ������� ������� � ����������� ��������
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // ������ ������������ ���� � ������ �����������
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
