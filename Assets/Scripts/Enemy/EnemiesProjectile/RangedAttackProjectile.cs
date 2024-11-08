using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackProjectile : MonoBehaviour
{
    private Vector3 direction;
    private float damage;
    private float speed;

    public void Initialize(Vector3 targetPosition, float damage, float speed, float lifetime)
    {
        direction = (targetPosition - transform.position).normalized;
        this.damage = damage;
        this.speed = speed;
        Destroy(gameObject, lifetime); // Уничтожить снаряд через заданное время, если он не достиг цели
    }

    private void Update()
    {
        

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = direction * speed;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("Дамаг от Sea Eal");
            // Наносим урон игроку при столкновении
            HeroHPController.Instance.TakeHit(damage);
            Destroy(gameObject);
        }
    }
}
