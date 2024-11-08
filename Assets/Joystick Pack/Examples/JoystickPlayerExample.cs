using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    private Animator anim;
    public Collider colWeapon;

    private bool fight = false;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        if (!fight)
            rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        
        if (direction != Vector3.zero && !fight)
        {
            anim.SetBool("run", true);
            float speed = Mathf.Abs(variableJoystick.Horizontal) + Mathf.Abs(variableJoystick.Vertical);
            anim.SetFloat("speed", speed);
            rb.MoveRotation(Quaternion.LookRotation(direction));
        }
        else
            anim.SetBool("run", false);
    }

    public void Attack()
    {
        anim.SetTrigger("attack");
        fight = true;
        //colWeapon.Raycast()
    }

    public void EndAttack()
    {
        Debug.Log("end");
        fight = false;
    }
}