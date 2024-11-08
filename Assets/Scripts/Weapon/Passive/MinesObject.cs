using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesObject : MonoBehaviour
{
    private Mines _mines;
    private int destroyTime = 10;
    private Coroutine coroutineDesctroy;
    [SerializeField] private ParticleSystem explosionEffect;
    
    public void FillInfo(Mines mines)
    {
        
        _mines = mines;

        coroutineDesctroy = StartCoroutine(DestroyMine());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.gameObject.CompareTag("Enemy"))
        {
            Mine();
            MinesController.Instance.ChangeAmount();
            //������ ������
            Explode(); // ��������� ������ ������
            
            Destroy(gameObject, 0.8f);
            
        }
    }
    public void Mine()
    {
        // �������� ��� ������� � ����������� Enemy � ������� scanRadius �� ������� ������� 
        Collider[] colliders = Physics.OverlapSphere(transform.position, _mines.Size);
        
        foreach (Collider collider in colliders)
        {
            if (collider.transform.parent != null && collider.transform.parent.CompareTag("Enemy"))
            {
                EnemyActions enemyComponent = collider.transform.parent.gameObject.GetComponent<EnemyActions>();
                enemyComponent.TakeDamage((int)_mines.Damage);

            }
        }
    }
    //���� ����� ����� �� �������, ����������
    IEnumerator DestroyMine()
    {
        yield return new WaitForSeconds(destroyTime);
        MinesController.Instance.ChangeAmount();
        //������ ������
        Explode(); // ��������� ������ ������
        Destroy(gameObject, 0.8f);
       
    }
    private void Explode()
    {
        if (explosionEffect != null)
        {
            Debug.Log("Explode");
            
            explosionEffect.Play(); // ��������� ������ ������
        }
    }
    
}
