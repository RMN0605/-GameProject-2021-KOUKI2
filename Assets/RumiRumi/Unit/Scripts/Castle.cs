public class Castle : Unit
{
    protected override void Attack()
    {
        //ダメージを計算　　HP = 自分の攻撃力 - 敵の防御力
        target.GetComponent<Unit_model>().hp -= unit_manager.Attack_calculation
            (unit_model.attack_power, target.GetComponent<Unit_model>().defense_power, this.gameObject);
    }
}
