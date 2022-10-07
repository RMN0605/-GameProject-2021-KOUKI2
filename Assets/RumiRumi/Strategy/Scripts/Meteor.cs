using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    List<GameObject> unitList = new List<GameObject>();
    [Header("攻撃ディレイ時間(攻撃アニメーションの時間)/秒")]
    public float attack_delay;  //攻撃モーション
    private float nowDelay; //現在のモーション時間

    private void Start()
    {
        gameObject.transform.parent = GameObject.Find("UICanvas").transform;
    }
    private void Update()
    {
        if(attack_delay >= nowDelay)
        {
            nowDelay += Time.deltaTime;
        }
        else if(attack_delay < nowDelay)
        {
            Magic();
        }
    }
    private void OnCollisionEnter2D(Collision2D co)
    {
        if(co.gameObject.CompareTag("Unit1") || co.gameObject.CompareTag("Unit2"))
        {
            unitList.Add(co.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D co)
    {
        if (co.gameObject.CompareTag("Unit1") || co.gameObject.CompareTag("Unit2"))
        {
            unitList.Remove(co.gameObject);
        }
    }
    private void Magic()
    {
        for(int i = 0; i < unitList.Count; i++)
        {
            if (unitList[i] == null)
                unitList.Remove(unitList[i]);
            Destroy(unitList[i].gameObject);
        }
        Destroy(this.gameObject);
    }
}
