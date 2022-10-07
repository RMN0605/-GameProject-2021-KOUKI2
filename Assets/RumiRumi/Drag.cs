using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Image))]
public class Drag: MonoBehaviour
{
//-----------------------------------------------------------------
//���ϐ���
    #region public
    [Header("���X�g�i���o�[")]
    public int summon_number;   //����or��������J�[�h�̃��X�g����g���J�[�h�̔ԍ����L��
    [Header("�v���C���[�P�Ȃ�False,�v���C���[�Q�Ȃ�True")]
    public bool isPlayer;       //�v���C���[�̎���
    [Header("���j�b�g�Ȃ�False,�v���J�[�h�Ȃ�True")]
    public bool isCardType; //���j�b�g�ƌv���J�[�h�̎���
    [Header("���@���g���ۂɔ͈͂�����ꍇ�͂����True��")]
    #region
    [Header("-------------------------------")]
    [Header("�K�v�ȏꍇ�͉��L��ݒ肵�Ă�������")]
    [Header("-------------------------------")]
    [Space(10)]
    #endregion
    public bool isEffectRange = false;  //�v���J�[�h�ɔ͈͂��������ۂɎg�p
    [Header("EffectRange������ꍇ�͊i�[")]
    public GameObject childGameObject;  //isEffectRange��true�ɂ����ۂɎg�p�A�͈̓I�u�W�F�N�g���i�[
    #endregion

    #region private
    public GameObject zone;    //���I�u�W�F�N�g�̉��ɂ��镨�������𔻒肷��A�����ł���ꏊ�A�����ł���ꏊ���𔻒肷��ۂɎg�p����
    private GameObject unit_parent; //�������ɂǂ��̎q�Ƃ��ďo����
    //�h���b�O�O�̈ʒu
    private Vector2 startPos;   //�I�u�W�F�N�g�̊J�n�ʒu�̐ݒ�
    //�J�[�\���̎擾
    private GameObject cursor;  //���I�u�W�F�N�g�̏�ɃJ�[�\���������Ă��邩�A�܂������Ă����ꍇ���̃J�[�\���̍��W�擾�̍ۂɎg�p
    private bool isSummonZone = false;  //�����A�����ł���ꏊ�Ɏ��I�u�W�F�N�g�����邩�m�F
    #endregion

