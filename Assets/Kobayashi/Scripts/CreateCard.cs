using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCard : MonoBehaviour
{
    [SerializeField] private Transform PlayerHand;  //�ꏊ�Q��
    //[SerializeField] private List<GameObject> cards = new List<GameObject>();  //�J�[�h���X�g�쐬

    private void Start()
    {
        //Create();
    }
    /// <summary>
    /// �J�[�h�𐶐�����
    /// </summary>
    public void Create()
    {

        int ObjCount = this.transform.childCount;

        if (ObjCount <= 0)
        {
            for (int i = 0; i <= 4; i++)
            {
                //int num = Random.Range(0, cards.Count); //0����J�[�h�̃��X�g�̐����������_���ł���
                //GameObject.Instantiate(cards[num], PlayerHand);    //�J�[�h����
            }
        }
        else
        {
            foreach (Transform n in PlayerHand.transform)   //�q�ɂ���I�u�W�F�N�g�S����
            {
                GameObject.Destroy(n.gameObject);
            }
            for (int i = 0; i <= 4; i++)
            {
                //int num = Random.Range(0, cards.Count); //0����J�[�h�̃��X�g�̐����������_���ł���
                //GameObject.Instantiate(cards[num], PlayerHand);    //�J�[�h����
            }
        }
    }
}
