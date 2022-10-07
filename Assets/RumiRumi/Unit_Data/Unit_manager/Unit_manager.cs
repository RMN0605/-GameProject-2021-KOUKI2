using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_manager : MonoBehaviour
{
    [HideInInspector]
    public List<Unit> attackInfos = new List<Unit>();

    [SerializeField]
    public static List<Unit_data> unit_list { get; private set; }
    [SerializeField]
    public static List<Unit_data> strategy_list { get; private set; }
    private static GameObject unit;
    [Header("プレイヤー１の金デース")]
    public int UnitMoney = 50;
    [Header("プレイヤー２の金デース")]
    public int UnitMoney2 = 50;

    private void Awake()
    {
        //ユニットのデータはすべてここにリスト化されるよ。
        Unit_data[] dataFiles = Resources.LoadAll<Unit_data>("Unit_data");
        //Unit_dataをロード
        unit_list = new List<Unit_data>(dataFiles);
        Unit_data[] dataFiles2 = Resources.LoadAll<Unit_data>("Strategy_data");
        strategy_list = new List<Unit_data>(dataFiles2);
    }
    private void Start()
    {
        UnitMoney = 50;
        UnitMoney2 = 50;
    }

    public void AddAttack(Unit us)
    {
        attackInfos.Add(us);
    }

    /// <summary>
    /// これはなんぞや： 攻撃に関する衝突回避処理
    /// 実装理由：      同時に攻撃が発生した場合のエラー回避用
    /// やっていること：攻撃範囲に入った者から攻撃を予約して攻撃時間が来たら攻撃をする。
    /// </summary>
    public void FixedUpdate()
    {
        float elapsed_time = Time.fixedDeltaTime;    //経過時間の取得

        for (int i = 0; i < attackInfos.Count; i++)
        {
            if (attackInfos[i] != null && attackInfos[i].GetComponent<Unit>().isCool_down == false)
            {
                attackInfos[i].GetComponent<Unit_model>().now_attack_delay += elapsed_time;

                //攻撃ディレイが規定時間に達した場合読み込まれるよん / 攻撃のアニメーションが使われている際にここの計算を使用されるよ。
                if (attackInfos[i].GetComponent<Unit_model>().now_attack_delay > attackInfos[i].GetComponent<Unit_model>().attack_delay)
                {
                    //攻撃の処理
                    StartCoroutine(attackInfos[i].GetComponent<Unit>().Battle(attackInfos[i].GetComponent<Unit_model>().attack_cool_time));   //攻撃の呼び出し

                    //リセット
                    attackInfos[i].GetComponent<Unit_model>().now_attack_delay = 0; //攻撃待機時間を０にするよ
                    //リストから削除
                    attackInfos.Remove(attackInfos[i]);
                }
            }
            else if(attackInfos[i]　== null) //攻撃予約をしたユニットが死んでいるか確認
            {
                attackInfos.Remove(attackInfos[i]); //nullだったら破棄するよ
            }
        }
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    /// <param name="attack_power">攻撃する側の攻撃力</param>
    /// <param name="defense_power">攻撃を受ける側の防御力</param>
    /// <returns></returns>
    public int Attack_calculation(int attack_power,int defense_power,GameObject attack_name)
    {
        //Debug.Log((attack_name)+"が"+(attack_power - defense_power)+"ダメージを与えた");
        if (attack_power > defense_power)
        {
            //Debug.Log("以下のダメージが入ったよ");
            //Debug.Log(attack_power - defense_power);
            return attack_power - defense_power;
        }
        else if (attack_power < defense_power)
        {
            //攻撃力がディフェンス力の方が上回っていたばあい1ダメージ与えるよ君
            return 1;
        }
        else if (attack_power == defense_power)  
        {
            return 0;
        }
        else //何らかのエラーが起きた場合はこちらへ～
        {
            Debug.Log("ここでデバッグログが流れるのはおかしいよ（ダメージ計算）");
            return 0;
        }
    }

    /// <summary>
    /// ユニットの生成関数
    /// </summary>
    /// <param name="unit_data">生成したいオブジェクトデータ</param>
    /// <param name="vec">生成したい場所</param>
    /// /// <param name="player">生成を求めているプレイヤーの判別、１なら左、２なら右 、３なら計略カード</param>
    /// <returns></returns>
    public static GameObject Instantiate_unit(Unit_data unit_data , Vector3 vec ,int player)
    {
        if (player == 1)
        {
            unit_data.Unit_object.tag = ("Unit1");  //左のプレイヤー側のユニットであるタグをつける
            unit = Instantiate(unit_data.Unit_object, vec, Quaternion.identity);
        }
        else if (player == 2)
        {
            unit_data.Unit_object.tag = ("Unit2");  ////右のプレイヤー側のユニットであるタグをつける
            unit = Instantiate(unit_data.Unit_object, vec, new Quaternion(0,180,0,1));  //右プレイヤー側のユニットの場合は
        }
        else if(player == 3)
        {
            unit = Instantiate(unit_data.Unit_object, vec, Quaternion.identity);
        }
        return unit;
    }
}
