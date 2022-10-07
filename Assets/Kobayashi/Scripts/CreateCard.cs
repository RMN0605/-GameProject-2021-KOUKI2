using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCard : MonoBehaviour
{
    [SerializeField] private Transform PlayerHand;  //場所参照
    //[SerializeField] private List<GameObject> cards = new List<GameObject>();  //カードリスト作成

    private void Start()
    {
        //Create();
    }
    /// <summary>
    /// カードを生成する
    /// </summary>
    public void Create()
    {

        int ObjCount = this.transform.childCount;

        if (ObjCount <= 0)
        {
            for (int i = 0; i <= 4; i++)
            {
                //int num = Random.Range(0, cards.Count); //0からカードのリストの数だけランダムでだす
                //GameObject.Instantiate(cards[num], PlayerHand);    //カード生成
            }
        }
        else
        {
            foreach (Transform n in PlayerHand.transform)   //子にあるオブジェクト全消し
            {
                GameObject.Destroy(n.gameObject);
            }
            for (int i = 0; i <= 4; i++)
            {
                //int num = Random.Range(0, cards.Count); //0からカードのリストの数だけランダムでだす
                //GameObject.Instantiate(cards[num], PlayerHand);    //カード生成
            }
        }
    }
}
