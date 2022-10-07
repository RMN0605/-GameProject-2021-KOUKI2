using UnityEngine;

[CreateAssetMenu(fileName = "New_Card_Data", menuName = "Create_Card_Data")]
public class Unit_data : ScriptableObject
{
    public enum Unit_class    //ユニットのクラス
    {
        castle, //城
        short_range_unit,   //近距離ユニット
        long_range_unit,    //遠距離ユニット
        machine_unit,       //兵器ユニット
        strategy_card     //計略カード
    }
    [SerializeField,Header("カード名")]
    private string unit_name;

    [SerializeField, Header("カードクラス")]
    private Unit_class unit_type;

    [SerializeField, Header("カードプレハブ")]
    private GameObject unit_object;

    [Header("カードの値段")]
    public int Price;

    public string Unit_name { get { return unit_name; } private set { unit_name = value; } }
    public Unit_class unit_class { get { return unit_class; } private set { unit_class = value; } }
    public GameObject Unit_object { get { return unit_object; } private set { unit_object = value; } }
}
