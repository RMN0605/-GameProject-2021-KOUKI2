using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class moto_cursor : MonoBehaviour
{
    [SerializeField, Header("カーソルの移動速度")]
    private int Speed;
    [Header("プレイヤー１ならFalse,プレイヤー２ならTrue")]
    public bool isPlayer;       //プレイヤーの識別

    public const int Player1Num = 0;
    public const int Player2Num = 1;

    [SerializeField, Header("ページ変更ボタン")]
    private GameObject pageChange;
    private Vector3 move;   //カーソル移動の際の座標
    private Vector2 playerPos; //現在の座標
    private bool isUnderObjectMove; //カードを動かしているか
    private bool isPageChange = false;
    public GameObject underObject;  //下にあるオブジェクトが何か
    private GameObject RootObject;

    /// <summary>
    /// スティック操作
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// ボタンを押した際の処理
    /// </summary>
    /// <param name="context"></param>
    public void OnPageChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Fire");
        }
    }
    private void Start()
    {

        if (!isPlayer)
        {
            RootObject = GameObject.Find("Display1Canvas");
            pageChange = GameObject.Find("Button");
            this.gameObject.transform.parent = RootObject.gameObject.transform;

        }
        else
        {
            RootObject = GameObject.Find("Display2Canvas");
            pageChange = GameObject.Find("Button2");
            this.gameObject.transform.parent = RootObject.gameObject.transform;
        }
    }

    void Update()
    {
        transform.Translate(move * Speed * Time.deltaTime); //スティック操作による移動
        Clamp();    //移動に対する制限

        #region カードの移動→使用するまでの処理
        var gamepad = Gamepad.all;

        if (!isUnderObjectMove && Gamepad.current.buttonSouth.isPressed && underObject != null)  //カードをつかんでいた場合下記の行動を実行
        {
            isUnderObjectMove = true;
            underObject.GetComponent<Drag>().OnBeginDrag();
        }
        else if (isUnderObjectMove && Gamepad.current.buttonSouth.isPressed)
        {
            underObject.GetComponent<Drag>().OnDrag();
        }
        else if (isUnderObjectMove && !Gamepad.current.buttonSouth.isPressed)
        {
            isUnderObjectMove = false;
            underObject.GetComponent<Drag>().OnEndDrag();
        }
        #endregion
        if ((Gamepad.current.leftShoulder.isPressed || Gamepad.current.rightShoulder.isPressed ) && isPageChange == false)
        {
            pageChange.GetComponent<PageChange>().OnPageChange();
            isPageChange = true;
        }
        else if (!(Gamepad.current.leftShoulder.isPressed || Gamepad.current.rightShoulder.isPressed) && isPageChange == true)
        {
            isPageChange = false;
        }

    }

    /// <summary>
    /// カーソルの移動制限
    /// </summary>
    private void Clamp()
    {
        playerPos = transform.position; //プレイヤーの位置を取得

        playerPos.x = Mathf.Clamp(playerPos.x, -1600f, 1800f); //x位置が常に範囲内か監視
        playerPos.y = Mathf.Clamp(playerPos.y, -900f, 930f); //x位置が常に範囲内か監視
        transform.position = new Vector2(playerPos.x, playerPos.y); //範囲内であれば常にその位置がそのまま入る
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //var  playerNum = isPlayer ? 1 : 0;
        var gamepad = Gamepad.all;

        if (!isPlayer)
        {
            if (!gamepad[Player1Num].buttonEast.isPressed && underObject == null && collision.CompareTag("UnitCard1") || collision.CompareTag("StrategyCard1"))
            {
                underObject = collision.gameObject;
            }
        }
        else if (isPlayer)
        {
            if (!gamepad[Player2Num].buttonEast.isPressed && underObject == null && collision.CompareTag("UnitCard2") || collision.CompareTag("StrategyCard2"))
            {
                underObject = collision.gameObject;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Gamepad.current.buttonSouth.isPressed && underObject != null && underObject != collision)
        {
            underObject = null;
        }
    }
}