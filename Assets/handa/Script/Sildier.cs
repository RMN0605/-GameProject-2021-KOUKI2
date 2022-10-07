using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof (Image))]
public class Sildier : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //�h���b�O�O�̈ʒu
    private Vector2 startPos;
    //�h���b�O�J�n
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = transform.position;
        //�F�𔖂�����
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);

        //raycastTarget��OFF�ɂ���
        GetComponent<Image>().raycastTarget = false;
    }

    //�h���b�O��
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 objectPoint�@= Camera.main.WorldToScreenPoint(transform.position);   //object�̈ʒu�����[���h���W����X�N���[�����W�ɕϊ����āAobjectPoint�Ɋi�[
        Vector2 pointScreen�@= new Vector2(Input.mousePosition.x,Input.mousePosition.y);   //�}�E�X�̈ʒu��ۑ�   
        Vector2 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);   //�I�u�W�F�N�g�̌��݈ʒu���A�X�N���[�����W���烏�[���h���W�ɕϊ����āApointWorld�Ɋi�[
        transform.position = pointWorld;    //�I�u�W�F�N�g�̈ʒu���ApointWorld�ɂ���
    }

    //�h���b�O�I��
    public void OnEndDrag(PointerEventData eventData)
    {
        //�F�����ɖ߂��i���F�ɂ���j
        GetComponent<Image>().color = Color.white;

        //raycastTarget��ON�ɂ���
        GetComponent<Image>().raycastTarget = true;

        bool flg = true;

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("SummonZone_p1"))
            {
                transform.position = hit.gameObject.transform.position;
                flg = false;

                GetComponent<Image>().raycastTarget = false;

            }
        }

        if (flg)
        {
            transform.position = startPos;
        }
    }
}
