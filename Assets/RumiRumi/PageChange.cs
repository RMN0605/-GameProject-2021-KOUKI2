using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChange : MonoBehaviour
{

    public GameObject Page1;
    public GameObject Page2;
    public GameObject RootObject;
    public  bool PageNum = true;

    public void OnPageChange()
    {
        if (PageNum)
        {
            Page1.SetActive(false);
            Page2.SetActive(true);
            PageNum = false;
        }
        else if(!PageNum)
        {
            Page1.SetActive(true);
            Page2.SetActive(false);
            PageNum = true;
        }
    }
}
