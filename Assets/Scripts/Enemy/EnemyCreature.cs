using System.Collections;
using UnityEngine;

public abstract class EnemyCreature : MonoBehaviour
{
    //protected Enemy _enemy;
    protected bool isFrozen = false;
    protected bool isAttacking = false;
    protected Transform hero;



    protected virtual void OnTriggerEnter(Collider other) 
    {
        //Debug.Log("касаюсь ");
        /*if (gameObject.GetComponent<EnemyForEscortItem>() != null)
        {
            Debug.Log("особенный враг");
            if (other.gameObject.GetComponent<EscortItem>() != null)
            {
                Debug.Log(gameObject.name + " касаюсь лодки и атакую (специальная логика)");
                // Дополнительная логика или действия
            }
        }
        else
        {
            if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
            {
                if (isFrozen)
                {
                    //Debug.Log("касаюсь хиро, но я замерз");
                }
                else
                {
                    Debug.Log(gameObject.name + " касаюсь хиро и атакую");//атака
                                                                          // HeroHPController.Instance.TakeHit(Attack());
                    StartAttacking();
                }
            }
        }*/
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
        {
           // Debug.Log("касаюсь hero");
            if (isFrozen)
            {
                //Debug.Log("касаюсь хиро, но я замерз");
            }
            else
            {
                //Debug.Log(gameObject.name + " касаюсь хиро и атакую");//атака
                                                                      // HeroHPController.Instance.TakeHit(Attack());
                StartAttacking();
            }
        }
        if (other.gameObject.GetComponent<BulletTouch>() != null)
        {
            //Debug.Log("касаюсь пули");
            TakeDamage(other.gameObject.GetComponent<BulletTouch>().Actions());
        }
        if (other.gameObject.GetComponent<Explosion>() != null)
        {
            //Debug.Log("касаюсь взрыв");
            TakeDamage(other.gameObject.GetComponent<Explosion>().Actions());
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
        {
            StopAttacking();
        }
    }
    protected virtual void FillInfo()
    {
        //_enemy = EnemyController.Instance.EnemyInfo(gameObject.name);
        //Debug.Log("Enemy name " + gameObject.name);
        hero = GameObject.Find("Player").transform;

        StartCoroutine(CheckDistanceToHero());
    }
    
    public abstract void TakeDamage(float damage);// было protected

    public abstract void EnemyDeath();
    
    private IEnumerator CheckDistanceToHero()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(2);

            float dist = Vector3.Distance(transform.position, hero.position);
            if (dist > 55)
            {
                Death();
            }
        }
    }
    public virtual void Death()
    {
        //дестрой
        Destroy(gameObject);
    }
    public abstract float Attack();

    public abstract IEnumerator CoolDownAttack();
    
    protected void StartAttacking()
    {
        //Debug.Log("StartAttacking isAttacking " + isAttacking);
        if (!isAttacking) 
        {
            //Debug.Log("StartCoroutine");
            isAttacking = true; 
            StartCoroutine(ContinuousAttack());
        }
    }

    protected void StopAttacking()
    {
        //Debug.Log("StopAttacking isAttacking "+ isAttacking);
        isAttacking = false; 
        StopCoroutine(ContinuousAttack()); 
    }

    // Корутина для непрерывной атаки
    protected virtual IEnumerator ContinuousAttack()
    {
        Debug.Log("ContinuousAttack!!!!!!!!!!!!!!!!!!!");

        while (isAttacking)
        {
            HeroHPController.Instance.TakeHit(Attack());
            //Attack();
            yield return new WaitForSeconds(1f); 
        }
    }


}
