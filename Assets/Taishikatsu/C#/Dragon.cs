using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Unit

{
    [SerializeField]
    private UnityEngine.Animator anim;


    protected override void Attack()
    {
        //ダメージを計算　　HP = 自分の攻撃力 - 敵の防御力
        target.GetComponent<Unit_model>().hp -= unit_manager.Attack_calculation
            (unit_model.attack_power, target.GetComponent<Unit_model>().defense_power, this.gameObject);
        anim.SetBool("kougekiBool",true);
    }

    public void endAnimBool()
    {
        anim.SetBool("kougekiBool", false);
    }
}
