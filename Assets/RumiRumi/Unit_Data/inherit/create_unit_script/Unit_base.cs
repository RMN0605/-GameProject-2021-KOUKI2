using UnityEngine;

public class Unit_base : MonoBehaviour
{
    public enum Unit_type    //���j�b�g�̎��
    {
        castle,     //��
        cavalry,    //�R��
        shielder,   //����
        archer,     //�|��
        pikeman,    //����
        scout,      //�ˌ�
        catapult,   //���Ί�

//---------------------------------
        strategy,  //�v���n
    }

    [SerializeField, Header("���j�b�g�^�C�v")]
    private Unit_type unit_type;    //���j�b�g�̃^�C�v��I��

    [SerializeField, Header("���̃X�N���v�g�̃f�[�^")]
    private Unit_data unit_data;
    public Unit_data Unit_data => Unit_data;
}
