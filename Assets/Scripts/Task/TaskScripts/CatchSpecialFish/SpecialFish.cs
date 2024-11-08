using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFish : MonoBehaviour
{
    [SerializeField] private GameObject effectPickUp;
    private float speed = 5f; // скорость передвижения
    private float changeDirectionInterval = 2f; // интервал смены направления
    private float rotationSpeed = 5f; // скорость поворота

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

        // Разворачиваем объект по оси Y на 180 градусов
        transform.Rotate(180, 0, 0);

        // Отключаем анимацию, если есть компонент Animator
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
        // генерируем случайное направление движения
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        direction = new Vector3(randomX, 0, randomZ).normalized; // нормализуем, чтобы сохранять постоянную скорость
    }

    private void RotateFish()
    {
        // создаем целевой поворот в направлении движения
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // плавно поворачиваем рыбу к новому направлению
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
