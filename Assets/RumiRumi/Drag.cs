using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Image))]
public class Drag: MonoBehaviour
{
//-----------------------------------------------------------------
//＜変数＞
    #region public
    [Header("リストナンバー")]
    public int summon_number;   //召喚or発動するカードのリストから使うカードの番号を記入
    [Header("プレイヤー１ならFalse,プレイヤー２ならTrue")]
    public bool isPlayer;       //プレイヤーの識別
    [Header("ユニットならFalse,計略カードならTrue")]
    public bool isCardType; //ユニットと計略カードの識別
    [Header("魔法を使う際に範囲がある場合はこれをTrueに")]
    #region
    [Header("-------------------------------")]
    [Header("必要な場合は下記を設定してください")]
    [Header("-------------------------------")]
    [Space(10)]
    #endregion
    public bool isEffectRange = false;  //計略カードに範囲があった際に使用
    [Header("EffectRangeがある場合は格納")]
    public GameObject childGameObject;  //isEffectRangeをtrueにした際に使用、範囲オブジェクトを格納
    #endregion

    #region private
    public GameObject zone;    //自オブジェクトの下にある物が何かを判定する、召喚できる場所、発動できる場所かを判定する際に使用する
    private GameObject unit_parent; //生成時にどこの子として出すか
    //ドラッグ前の位置
    private Vector2 startPos;   //オブジェクトの開始位置の設定
    //カーソルの取得
    private GameObject cursor;  //自オブジェクトの上にカーソルが入っているか、また入っていた場合そのカーソルの座標取得の際に使用
    private bool isSummonZone = false;  //召喚、生成できる場所に自オブジェクトがあるか確認
    #endregion

    private Cooltime cooltime; //半田：Cooltime.csに召喚確認フラグがあるので追加 2 / 5

//-----------------------------------------------------------------
//＜操作＞
    #region ゲームパッド操作
    public void Start()
    {
        unit_parent = GameObject.Find("Unit_generation_location");  //Hierarchyにある格納する場所に対する検索
        cooltime = GetComponent<Cooltime>();
    }
    /// <summary>
    /// ドラッグ開始
    /// </summary>
    public void OnBeginDrag()
    {
        if (isEffectRange)
            childGameObject.SetActive(true);    //当たり判定
        startPos = transform.position;  //マウスをクリックした最初の場所を保管                               
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);  //色を薄くする
    }
    /// <summary>
    /// ドラッグ中
    /// </summary>
    public void OnDrag()
    {
        if (cursor == null)
            return;
        transform.position = cursor.transform.position;   //カーソルに移動
    }

    /// <summary>
    /// ドラッグ終了
    /// </summary>
    public void OnEndDrag()
    {
        if (isEffectRange)
            childGameObject.SetActive(false);    //当たり判定

        //色を元に戻す（白色にする）
        GetComponent<Image>().color = Color.white;  

        //ユニットカードだった場合
        if (!isCardType)
        {
            if (GeneralManager.instance.unitManager.UnitMoney >= Unit_manager.unit_list[summon_number].Price && isSummonZone == true)    //生成できるコインを持っているか、生成できる場所であるか
            {
                if (!isPlayer && zone.gameObject.CompareTag("SummonZone_p1"))
                {
                    GeneralManager.instance.unitManager.UnitMoney -= Unit_manager.unit_list[summon_number].Price;
                    var unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[summon_number], this.transform.position, 1);
                    unit_child.transform.parent = unit_parent.transform;

                    cooltime.OnCoolTime = true; //半田：召喚されたことを確認するフラグを追加 2 / 5
                }
            }
            if(GeneralManager.instance.unitManager.UnitMoney2 >= Unit_manager.unit_list[summon_number].Price && isSummonZone == true)
            {
                if (isPlayer && zone.gameObject.CompareTag("SummonZone_p2"))
                {
                    GeneralManager.instance.unitManager.UnitMoney2 -= Unit_manager.unit_list[summon_number].Price;
                    var unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[summon_number], this.transform.position, 2);
                    unit_child.transform.parent = unit_parent.transform;

                    cooltime.OnCoolTime = true; //半田：召喚されたことを確認するフラグを追加 2 / 5
                }
            }
        }
        //計略カードだった場合
        else if (isCardType)
        {
            if(zone == null)
            {
                transform.position = startPos;  //元の場所に戻すよ
            }
            else if (zone.gameObject.CompareTag("StrategyStage"))
            {
                if (gameObject.tag == "StrategyCard1")  //カードの生成
                {
                    if (GeneralManager.instance.unitManager.UnitMoney >= Unit_manager.strategy_list[summon_number].Price)
                    {
                        GeneralManager.instance.unitManager.UnitMoney -= Unit_manager.strategy_list[summon_number].Price;
                        var unit_child = Unit_manager.Instantiate_unit(Unit_manager.strategy_list[summon_number], this.transform.position, 3);

                        cooltime.OnCoolTime = true; //半田：召喚されたことを確認するフラグを追加 2 / 5
                    }
                }
                else if (gameObject.tag == "StrategyCard2")
                {
                    if (GeneralManager.instance.unitManager.UnitMoney2 >= Unit_manager.strategy_list[summon_number].Price)
                    {
                        GeneralManager.instance.unitManager.UnitMoney2 -= Unit_manager.strategy_list[summon_number].Price;
                        var unit_child = Unit_manager.Instantiate_unit(Unit_manager.strategy_list[summon_number], this.transform.position, 3);

                        cooltime.OnCoolTime = true; //半田：召喚されたことを確認するフラグを追加 2 / 5
                    }
                }
            }
           

        } 
        transform.position = startPos;  //元の場所に戻すよ
    }
    #endregion

    #region マウス操作
    public void OnMouseCursorBeginDrag(BaseEventData eventData)
    {
        if (isEffectRange)
            childGameObject.SetActive(true);    //当たり判定
        startPos = transform.position;  //マウスをクリックした最初の場所を保管                               
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);  //色を薄くする
    }
    /// <summary>
    /// ドラッグ中
    /// </summary>
    public void OnMouseCursorDrag(BaseEventData eventData)
    {
        Vector2 objectPoint = Camera.main.WorldToScreenPoint(transform.position);   //objectの位置をワールド座標からスクリーン座標に変換して、objectPointに格納
        Vector2 pointScreen = new Vector2(Input.mousePosition.x, Input.mousePosition.y);   //マウスの位置を保存   
        Vector2 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);   //オブジェクトの現在位置を、スクリーン座標からワールド座標に変換して、pointWorldに格納
        transform.position = pointWorld;    //オブジェクトの位置を、pointWorldにする  
    }
    /// <summary>
    /// ドラッグ終了
    /// </summary>
    public void OnMouseCursorEndDrag(BaseEventData eventData)
    {
        if (isEffectRange)
            childGameObject.SetActive(false);    //当たり判定

        //色を元に戻す（白色にする）
        GetComponent<Image>().color = Color.white;

        //計略カードだった場合
        if (isCardType)
        {
            if (zone.gameObject.CompareTag("StrategyStage")) //      マウスのレイが範囲以内に当たってる場合
            {
                if (GeneralManager.instance.unitManager.UnitMoney > Unit_manager.strategy_list[summon_number].Price)
                {
                    GeneralManager.instance.unitManager.UnitMoney -= Unit_manager.strategy_list[summon_number].Price;
                    var unit_child = Unit_manager.Instantiate_unit(Unit_manager.strategy_list[summon_number], this.transform.position, 3);
                }
            }
        }
        //ユニットカードだった場合
        else if (!isCardType)
        {
            if (GeneralManager.instance.unitManager.UnitMoney > Unit_manager.unit_list[summon_number].Price && isSummonZone == true)    //生成できるコインを持っているか、生成できる場所であるか
            {
                if (!isPlayer && zone.gameObject.CompareTag("SummonZone_p1"))
                {
                    GeneralManager.instance.unitManager.UnitMoney -= Unit_manager.unit_list[summon_number].Price;
                    var unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[summon_number], this.transform.position, 1);
                    unit_child.transform.parent = unit_parent.transform;
                }
                else if (isPlayer && zone.gameObject.CompareTag("SummonZone_p2"))
                {
                    GeneralManager.instance.unitManager.UnitMoney -= Unit_manager.unit_list[summon_number].Price;
                    var unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[summon_number], this.transform.position, 2);
                    unit_child.transform.parent = unit_parent.transform;
                }
            }
        }


        transform.position = startPos;  //元の場所に戻すよ
    }
    #endregion

