using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetObject : MonoBehaviour
{
    private Magnet _magnet;

    public void FillInfo(Magnet magnet)
    {
        _magnet = magnet;

        transform.localScale = new Vector3(_magnet.Size, _magnet.Size, _magnet.Size);

        Destroy(gameObject, _magnet.DestroyTime);
    }

    private void OnTriggerEnter(Collider other)

    {
        if (other.transform.parent != null && other.transform.parent.CompareTag("PointExp"))
        {
            other.transform.parent.gameObject.GetComponent<PointExp>().StartCoroutineMoveToObjectAndDestroy();
        } 
        if (other.transform.parent != null && other.transform.parent.CompareTag("Bonus"))
        {
            other.transform.parent.gameObject.GetComponent<ShellBonus>().StartCoroutineMoveToObject();
        }

    }

}
