using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private List<string> actualActiveWeapon;
    private int index;
    private int decreaseCooldownPercent;
    [SerializeField] private GameObject panelChest;
    private Transform canvas;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Сундук сработал триггер: " + transform.parent.gameObject.name);
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null)
        {
            //стоп корутина
            ArrowPointer.Instance.StopArrowCoroutine(transform.parent.gameObject);
            DeclineCoolDown();
            CreateChestPanel();
            Debug.Log("Удален сундук: " + transform.parent.gameObject.name);
            Destroy(gameObject);
        }
    }
    private int RandomCoin()
    {
        return Random.Range(200, 1501);
    }
    private string PickWeaponNameForChange()
    {
        actualActiveWeapon = SaveGame.Instance.LoadSelectedWeaponNames();
        

        index = Random.Range(0, 3);
        return actualActiveWeapon[index];
    }
    private void DeclineCoolDown()
    {
        decreaseCooldownPercent = Random.Range(10, 31);

        WeaponButtonController.Instance.ChangeCoolDown(decreaseCooldownPercent, PickWeaponNameForChange());
    }
    private void CreateChestPanel()
    {
        canvas = GameObject.FindWithTag("Canvas").transform;
        GameObject panel = Instantiate(panelChest, canvas, false);
        Time.timeScale = 0f;
        panel.GetComponent<ChestPanel>().FillInfo(RandomCoin(), PickWeaponNameForChange(), decreaseCooldownPercent);
    }
}
