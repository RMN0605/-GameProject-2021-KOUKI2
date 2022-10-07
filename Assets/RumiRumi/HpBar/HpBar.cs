using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private int maxHp = 0;
    private int currentHp = 0;  //���݂̗̑�
    [Header("�e�I�u�W�F�N�g�̗̑�")]
    public Unit_model parentHp;
    [Header("���l��ς���X���C�_�[")]
    public Slider slider;

    private void Start()
    {
        slider.value = 1;   //�̗̓Q�[�W���ő�ɂ����
        maxHp = parentHp.hp;
        currentHp = maxHp;
    }
    private void FixedUpdate()
    {
        currentHp = parentHp.hp;
        slider.value = (float)currentHp / (float)maxHp;
    }
}
