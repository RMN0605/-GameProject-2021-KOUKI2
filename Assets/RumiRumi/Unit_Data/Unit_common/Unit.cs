using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Unit_move    //ユニットの行動
{
    Moving_method,   //移動
    Battle, //攻撃  
}

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public Unit_move unit_move;  //ユニットが今何の行動をしているか

    protected Unit_model unit_model;  //modelにかいてあるものを使えるようにするよ
    protected Unit_manager unit_manager;  // //managerにあるダメージ計算を使えるようにするよ

    [Header("攻撃対象")]
    public GameObject target = null;  //攻撃するターゲットを格納
    [Header("武器がある場合はこちらに格納")]
    public Weapon attackZone = null;
    [HideInInspector]
    public bool isCool_down;    //クールダウンしてるかみるYO!
    private bool isAttack_reserve;  //攻撃予約を行っているかをみるYO!

    GameObject _dragon;
    ChangeDragon changeDragon;
    GameObject _winorlose;
    WinOrLose winOrLose;

    //------------------------------------------------------------------------------

    private void Awake()
    {
        unit_model = gameObject.GetComponent<Unit_model>();//自身にアタッチされているUnit_modelを探すYO!
        unit_manager = GameObject.FindObjectOfType<Unit_manager>(); //hierarchyにあるUnit_managerを探すYO！
    }

    private void Start()
    {
        isCool_down = false; //クールダウン中はAttack_speedの数値分待機しています。
        isAttack_reserve = false;

        if (gameObject.tag == "Dragon1" || gameObject.tag == "Dragon2" || gameObject.tag == "Castle1" || gameObject.tag == "Castle2")
        {
            _dragon = GameObject.Find("ChangeDragon");
            changeDragon = _dragon.GetComponent<ChangeDragon>();
            _winorlose = GameObject.Find("WinLose");
            winOrLose = _winorlose.GetComponent<WinOrLose>();
        }
    }


    //------------------------------------------------------------------------------

    private void FixedUpdate()
    {
        //洗脳されたら攻撃対象をなくす
        if (target != null)
        {
            if (gameObject.tag == "Unit1" && (target.gameObject.tag == "Unit1" || target.gameObject.tag == "Castle1" || target.gameObject.tag == "Dragon1"))
                target = null;
            else if (gameObject.tag == "Unit2" && (target.gameObject.tag == "Unit2" || target.gameObject.tag == "Castle2" || target.gameObject.tag == "Dragon2"))
                target = null;
        }

        //武器の攻撃範囲に入ったらターゲットに固定
        if (target == null && attackZone != null)
        {
            if (attackZone.weaponTarget != null)
                target = attackZone.weaponTarget;
        }

        //体力が０以下ならオブジェクトを削除
        if (unit_model.hp <= 0)
        {
            //RiP
            Destroy(this.gameObject);
            if (CompareTag("Castle1"))
            {
                changeDragon.SummonDragon1();
            }
            else if (CompareTag("Castle2"))
            {
                changeDragon.SummonDragon2();
            }
            else if (CompareTag("Dragon1"))
            {
                winOrLose.Win_Or_Lose(false);
            }
            else if (CompareTag("Dragon2"))
            {
                winOrLose.Win_Or_Lose(true);
            }
        }

        else if (unit_model.hp > 0)
        {
            switch (unit_move)
            {
                case Unit_move.Moving_method:
                    Moving_method();
                    break;
                case Unit_move.Battle:
                    if (!isAttack_reserve)//連続で予約がされないようにしている
                    {
                        unit_manager.AddAttack(GetComponent<Unit>());
                        isAttack_reserve = true;    //攻撃予約しましたよん
                    }
                    break;
                default:
                    Debug.Log("ちゃんと動作してないよん(キャラの行動)");
                    break;
            }
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <param name="cool_time">Attack_speed(攻撃後から次の攻撃が呼ばれるまで。)</param>
    /// <returns></returns>
    public IEnumerator Battle(float cool_time)
    {
        if (isCool_down != true) //クールタイムが終了していることを確認
        {
            if (target != null) //攻撃対象が決まっているかを確認
            {
                Attack();
            }
            isCool_down = true;  //攻撃後のクールタイムを開始を指示
        }

        if (target == null)  //攻撃対象を倒したか確認
        {
            unit_move = Unit_move.Moving_method;   //移動開始
        }

        yield return new WaitForSeconds(cool_time); //クールタイム計測開始

        //リセット
        isCool_down = false; //攻撃後の待機開始
        isAttack_reserve = false;   //攻撃が完了し予約を解除したよ！
    }

    /// <summary>
    /// 移動
    /// </summary>
    protected virtual void Moving_method()
    {
        Move();
        if (target != null)
        {
            unit_move = Unit_move.Battle;   //攻撃開始
        }
    }

    protected virtual void Move()
    {
        //各ユニットごとに書き換えてね
    }
    protected virtual void Attack()
    {
        //各ユニットごとに書き換えてね
    }


//------------------------------------------------------------------------------

    protected virtual void OnCollisionEnter2D(Collision2D co)
    {
        if ((gameObject.CompareTag("Unit1") || gameObject.CompareTag("Dragon1"))&& target == null)
        {
            if (co.collider.tag == ("Unit2") || co.collider.tag == ("Castle2") || co.collider.tag == ("Dragon2"))
                target = co.gameObject;//攻撃対象を選択
        }
        else if ((gameObject.CompareTag("Unit2") || gameObject.CompareTag("Dragon2")) && target == null)
        {
            if (co.collider.tag == ("Unit1") || co.collider.tag == ("Castle1") || co.collider.tag == ("Dragon1"))
                target = co.gameObject;     //攻撃対象を選択
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D co)
    {
        if (target == null)
        {
            if (gameObject.CompareTag("Unit1") || gameObject.CompareTag("Dragon1"))
            {
                if (co.collider.tag == ("Unit2") || co.collider.tag == ("Castle2") || co.collider.tag == ("Dragon2"))
                    target = co.gameObject;//攻撃対象を選択
            }
            else if (gameObject.CompareTag("Unit2") || gameObject.CompareTag("Dragon2"))
            {
                if (co.collider.tag == ("Unit1") || co.collider.tag == ("Castle1") || co.collider.tag == ("Dragon1"))
                    target = co.gameObject;     //攻撃対象を選択
            }
        }
    }
}
