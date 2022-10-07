using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brainwashing : MonoBehaviour
{
    [SerializeField]
    List<GameObject> unitList = new List<GameObject>();
    [Header("î≠ìÆéûä‘/ïb")]
    public float attack_delay;  //çUåÇÉÇÅ[ÉVÉáÉì
    private float nowDelay; //åªç›ÇÃÉÇÅ[ÉVÉáÉìéûä‘
    [Header("êÙî]éûä‘(ìGÇñ°ï˚Ç…Ç∑ÇÈéûä‘)/ïb")]
    public float brainwashing_delay;  //çUåÇÉÇÅ[ÉVÉáÉì
    private float nowbrainwashingDelay; //åªç›ÇÃÉÇÅ[ÉVÉáÉìéûä‘

    private bool isMagic = false;
    private void Start()
    {
        StartMagic();
    }
    private void Update()
    {
        if (attack_delay >= nowDelay)
        {
            nowDelay += Time.deltaTime;
        }
        else if (attack_delay < nowDelay)
        {
            StartMagic();
            isMagic = true;
        }

        if (isMagic)
        {
            if (brainwashing_delay >= nowbrainwashingDelay)
            {
                nowbrainwashingDelay += Time.deltaTime;
            }
            else if (brainwashing_delay < nowbrainwashingDelay)
            {
                EndMagic();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D co)
    {
        if (!isMagic)
        {
            if (co.gameObject.CompareTag("Unit1") || co.gameObject.CompareTag("Unit2"))
            {
                unitList.Add(co.gameObject);
            }
        }
    }

    /// <summary>
    /// êÙî]äJén
    /// </summary>
    private void StartMagic()
    {
        if (CompareTag("StrategyCard1"))
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i] == null)
                    unitList.Remove(unitList[i]);
                else if (unitList[i].CompareTag("Unit2"))
                {
                    if (unitList[i] == null)
                        unitList.Remove(unitList[i]);

                    unitList[i].tag = "Unit1";
                    Transform unitTransform = unitList[i].transform;
                    Vector3 localAngle = unitTransform.localEulerAngles;
                    localAngle.y = 0.0f;
                    unitTransform.localEulerAngles = localAngle;

                }
            }
        }
        if (CompareTag("StrategyCard2"))
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i] == null)
                    unitList.Remove(unitList[i]);
                else if (unitList[i].CompareTag("Unit1"))
                {
                    if (unitList[i] == null)
                        unitList.Remove(unitList[i]);
                    unitList[i].tag = "Unit2";
                    Transform unitTransform = unitList[i].transform;
                    Vector3 localAngle = unitTransform.localEulerAngles;
                    localAngle.y = 180.0f;
                    unitTransform.localEulerAngles = localAngle;
                }
            }
        }
    }
    /// <summary>
    /// êÙî]âèú
    /// </summary>
    private void EndMagic()
    {
        if (CompareTag("StrategyCard1"))
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i] == null)
                    unitList.Remove(unitList[i]);
                else if (unitList[i].CompareTag("Unit1"))
                {
                    unitList[i].tag = "Unit2";
                    Transform unitTransform = unitList[i].transform;
                    Vector3 localAngle = unitTransform.localEulerAngles;
                    localAngle.y = 180f;
                    unitTransform.localEulerAngles = localAngle;
                }
            }
        }
        if (CompareTag("StrategyCard2"))
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i] == null)
                    unitList.Remove(unitList[i]);
                else if (unitList[i].CompareTag("Unit2"))
                {
                    unitList[i].tag = "Unit1";
                    Transform unitTransform = unitList[i].transform;
                    Vector3 localAngle = unitTransform.localEulerAngles;
                    localAngle.y = 0.0f;
                    unitTransform.localEulerAngles = localAngle;
                }
            }
        }
        Destroy(this.gameObject);
    }
}
