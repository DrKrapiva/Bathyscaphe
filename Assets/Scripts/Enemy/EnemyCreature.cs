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
        //Debug.Log("������� ");
        /*if (gameObject.GetComponent<EnemyForEscortItem>() != null)
        {
            Debug.Log("��������� ����");
            if (other.gameObject.GetComponent<EscortItem>() != null)
            {
                Debug.Log(gameObject.name + " ������� ����� � ������ (����������� ������)");
                // �������������� ������ ��� ��������
            }
        }
        else
        {
            if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
            {
                if (isFrozen)
                {
                    //Debug.Log("������� ����, �� � ������");
                }
                else
                {
                    Debug.Log(gameObject.name + " ������� ���� � ������");//�����
                                                                          // HeroHPController.Instance.TakeHit(Attack());
                    StartAttacking();
                }
            }
        }*/
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
        {
           // Debug.Log("������� hero");
            if (isFrozen)
            {
                //Debug.Log("������� ����, �� � ������");
            }
            else
            {
                //Debug.Log(gameObject.name + " ������� ���� � ������");//�����
                                                                      // HeroHPController.Instance.TakeHit(Attack());
                StartAttacking();
            }
        }
        if (other.gameObject.GetComponent<BulletTouch>() != null)
        {
            //Debug.Log("������� ����");
            TakeDamage(other.gameObject.GetComponent<BulletTouch>().Actions());
        }
        if (other.gameObject.GetComponent<Explosion>() != null)
        {
            //Debug.Log("������� �����");
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
    
    public abstract void TakeDamage(float damage);// ���� protected

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
        //�������
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

    // �������� ��� ����������� �����
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
