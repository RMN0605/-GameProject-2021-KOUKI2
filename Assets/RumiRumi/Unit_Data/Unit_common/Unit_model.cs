using UnityEngine;

public class Unit_model : Unit_base
{
    [Header("体力")]
    public int hp;              //ユニットの体力
    [Header("攻撃力")]
    public int attack_power;    //ユニットの攻撃力
    [Header("攻撃ディレイ時間(攻撃アニメーションの時間)/秒")]
    public float attack_delay;  //ユニットの攻撃する際の構えてからあたるまでのディレイ時間(アニメーション等で使用する際にどうぞ)
    [Header("攻撃のクールタイム(次の攻撃までの時間)/秒")]
    public int attack_cool_time;    //ユニットの攻撃後の待機時間
    [Header("防御力")]
    public int defense_power;   //ユニットの防御力
    [Header("移動速度")]
    public float move_speed;  //ユニットの移動速度
    [HideInInspector]
    public float now_attack_delay;    //ディレイ経過時間計算用 / 使用する場所はUnit_Manager
}
