using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooltime : MonoBehaviour
{
    [Header("クールタイム用Imsge")]
    public Image UIobj; //クールタイム用イメージ

    [Header("必要資金設定")]
    public int NeedMoney = 0; //必要資金の金額設定

    [Header ("クールタイム")]
    public float countTime = 5.0f;//クールタイム秒数

    [Header("クールタイム開始flag")]
    public bool OnCoolTime = false;
    BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        UIobj.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤー１or２の資金が必要資金より低く、召喚フラグが負の時
        if (tag == "UnitCard1" || tag == "StrategyCard1")
        {
            if (GeneralManager.instance.unitManager.UnitMoney < NeedMoney && OnCoolTime == false)
            {
                //表示
                UIobj.enabled = true;
                bc.enabled = false;
            }
            else if (UIobj.enabled == true)
            {
                UIobj.enabled = false;
                bc.enabled = true;
            }
        }
        else if (tag == "UnitCard2" || tag == "StrategyCard2")
        {
            if (GeneralManager.instance.unitManager.UnitMoney2 < NeedMoney && OnCoolTime == false)
            {
                //表示
                UIobj.enabled = true;
                bc.enabled = false;
            }
            else if (UIobj.enabled == true)
            {
                UIobj.enabled = false;
                bc.enabled = true;
            }
        }
        //召喚され、フラグが正になったら
        //Drag.cs内のドラッグ終了処理内にフラグの切り替え処理がある
        if (OnCoolTime == true)
        {
            //表示
            UIobj.enabled = true;
            bc.enabled = false;
            CoolTime();
        }
    }

    /// <summary>
    /// クールタイム
    /// </summary>
    void CoolTime()
    {
        //設定したクールタイムから一秒ごと減少
        UIobj.fillAmount -= 1.0f / countTime * Time.deltaTime;
        //fillAmountが0になったら
        if (UIobj.fillAmount == 0)
        {
            UIobj.fillAmount = 1;//fillAmountを１に戻して
            OnCoolTime = false;//フラグを負に
            bc.enabled = true;
            UIobj.enabled = false;//非表示
        }

    }
    
}

