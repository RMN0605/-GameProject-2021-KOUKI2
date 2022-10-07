using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassSummon_Card : MonoBehaviour
{
    private GameObject unit_child;
    private GameObject unit_parent;

    [Header("�U���f�B���C����(�U���A�j���[�V�����̎���)/�b")]
    public float attack_delay;  //�U�����[�V����
    private float nowDelay; //���݂̃��[�V��������
    [SerializeField,Header("�������鐔")]
    private int summons;
    [SerializeField, Header("�v���C���[:�P�Ȃ�false,2�Ȃ�true")]
    private bool isPlayer = false;
    private bool isUsed = false; //�g��ꂽ��true
    void Start()
    {
        unit_parent = GameObject.Find("Unit_generation_location");
    }

    // Update is called once per frame
    void Update()
    {
        if (attack_delay >= nowDelay)
        {
            nowDelay += Time.deltaTime;
        }
        else if (attack_delay < nowDelay && !isUsed)
        {
            SummonUnit(isPlayer, summons);
        }
    }

    public void SummonUnit(bool _isPlayer , int _summons)
    {
        isUsed = true;
        for (int i = 0; i < _summons; i++)
        {
            if (!_isPlayer)
            {
                unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[Random.Range(0, 4)], this.transform.position, 1);
                unit_child.transform.position = new Vector3(-1410, Random.Range(-650f,500f),0);
            }
            else if(_isPlayer)
            {
                unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[Random.Range(0, 4)], this.transform.position, 2);
                unit_child.transform.position = new Vector2(1410, Random.Range(-650f, 500f));
            }
            unit_child.transform.parent = unit_parent.transform;
        }
        Destroy(this.gameObject);
    }
}
