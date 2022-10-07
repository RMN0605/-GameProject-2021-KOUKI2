using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponTarget;

    protected void OnCollisionEnter2D(Collision2D co)
    {
        if (gameObject.CompareTag("Unit1") && weaponTarget == null)
        {
            if (co.collider.tag == ("Unit2") || co.collider.tag == ("Castle2"))
                weaponTarget = co.gameObject;//UŒ‚‘ÎÛ‚ğ‘I‘ğ
        }
        else if (gameObject.CompareTag("Unit2") && weaponTarget == null)
        {
            if (co.collider.tag == ("Unit1") || co.collider.tag == ("Castle1"))
                weaponTarget = co.gameObject;     //UŒ‚‘ÎÛ‚ğ‘I‘ğ
        }
    }

    protected void OnCollisionStay2D(Collision2D co)
    {
        if (weaponTarget == null)
        {
            if (gameObject.CompareTag("Unit1"))
            {
                if (co.collider.tag == ("Unit2") || co.collider.tag == ("Castle2"))
                    weaponTarget = co.gameObject;//UŒ‚‘ÎÛ‚ğ‘I‘ğ
            }
            else if (gameObject.CompareTag("Unit2"))
            {
                if (co.collider.tag == ("Unit1") || co.collider.tag == ("Castle1"))
                    weaponTarget = co.gameObject;     //UŒ‚‘ÎÛ‚ğ‘I‘ğ
            }
        }
    }
}
