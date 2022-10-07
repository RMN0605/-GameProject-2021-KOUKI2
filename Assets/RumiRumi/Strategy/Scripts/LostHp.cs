using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostHp : MonoBehaviour
{
    [SerializeField]
    List<GameObject> unitList = new List<GameObject>();
    private GameObject generationLocation;
    [Header("�U���f�B���C����(�U���A�j���[�V�����̎���)/�b")]
    public float attack_delay;  //�U�����[�V����
    private float nowDelay; //���݂̃��[�V��������

    private void Start()
    {
        generationLocation = GameObject.Find("Unit_generation_location");
    }

    private void Update()
    {
        if (attack_delay >= nowDelay)
        {
            nowDelay += Time.deltaTime;
        }
        else if (attack_delay < nowDelay)
        {
            Magic();
        }
    }
    private void Magic()
    {
        if (this.CompareTag("StrategyCard1"))
        {
            foreach (Transform unit in generationLocation.transform)
            {
                if (unit.CompareTag("Unit2"))   //�G���j�b�g��HP���P�ɂ����
                    unit.GetComponent<Unit_model>().hp = 1;
            }
        }
        foreach (Transform unit in generationLocation.transform)
        {
            if (this.CompareTag("StrategyCard2"))
            {
                if (unit.CompareTag("Unit1"))   //�G���j�b�g��HP���P�ɂ����
                    unit.GetComponent<Unit_model>().hp = 1;
            }
        }
        Destroy(this.gameObject);
    }
}
