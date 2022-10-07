using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    [SerializeField] CreateCard createCard;
    public void OnclickButton()
    {
        createCard.Create();
    }
}