//-----------------------------------------------------------------
//＜当たり判定＞
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlayer)
        {
            if (collision.CompareTag("Cursor1"))
            {
                cursor = collision.gameObject;  //カーソルが上にきたら格納
            }
        }
        else if (isPlayer)
        {
            if (collision.CompareTag("Cursor2"))
            {
                cursor = collision.gameObject;  //カーソルが上にきたら格納
            }
        }

        if (isCardType) //カードタイプの判別
        {
            if (collision.CompareTag("StrategyStage"))
            {
                zone = collision.gameObject;    //生成できる場所を格納
                isSummonZone = true;    //生成できる場所であることを確認
            }
        }
        else　//カードタイプの判別
        {
            if (!isPlayer)
            {
                if (collision.CompareTag("SummonZone_p1"))
                {
                    zone = collision.gameObject;    //生成できる場所を格納
                    isSummonZone = true;    //生成できる場所であることを確認
                }
            }
            else if (isPlayer)
            {
                if (collision.CompareTag("SummonZone_p2"))
                {
                    zone = collision.gameObject;    //生成できる場所を格納
                    isSummonZone = true;    //生成できる場所であることを確認
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isCardType)
        {
            if (collision.CompareTag("StrategyStage"))
            {
                zone = null;
                isSummonZone = false;
            }
        }
        else
        {
            if (!isPlayer)
            {
                if (collision.CompareTag("SummonZone_p1"))
                {
                    zone = null;
                    isSummonZone = false;
                }
            }
            else if (isPlayer)
            {
                if (collision.CompareTag("SummonZone_p2"))
                {
                    zone = null;
                    isSummonZone = false;
                }
            }
        }
        if (collision.CompareTag("Cursor1"))
        {
            cursor = null;
        }
        else if (collision.CompareTag("Cursor2"))
        {
            cursor = null;
        }
    }
}
