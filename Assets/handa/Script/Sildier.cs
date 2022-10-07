using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof (Image))]
public class Sildier : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //ドラッグ前の位置
    private Vector2 startPos;
    //ドラッグ開始
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = transform.position;
        //色を薄くする
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);

        //raycastTargetをOFFにする
        GetComponent<Image>().raycastTarget = false;
    }

    //ドラッグ中
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 objectPoint　= Camera.main.WorldToScreenPoint(transform.position);   //objectの位置をワールド座標からスクリーン座標に変換して、objectPointに格納
        Vector2 pointScreen　= new Vector2(Input.mousePosition.x,Input.mousePosition.y);   //マウスの位置を保存   
        Vector2 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);   //オブジェクトの現在位置を、スクリーン座標からワールド座標に変換して、pointWorldに格納
        transform.position = pointWorld;    //オブジェクトの位置を、pointWorldにする
    }

    //ドラッグ終了
    public void OnEndDrag(PointerEventData eventData)
    {
        //色を元に戻す（白色にする）
        GetComponent<Image>().color = Color.white;

        //raycastTargetをONにする
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
