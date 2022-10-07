using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooltime : MonoBehaviour
{
    [Header("�N�[���^�C���pImsge")]
    public Image UIobj; //�N�[���^�C���p�C���[�W

    [Header("�K�v�����ݒ�")]
    public int NeedMoney = 0; //�K�v�����̋��z�ݒ�

    [Header ("�N�[���^�C��")]
    public float countTime = 5.0f;//�N�[���^�C���b��

    [Header("�N�[���^�C���J�nflag")]
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
        //�v���C���[�Por�Q�̎������K�v�������Ⴍ�A�����t���O�����̎�
        if (tag == "UnitCard1" || tag == "StrategyCard1")
        {
            if (GeneralManager.instance.unitManager.UnitMoney < NeedMoney && OnCoolTime == false)
            {
                //�\��
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
                //�\��
                UIobj.enabled = true;
                bc.enabled = false;
            }
            else if (UIobj.enabled == true)
            {
                UIobj.enabled = false;
                bc.enabled = true;
            }
        }
        //��������A�t���O�����ɂȂ�����
        //Drag.cs���̃h���b�O�I���������Ƀt���O�̐؂�ւ�����������
        if (OnCoolTime == true)
        {
            //�\��
            UIobj.enabled = true;
            bc.enabled = false;
            CoolTime();
        }
    }

    /// <summary>
    /// �N�[���^�C��
    /// </summary>
    void CoolTime()
    {
        //�ݒ肵���N�[���^�C�������b���ƌ���
        UIobj.fillAmount -= 1.0f / countTime * Time.deltaTime;
        //fillAmount��0�ɂȂ�����
        if (UIobj.fillAmount == 0)
        {
            UIobj.fillAmount = 1;//fillAmount���P�ɖ߂���
            OnCoolTime = false;//�t���O�𕉂�
            bc.enabled = true;
            UIobj.enabled = false;//��\��
        }

    }
    
}

