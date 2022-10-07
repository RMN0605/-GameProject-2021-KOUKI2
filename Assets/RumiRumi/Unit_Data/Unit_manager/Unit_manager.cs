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
    [Header("�v���C���[�P�̋��f�[�X")]
    public int UnitMoney = 50;
    [Header("�v���C���[�Q�̋��f�[�X")]
    public int UnitMoney2 = 50;

    private void Awake()
    {
        //���j�b�g�̃f�[�^�͂��ׂĂ����Ƀ��X�g��������B
        Unit_data[] dataFiles = Resources.LoadAll<Unit_data>("Unit_data");
        //Unit_data�����[�h
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
    /// ����͂Ȃ񂼂�F �U���Ɋւ���Փˉ������
    /// �������R�F      �����ɍU�������������ꍇ�̃G���[���p
    /// ����Ă��邱�ƁF�U���͈͂ɓ������҂���U����\�񂵂čU�����Ԃ�������U��������B
    /// </summary>
    public void FixedUpdate()
    {
        float elapsed_time = Time.fixedDeltaTime;    //�o�ߎ��Ԃ̎擾

        for (int i = 0; i < attackInfos.Count; i++)
        {
            if (attackInfos[i] != null && attackInfos[i].GetComponent<Unit>().isCool_down == false)
            {
                attackInfos[i].GetComponent<Unit_model>().now_attack_delay += elapsed_time;

                //�U���f�B���C���K�莞�ԂɒB�����ꍇ�ǂݍ��܂���� / �U���̃A�j���[�V�������g���Ă���ۂɂ����̌v�Z���g�p������B
                if (attackInfos[i].GetComponent<Unit_model>().now_attack_delay > attackInfos[i].GetComponent<Unit_model>().attack_delay)
                {
                    //�U���̏���
                    StartCoroutine(attackInfos[i].GetComponent<Unit>().Battle(attackInfos[i].GetComponent<Unit_model>().attack_cool_time));   //�U���̌Ăяo��

                    //���Z�b�g
                    attackInfos[i].GetComponent<Unit_model>().now_attack_delay = 0; //�U���ҋ@���Ԃ��O�ɂ����
                    //���X�g����폜
                    attackInfos.Remove(attackInfos[i]);
                }
            }
            else if(attackInfos[i]�@== null) //�U���\����������j�b�g������ł��邩�m�F
            {
                attackInfos.Remove(attackInfos[i]); //null��������j�������
            }
        }
    }

    /// <summary>
    /// �_���[�W�v�Z
    /// </summary>
    /// <param name="attack_power">�U�����鑤�̍U����</param>
    /// <param name="defense_power">�U�����󂯂鑤�̖h���</param>
    /// <returns></returns>
    public int Attack_calculation(int attack_power,int defense_power,GameObject attack_name)
    {
        //Debug.Log((attack_name)+"��"+(attack_power - defense_power)+"�_���[�W��^����");
        if (attack_power > defense_power)
        {
            //Debug.Log("�ȉ��̃_���[�W����������");
            //Debug.Log(attack_power - defense_power);
            return attack_power - defense_power;
        }
        else if (attack_power < defense_power)
        {
            //�U���͂��f�B�t�F���X�͂̕��������Ă����΂���1�_���[�W�^�����N
            return 1;
        }
        else if (attack_power == defense_power)  
        {
            return 0;
        }
        else //���炩�̃G���[���N�����ꍇ�͂�����ց`
        {
            Debug.Log("�����Ńf�o�b�O���O�������̂͂���������i�_���[�W�v�Z�j");
            return 0;
        }
    }

    /// <summary>
    /// ���j�b�g�̐����֐�
    /// </summary>
    /// <param name="unit_data">�����������I�u�W�F�N�g�f�[�^</param>
    /// <param name="vec">�����������ꏊ</param>
    /// /// <param name="player">���������߂Ă���v���C���[�̔��ʁA�P�Ȃ獶�A�Q�Ȃ�E �A�R�Ȃ�v���J�[�h</param>
    /// <returns></returns>
    public static GameObject Instantiate_unit(Unit_data unit_data , Vector3 vec ,int player)
    {
        if (player == 1)
        {
            unit_data.Unit_object.tag = ("Unit1");  //���̃v���C���[���̃��j�b�g�ł���^�O������
            unit = Instantiate(unit_data.Unit_object, vec, Quaternion.identity);
        }
        else if (player == 2)
        {
            unit_data.Unit_object.tag = ("Unit2");  ////�E�̃v���C���[���̃��j�b�g�ł���^�O������
            unit = Instantiate(unit_data.Unit_object, vec, new Quaternion(0,180,0,1));  //�E�v���C���[���̃��j�b�g�̏ꍇ��
        }
        else if(player == 3)
        {
            unit = Instantiate(unit_data.Unit_object, vec, Quaternion.identity);
        }
        return unit;
    }
}