    private Cooltime cooltime; //���c�FCooltime.cs�ɏ����m�F�t���O������̂Œǉ� 2 / 5

//-----------------------------------------------------------------
//�����쁄
    #region �Q�[���p�b�h����
    public void Start()
    {
        unit_parent = GameObject.Find("Unit_generation_location");  //Hierarchy�ɂ���i�[����ꏊ�ɑ΂��錟��
        cooltime = GetComponent<Cooltime>();
    }
    /// <summary>
    /// �h���b�O�J�n
    /// </summary>
    public void OnBeginDrag()
    {
        if (isEffectRange)
            childGameObject.SetActive(true);    //�����蔻��
        startPos = transform.position;  //�}�E�X���N���b�N�����ŏ��̏ꏊ��ۊ�                               
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);  //�F�𔖂�����
    }
    /// <summary>
    /// �h���b�O��
    /// </summary>
    public void OnDrag()
    {
        if (cursor == null)
            return;
        transform.position = cursor.transform.position;   //�J�[�\���Ɉړ�
    }

    /// <summary>
    /// �h���b�O�I��
    /// </summary>
    public void OnEndDrag()
    {
        if (isEffectRange)
            childGameObject.SetActive(false);    //�����蔻��

        //�F�����ɖ߂��i���F�ɂ���j
        GetComponent<Image>().color = Color.white;  

        //���j�b�g�J�[�h�������ꍇ
        if (!isCardType)
        {
            if (GeneralManager.instance.unitManager.UnitMoney >= Unit_manager.unit_list[summon_number].Price && isSummonZone == true)    //�����ł���R�C���������Ă��邩�A�����ł���ꏊ�ł��邩
            {
                if (!isPlayer && zone.gameObject.CompareTag("SummonZone_p1"))
                {
                    GeneralManager.instance.unitManager.UnitMoney -= Unit_manager.unit_list[summon_number].Price;
                    var unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[summon_number], this.transform.position, 1);
                    unit_child.transform.parent = unit_parent.transform;

                    cooltime.OnCoolTime = true; //���c�F�������ꂽ���Ƃ��m�F����t���O��ǉ� 2 / 5
                }
            }
            if(GeneralManager.instance.unitManager.UnitMoney2 >= Unit_manager.unit_list[summon_number].Price && isSummonZone == true)
            {
                if (isPlayer && zone.gameObject.CompareTag("SummonZone_p2"))
                {
                    GeneralManager.instance.unitManager.UnitMoney2 -= Unit_manager.unit_list[summon_number].Price;
                    var unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[summon_number], this.transform.position, 2);
                    unit_child.transform.parent = unit_parent.transform;

                    cooltime.OnCoolTime = true; //���c�F�������ꂽ���Ƃ��m�F����t���O��ǉ� 2 / 5
                }
            }
        }
        //�v���J�[�h�������ꍇ
        else if (isCardType)
        {
            if(zone == null)
            {
                transform.position = startPos;  //���̏ꏊ�ɖ߂���
            }
            else if (zone.gameObject.CompareTag("StrategyStage"))
            {
                if (gameObject.tag == "StrategyCard1")  //�J�[�h�̐���
                {
                    if (GeneralManager.instance.unitManager.UnitMoney >= Unit_manager.strategy_list[summon_number].Price)
                    {
                        GeneralManager.instance.unitManager.UnitMoney -= Unit_manager.strategy_list[summon_number].Price;
                        var unit_child = Unit_manager.Instantiate_unit(Unit_manager.strategy_list[summon_number], this.transform.position, 3);

                        cooltime.OnCoolTime = true; //���c�F�������ꂽ���Ƃ��m�F����t���O��ǉ� 2 / 5
                    }
                }
                else if (gameObject.tag == "StrategyCard2")
                {
                    if (GeneralManager.instance.unitManager.UnitMoney2 >= Unit_manager.strategy_list[summon_number].Price)
                    {
                        GeneralManager.instance.unitManager.UnitMoney2 -= Unit_manager.strategy_list[summon_number].Price;
                        var unit_child = Unit_manager.Instantiate_unit(Unit_manager.strategy_list[summon_number], this.transform.position, 3);

                        cooltime.OnCoolTime = true; //���c�F�������ꂽ���Ƃ��m�F����t���O��ǉ� 2 / 5
                    }
                }
            }
           

        } 
        transform.position = startPos;  //���̏ꏊ�ɖ߂���
    }
    #endregion

    #region �}�E�X����
    public void OnMouseCursorBeginDrag(BaseEventData eventData)
    {
        if (isEffectRange)
            childGameObject.SetActive(true);    //�����蔻��
        startPos = transform.position;  //�}�E�X���N���b�N�����ŏ��̏ꏊ��ۊ�                               
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);  //�F�𔖂�����
    }
    /// <summary>
    /// �h���b�O��
    /// </summary>
    public void OnMouseCursorDrag(BaseEventData eventData)
    {
        Vector2 objectPoint = Camera.main.WorldToScreenPoint(transform.position);   //object�̈ʒu�����[���h���W����X�N���[�����W�ɕϊ����āAobjectPoint�Ɋi�[
        Vector2 pointScreen = new Vector2(Input.mousePosition.x, Input.mousePosition.y);   //�}�E�X�̈ʒu��ۑ�   
        Vector2 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);   //�I�u�W�F�N�g�̌��݈ʒu���A�X�N���[�����W���烏�[���h���W�ɕϊ����āApointWorld�Ɋi�[
        transform.position = pointWorld;    //�I�u�W�F�N�g�̈ʒu���ApointWorld�ɂ���  
    }
    /// <summary>
    /// �h���b�O�I��
    /// </summary>
    public void OnMouseCursorEndDrag(BaseEventData eventData)
    {
        if (isEffectRange)
            childGameObject.SetActive(false);    //�����蔻��

        //�F�����ɖ߂��i���F�ɂ���j
        GetComponent<Image>().color = Color.white;

        //�v���J�[�h�������ꍇ
        if (isCardType)
        {
            if (zone.gameObject.CompareTag("StrategyStage")) //      �}�E�X�̃��C���͈͈ȓ��ɓ������Ă�ꍇ
            {
                if (GeneralManager.instance.unitManager.UnitMoney > Unit_manager.strategy_list[summon_number].Price)
                {
                    GeneralManager.instance.unitManager.UnitMoney -= Unit_manager.strategy_list[summon_number].Price;
                    var unit_child = Unit_manager.Instantiate_unit(Unit_manager.strategy_list[summon_number], this.transform.position, 3);
                }
            }
        }
        //���j�b�g�J�[�h�������ꍇ
        else if (!isCardType)
        {
            if (GeneralManager.instance.unitManager.UnitMoney > Unit_manager.unit_list[summon_number].Price && isSummonZone == true)    //�����ł���R�C���������Ă��邩�A�����ł���ꏊ�ł��邩
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


        transform.position = startPos;  //���̏ꏊ�ɖ߂���
    }
    #endregion

//-----------------------------------------------------------------
//�������蔻�聄
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlayer)
        {
            if (collision.CompareTag("Cursor1"))
            {
                cursor = collision.gameObject;  //�J�[�\������ɂ�����i�[
            }
        }
        else if (isPlayer)
        {
            if (collision.CompareTag("Cursor2"))
            {
                cursor = collision.gameObject;  //�J�[�\������ɂ�����i�[
            }
        }

        if (isCardType) //�J�[�h�^�C�v�̔���
        {
            if (collision.CompareTag("StrategyStage"))
            {
                zone = collision.gameObject;    //�����ł���ꏊ���i�[
                isSummonZone = true;    //�����ł���ꏊ�ł��邱�Ƃ��m�F
            }
        }
        else�@//�J�[�h�^�C�v�̔���
        {
            if (!isPlayer)
            {
                if (collision.CompareTag("SummonZone_p1"))
                {
                    zone = collision.gameObject;    //�����ł���ꏊ���i�[
                    isSummonZone = true;    //�����ł���ꏊ�ł��邱�Ƃ��m�F
                }
            }
            else if (isPlayer)
            {
                if (collision.CompareTag("SummonZone_p2"))
                {
                    zone = collision.gameObject;    //�����ł���ꏊ���i�[
                    isSummonZone = true;    //�����ł���ꏊ�ł��邱�Ƃ��m�F
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
