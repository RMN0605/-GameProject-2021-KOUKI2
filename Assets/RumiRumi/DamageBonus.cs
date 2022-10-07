using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBonus : MonoBehaviour
{
    [SerializeField] Unit_model obj;
    private int beforeHp;
    private void Start()
    {
        beforeHp = obj.hp;
    }
    private void Update()
    {
        if (beforeHp != obj.hp)
        {
            GeneralManager.instance.unitManager.UnitMoney2 += 2;
            beforeHp = obj.hp;
        }
    }
}
