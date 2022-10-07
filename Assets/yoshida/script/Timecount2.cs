using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Timecount2 : MonoBehaviour
{
    [SerializeField, Header("�������Ԃ̐ݒ�l")]
    public int _countdown;
    private static int countdown;
    [SerializeField, Header("�������Ԃ̕\��")]
    public Text countdownText;
    [SerializeField, Header("�������Ԃ̕\��2")]
    public Text countdownText2;
    private int currentTime;    // ���݂̎c�莞�ԁi�s�v�ȏꍇ�͐錾���Ȃ��j
    private float timer;        // ���Ԍv���p
    public GameObject castle1;
    public GameObject castle2;
    public GameObject dragon1;
    public GameObject dragon2;
    private float moneyTimer;   // ���Ԍv���p(����)
    private void Awake()
    {
        countdown = _countdown;

        //countdown��currentTime�ɏ���������
        currentTime = countdown;
    }

    void Update()
    {
        // timer�𗘗p���Čo�ߎ��Ԃ��v��
        timer += Time.deltaTime;

        // 1�b�o�߂��Ƃ�timer��0�ɖ߂��Acountdown�����Z����
        if (timer >= 1)
        {
            timer = 0;
            countdown--;  
            // ���ԕ\�����X�V���郁�\�b�h���Ăяo��
            DisplayCountTime(countdown);   
        }

        // timer�𗘗p���Čo�ߎ��Ԃ��v��
        moneyTimer += Time.deltaTime;

        // 2�b�o�߂��Ƃ�timer��0�ɖ߂��Amoney�����Z����
        if (moneyTimer >= 2)
        {
            moneyTimer = 0;
            GeneralManager.instance.unitManager.UnitMoney++;
            GeneralManager.instance.unitManager.UnitMoney2++;
        }

        if (countdown <= 0)
        {
            countdownText.text = "Time Up!!";
            //TextChange.is_draw = true;
            if(castle1 != null && castle2 != null)
            {
                if(castle1.GetComponent<Unit_model>().hp >= castle2.GetComponent<Unit_model>().hp)
                {
                    TextChange.is_witch = true;
                }
                else
                    TextChange.is_witch = false;
            }
            else if (castle1 == null && castle2 != null) 
            {
                TextChange.is_witch = false;
            }
            else if (castle1 != null && castle2 == null)
            {
                TextChange.is_witch = true;
            }
            else
            {
                if (dragon1.GetComponent<Unit_model>().hp >= dragon2.GetComponent<Unit_model>().hp)
                {
                    TextChange.is_witch = true;
                }
                else
                    TextChange.is_witch = false;
            }
            SceneManager.LoadScene("Result");
        }
    }

    /// <summary>
    /// �������Ԃ��X�V����[��:�b]�ŕ\������
    /// </summary>
    public void DisplayCountTime(int limitTime)
    {
        // �����Ŏ󂯎�����l��[��:�b]�ɕϊ����ĕ\������
        // ToString("00")�Ń[���v���[�X�t�H���_�[���āA�P���̂Ƃ��͓���0������
        countdownText.text = ((int)(limitTime / 60)).ToString("00") + ":" + ((int)limitTime % 60).ToString("00     ") + "����:" + GeneralManager.instance.unitManager.UnitMoney.ToString("0");
        if(countdownText2 != null)
        {
            countdownText2.text = ((int)(limitTime / 60)).ToString("00") + ":" + ((int)limitTime % 60).ToString("00     ") + "����:" + GeneralManager.instance.unitManager.UnitMoney2.ToString("0");
        }
    }
}

/*
 f0 = �����_��\�����Ȃ�
 f1 = �����_���ʂ܂ŕ\��
 f2 = �����_���ʂ܂ŕ\��
   ....
 */
