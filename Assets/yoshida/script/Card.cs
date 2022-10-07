using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("�U���f�B���C����(�U���A�j���[�V�����̎���)/�b")]
    public float attack_delay;  //�U�����[�V����
    private float nowDelay; //���݂̃��[�V��������

    int A;  //A���ĉ��ł����H
    int B;  //B���ĉ��ł����H
    int C;
    int D;

    public GameObject castle1HP;
    public GameObject castle2HP;
    public GameObject dragon1HP;
    public GameObject dragon2HP;
    private void Awake()
    {
        castle1HP = GameObject.Find("Player1Castle");
        castle2HP = GameObject.Find("Player2Castle");
        dragon1HP = GameObject.Find("Player1Dragon");
        dragon2HP = GameObject.Find("Player2Dragon");
    }

    void Start()
    {
        if (castle1HP == null && castle2HP == null)
        {
            C = dragon1HP.GetComponent<Unit_model>().hp;
            D = dragon2HP.GetComponent<Unit_model>().hp;
        }
        else if (castle1HP == null && castle2HP != null)
        {
            B = castle2HP.GetComponent<Unit_model>().hp;
            C = dragon1HP.GetComponent<Unit_model>().hp;
        }
        else if (castle1HP != null && castle2HP == null)
        {
            A = castle1HP.GetComponent<Unit_model>().hp;
            D = dragon2HP.GetComponent<Unit_model>().hp;
        }
        else
        {
            A = castle1HP.GetComponent<Unit_model>().hp;
            B = castle2HP.GetComponent<Unit_model>().hp;
        }

    }

    void Update()
    {
        if (attack_delay >= nowDelay)
        {
            nowDelay += Time.deltaTime;
        }
        else if (attack_delay < nowDelay)
        {
            ChangeHp();
        }
    }

    private void ChangeHp()
    {
        if (castle1HP == null && castle2HP == null)
        {
            dragon1HP.GetComponent<Unit_model>().hp = D;
            dragon2HP.GetComponent<Unit_model>().hp = C;
        }
        else if (castle1HP == null && castle2HP != null)
        {
            dragon1HP.GetComponent<Unit_model>().hp = B;
            castle2HP.GetComponent<Unit_model>().hp = C;
        }
        else if(castle1HP != null && castle2HP == null)
        {
            dragon2HP.GetComponent<Unit_model>().hp = A;
            castle1HP.GetComponent<Unit_model>().hp = D;
        }
        else
        {
            castle1HP.GetComponent<Unit_model>().hp = B;
            castle2HP.GetComponent<Unit_model>().hp = A;
        }

        Destroy(this.gameObject);
    }
}
