using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black : MonoBehaviour
{
    [SerializeField]
    List<GameObject> within_range = new List<GameObject>();
    [Header("�U���f�B���C����(�U���A�j���[�V�����̎���)/�b")]
    public float attack_delay;  //�U�����[�V����
    private float nowDelay; //���݂̃��[�V��������
    private Vector2 distance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        within_range.Add(collision.gameObject); //�͈͓��ɓ���ƃ��X�g�ɒǉ�
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        within_range.Remove(collision.gameObject);//�͈͓��𗣂��ƃ��X�g�������
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