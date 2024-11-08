using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject person;

    void Awake()
    {
        person = GameObject.Find("DemonGirlMesh");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(person.transform.position.x, person.transform.position.y + 4f, person.transform.position.z - 4f);
    }
}
