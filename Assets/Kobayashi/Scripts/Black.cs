using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black : MonoBehaviour
{
    [SerializeField]
    List<GameObject> within_range = new List<GameObject>();
    [Header("攻撃ディレイ時間(攻撃アニメーションの時間)/秒")]
    public float attack_delay;  //攻撃モーション
    private float nowDelay; //現在のモーション時間
    private Vector2 distance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        within_range.Add(collision.gameObject); //範囲内に入るとリストに追加
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        within_range.Remove(collision.gameObject);//範囲内を離れるとリストから消去
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (attack_delay >= nowDelay)
        {
            nowDelay += Time.deltaTime;
        }
        else if (attack_delay < nowDelay)
        {
            BlackHole(); 
        }
    }
    void BlackHole()
    {
          
        foreach(GameObject unit in within_range)
        {
            distance = this.gameObject.transform.position - unit.transform.position;
            transform.Translate(distance);
        }
        //Destroy(this.gameObject);
    }
}