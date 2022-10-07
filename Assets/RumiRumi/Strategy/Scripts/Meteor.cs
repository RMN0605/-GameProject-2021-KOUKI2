using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    List<GameObject> unitList = new List<GameObject>();
    [Header("�U���f�B���C����(�U���A�j���[�V�����̎���)/�b")]
    public float attack_delay;  //�U�����[�V����
    private float nowDelay; //���݂̃��[�V��������

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
