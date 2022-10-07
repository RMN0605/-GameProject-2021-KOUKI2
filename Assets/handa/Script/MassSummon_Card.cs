using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassSummon_Card : MonoBehaviour
{
    private GameObject unit_child;
    private GameObject unit_parent;

    [Header("攻撃ディレイ時間(攻撃アニメーションの時間)/秒")]
    public float attack_delay;  //攻撃モーション
    private float nowDelay; //現在のモーション時間
    [SerializeField,Header("召喚する数")]
    private int summons;
    [SerializeField, Header("プレイヤー:１ならfalse,2ならtrue")]
    private bool isPlayer = false;
    private bool isUsed = false; //使われたらtrue
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
