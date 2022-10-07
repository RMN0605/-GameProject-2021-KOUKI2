using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDragon : MonoBehaviour
{
    [SerializeField] GameObject _dragon1;
    [SerializeField] GameObject _dragon2;
    [SerializeField] GameObject _unit_location;

    public void SummonDragon1()
    {
        _dragon1.SetActive(true);
    }

    public void SummonDragon2()
    {
        _dragon2.SetActive(true);
    }

    private void Dragon()
    {
        foreach(Transform child in _unit_location.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
