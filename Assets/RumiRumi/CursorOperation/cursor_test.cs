using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cursor_test : MonoBehaviour
{
    [SerializeField, Header("�J�[�\���̈ړ����x")]
    private int Speed;
    [Header("�v���C���[�P�Ȃ�False,�v���C���[�Q�Ȃ�True")]
    public bool isPlayer;       //�v���C���[�̎���
    [SerializeField, Header("�y�[�W�ύX�{�^��")]
    private GameObject pageChange;
    private Vector3 move;   //�J�[�\���ړ��̍ۂ̍��W
    private Vector2 playerPos; //���݂̍��W
    private bool isUnderObjectMove; //�J�[�h�𓮂����Ă��邩
    private bool isPageChange = false;
    public GameObject underObject;  //���ɂ���I�u�W�F�N�g������
    [SerializeField, Header("���[�g�I�u�W�F�N�g")]
    private GameObject RootObject;

    public const int Player1Num = 0;
    public const int Player2Num = 1;
    private int _padNum = 0;
    private Gamepad gamePad;

    private void Awake()
    {
        if (!isPlayer)
            Setup(Player1Num);
        else if(isPlayer)
            Setup(Player2Num);
    }

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
            if (SceneManager.GetActiveScene().name == "Title 1")
            {
                GeneralManager.instance.unitManager.UnitMoney = 50;
                GeneralManager.instance.unitManager.UnitMoney2 = 50;
                SceneManager.LoadScene("mainBattleScene(tuusinn)");
            }
            if (SceneManager.GetActiveScene().name == "Result")
            {
                SceneManager.LoadScene("Title 1");
            }
        }
    }
    public void Setup(int padNum)
    {

        _padNum = padNum;
        if (padNum == 0)
        {
            gamePad = Gamepad.all[1];
        }
        else
        {
            gamePad = Gamepad.all[0];
        }
        if (RootObject == null)
            return;
    }

    void Update()
    {
        transform.Translate(move * Speed * Time.deltaTime); //�X�e�B�b�N����ɂ��ړ�
        Clamp();    //�ړ��ɑ΂��鐧��

        #region �J�[�h�̈ړ����g�p����܂ł̏���
        if (underObject != null && (underObject.gameObject.CompareTag("UnitCard1") || underObject.gameObject.CompareTag("StrategyCard1") || underObject.gameObject.CompareTag("UnitCard2") || underObject.gameObject.CompareTag("StrategyCard2")))
        {
            if(underObject.GetComponent<Cooltime>()== null)
            {
                return;
            }
            else if (!underObject.GetComponent<Cooltime>().OnCoolTime)
            {
                if (!isUnderObjectMove && gamePad.buttonSouth.isPressed)  //�J�[�h������ł����ꍇ���L�̍s�������s
                {
                    isUnderObjectMove = true;
                    underObject.GetComponent<Drag>().OnBeginDrag();
                }
                else if (isUnderObjectMove && gamePad.buttonSouth.isPressed)
                {
                    underObject.GetComponent<Drag>().OnDrag();
                }
                else if (isUnderObjectMove && !gamePad.buttonSouth.isPressed)
                {
                    isUnderObjectMove = false;
                    underObject.GetComponent<Drag>().OnEndDrag();
                }
            }
        }
        #endregion
        if ((gamePad.leftShoulder.isPressed || gamePad.rightShoulder.isPressed) && isPageChange == false)
        {
            pageChange.GetComponent<PageChange>().OnPageChange();
            isPageChange = true;
        }
        else if (!(gamePad.leftShoulder.isPressed || gamePad.rightShoulder.isPressed) && isPageChange == true)
        {
            isPageChange = false;
        }

        //���\�p
        if(gamePad.buttonEast.isPressed && gamePad.buttonNorth.isPressed && SceneManager.GetActiveScene().name == "mainBattleScene(tuusinn)")
        {
            SceneManager.LoadScene("Title 1");
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
        if (!isUnderObjectMove)
        {
            if (!isPlayer)
            {
                if (!gamePad.buttonEast.isPressed && underObject == null && collision.CompareTag("UnitCard1") || collision.CompareTag("StrategyCard1"))
                {
                    underObject = collision.gameObject;
                }
            }
            else
            {
                if (!gamePad.buttonEast.isPressed && underObject == null && collision.CompareTag("UnitCard2") || collision.CompareTag("StrategyCard2"))
                {
                    underObject = collision.gameObject;
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isUnderObjectMove)
        {
            if (!isPlayer)
            {
                if (!gamePad.buttonEast.isPressed && underObject == null && collision.CompareTag("UnitCard1") || collision.CompareTag("StrategyCard1"))
                {
                    if (underObject == collision.gameObject)
                        return;
                    underObject = collision.gameObject;
                }
            }
            else
            {
                if (!gamePad.buttonEast.isPressed && underObject == null && collision.CompareTag("UnitCard2") || collision.CompareTag("StrategyCard2"))
                {
                    if (underObject == collision.gameObject)
                        return;
                    underObject = collision.gameObject;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isUnderObjectMove)
        {
            if (!gamePad.buttonSouth.isPressed && underObject != null && underObject != collision)
            {
                underObject = null;
            }
        }
    }
}