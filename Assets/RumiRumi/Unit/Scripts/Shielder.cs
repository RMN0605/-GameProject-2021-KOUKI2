public class Shielder : Unit
{
    protected override void Move()
    {
        transform.Translate(unit_model.move_speed, 0f, 0f);
    }

    protected override void Attack()
    {
        //�_���[�W���v�Z�@�@HP = �����̍U���� - �G�̖h���
        target.GetComponent<Unit_model>().hp -= unit_manager.Attack_calculation
            (unit_model.attack_power, target.GetComponent<Unit_model>().defense_power, this.gameObject);
    }
}
