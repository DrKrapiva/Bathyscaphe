using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivWeapon : DictionaryActivWeapon
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform gun;
    private List<Vector3> vectors ;
    private GameObject clon;
    private string nameWeapon;
    public static ActivWeapon Instance
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
    static private ActivWeapon _instance;
    
    public void CreateWeapon(GameObject prefabBullet, string weaponNameShort)
    {
        //nameWeapon = GetNameWeaponRarity(prefabBullet.name);
        nameWeapon = prefabBullet.name;
        ActWeapon actWeapon = GetActiveWeaponInfo(nameWeapon);

        if(actWeapon.vectors != null)
        {
            foreach(Vector3 vector in actWeapon.vectors)
            {
                StartCoroutine(CreateBullet(actWeapon, prefabBullet, vector, weaponNameShort));
            }
        }
        else StartCoroutine(CreateBullet(actWeapon, prefabBullet, Vector3.forward, weaponNameShort));

        
    }
    IEnumerator CreateBullet(ActWeapon actWeapon, GameObject prefabBullet, Vector3 vector, string weaponNameShort)
    {
        for (int i = 0; i < actWeapon.countBullet; i++)
        {
            clon = Instantiate(prefabBullet, gun.transform.position, Quaternion.identity);
            clon.name = nameWeapon;

            if (weaponNameShort == "Net")
            {
                Sprite arrowSprite = Resources.Load<Sprite>("UI/Arrows/targetPoint");
                ArrowPointer.Instance.StartArrowCoroutine( clon, arrowSprite);
            }

            if (clon.GetComponent<RocketMove>() != null)
                clon.GetComponent<RocketMove>().FillInfo(player.rotation * vector, actWeapon);
            yield return new WaitForSeconds(0.1f);
        }

    }
    public string GetNameWeaponRarity(string prefabBullet)
    {
        return GetComponent<SaveWeaponUpgrade>().GetWeaponLevelRarity(prefabBullet); 
    }
    public bool CheckIfKeyExists(string key)
    {
        return GetComponent<SaveWeaponUpgrade>().CheckIfKeyExists(key);
    }
    public void ChangeWeaponParam()
    {

    }
}
