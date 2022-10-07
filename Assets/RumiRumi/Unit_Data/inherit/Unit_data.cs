using UnityEngine;

[CreateAssetMenu(fileName = "New_Card_Data", menuName = "Create_Card_Data")]
public class Unit_data : ScriptableObject
{
    public enum Unit_class    //���j�b�g�̃N���X
    {
        castle, //��
        short_range_unit,   //�ߋ������j�b�g
        long_range_unit,    //���������j�b�g
        machine_unit,       //���탆�j�b�g
        strategy_card     //�v���J�[�h
    }
    [SerializeField,Header("�J�[�h��")]
    private string unit_name;

    [SerializeField, Header("�J�[�h�N���X")]
    private Unit_class unit_type;

    [SerializeField, Header("�J�[�h�v���n�u")]
    private GameObject unit_object;

    [Header("�J�[�h�̒l�i")]
    public int Price;

    public string Unit_name { get { return unit_name; } private set { unit_name = value; } }
    public Unit_class unit_class { get { return unit_class; } private set { unit_class = value; } }
    public GameObject Unit_object { get { return unit_object; } private set { unit_object = value; } }
}
