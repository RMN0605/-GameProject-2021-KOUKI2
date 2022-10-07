using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance = null;

    [HideInInspector]
    public Unit_manager unitManager;
    [HideInInspector]
    public AiManager aiManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        unitManager = GetComponent<Unit_manager>();
        aiManager = GetComponent<AiManager>();
    }

}
