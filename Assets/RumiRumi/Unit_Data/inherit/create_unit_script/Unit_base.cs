using UnityEngine;

public class Unit_base : MonoBehaviour
{
    public enum Unit_type    //ユニットの種類
    {
        castle,     //城
        cavalry,    //騎兵
        shielder,   //盾兵
        archer,     //弓兵
        pikeman,    //槍兵
        scout,      //斥候
        catapult,   //投石器

//---------------------------------
        strategy,  //計略系
    }

    [SerializeField, Header("ユニットタイプ")]
    private Unit_type unit_type;    //ユニットのタイプを選択

    [SerializeField, Header("このスクリプトのデータ")]
    private Unit_data unit_data;
    public Unit_data Unit_data => Unit_data;
}
