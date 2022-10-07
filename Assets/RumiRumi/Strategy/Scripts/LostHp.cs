using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostHp : MonoBehaviour
{
    [SerializeField]
    List<GameObject> unitList = new List<GameObject>();
    private GameObject generationLocation;
    [Header("UŒ‚ƒfƒBƒŒƒCŽžŠÔ(UŒ‚ƒAƒjƒ[ƒVƒ‡ƒ“‚ÌŽžŠÔ)/•b")]
    public float attack_delay;  //UŒ‚ƒ‚[ƒVƒ‡ƒ“
    private float nowDelay; //Œ»Ý‚Ìƒ‚[ƒVƒ‡ƒ“ŽžŠÔ

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
                if (unit.CompareTag("Unit2"))   //“Gƒ†ƒjƒbƒg‚ÌHP‚ð‚P‚É‚·‚é‚æ
                    unit.GetComponent<Unit_model>().hp = 1;
            }
        }
        foreach (Transform unit in generationLocation.transform)
        {
            if (this.CompareTag("StrategyCard2"))
            {
                if (unit.CompareTag("Unit1"))   //“Gƒ†ƒjƒbƒg‚ÌHP‚ð‚P‚É‚·‚é‚æ
                    unit.GetComponent<Unit_model>().hp = 1;
            }
        }
        Destroy(this.gameObject);
    }
}
