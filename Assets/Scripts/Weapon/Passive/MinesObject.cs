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
            //эффект взрыва
            Explode(); // Запускаем эффект взрыва
            
            Destroy(gameObject, 0.8f);
            
        }
    }
    public void Mine()
    {
        // Получаем все объекты с компонентом Enemy в радиусе scanRadius от текущей позиции 
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
    //если долго никто не касался, взрываться
    IEnumerator DestroyMine()
    {
        yield return new WaitForSeconds(destroyTime);
        MinesController.Instance.ChangeAmount();
        //эффект взрыва
        Explode(); // Запускаем эффект взрыва
        Destroy(gameObject, 0.8f);
       
    }
    private void Explode()
    {
        if (explosionEffect != null)
        {
            Debug.Log("Explode");
            
            explosionEffect.Play(); // Запускаем эффект взрыва
        }
    }
    
}
