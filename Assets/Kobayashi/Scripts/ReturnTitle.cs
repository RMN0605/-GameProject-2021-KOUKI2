public class ReturnTitle : Unit
{
    protected override void Attack()
    {
        //�_���[�W���v�Z�@�@HP = �����̍U���� - �G�̖h���
        target.GetComponent<Unit_model>().hp -= unit_manager.Attack_calculation
            (unit_model.attack_power, target.GetComponent<Unit_model>().defense_power, this.gameObject);
    }
}
