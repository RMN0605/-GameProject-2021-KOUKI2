using UnityEngine;

public class Unit_model : Unit_base
{
    [Header("�̗�")]
    public int hp;              //���j�b�g�̗̑�
    [Header("�U����")]
    public int attack_power;    //���j�b�g�̍U����
    [Header("�U���f�B���C����(�U���A�j���[�V�����̎���)/�b")]
    public float attack_delay;  //���j�b�g�̍U������ۂ̍\���Ă��炠����܂ł̃f�B���C����(�A�j���[�V�������Ŏg�p����ۂɂǂ���)
    [Header("�U���̃N�[���^�C��(���̍U���܂ł̎���)/�b")]
    public int attack_cool_time;    //���j�b�g�̍U����̑ҋ@����
    [Header("�h���")]
    public int defense_power;   //���j�b�g�̖h���
    [Header("�ړ����x")]
    public float move_speed;  //���j�b�g�̈ړ����x
    [HideInInspector]
    public float now_attack_delay;    //�f�B���C�o�ߎ��Ԍv�Z�p / �g�p����ꏊ��Unit_Manager
}
