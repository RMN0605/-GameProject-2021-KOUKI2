using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class moto_cursor : MonoBehaviour
{
    [SerializeField, Header("�J�[�\���̈ړ����x")]
    private int Speed;
    [Header("�v���C���[�P�Ȃ�False,�v���C���[�Q�Ȃ�True")]
    public bool isPlayer;       //�v���C���[�̎���

    public const int Player1Num = 0;
    public const int Player2Num = 1;

    [SerializeField, Header("�y�[�W�ύX�{�^��")]
    private GameObject pageChange;
    private Vector3 move;   //�J�[�\���ړ��̍ۂ̍��W
    private Vector2 playerPos; //���݂̍��W
    private bool isUnderObjectMove; //�J�[�h�𓮂����Ă��邩
    private bool isPageChange = false;
    public GameObject underObject;  //���ɂ���I�u�W�F�N�g������
    private GameObject RootObject;

    /// <summary>
    /// �X�e�B�b�N����
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// �{�^�����������ۂ̏���
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
        transform.Translate(move * Speed * Time.deltaTime); //�X�e�B�b�N����ɂ��ړ�
        Clamp();    //�ړ��ɑ΂��鐧��

        #region �J�[�h�̈ړ����g�p����܂ł̏���
        var gamepad = Gamepad.all;

        if (!isUnderObjectMove && Gamepad.current.buttonSouth.isPressed && underObject != null)  //�J�[�h������ł����ꍇ���L�̍s�������s
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
    /// �J�[�\���̈ړ�����
    /// </summary>
    private void Clamp()
    {
        playerPos = transform.position; //�v���C���[�̈ʒu���擾

        playerPos.x = Mathf.Clamp(playerPos.x, -1600f, 1800f); //x�ʒu����ɔ͈͓����Ď�
        playerPos.y = Mathf.Clamp(playerPos.y, -900f, 930f); //x�ʒu����ɔ͈͓����Ď�
        transform.position = new Vector2(playerPos.x, playerPos.y); //�͈͓��ł���Ώ�ɂ��̈ʒu�����̂܂ܓ���
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