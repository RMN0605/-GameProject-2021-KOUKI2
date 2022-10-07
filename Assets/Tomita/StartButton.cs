using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    public void StartGame()
    {
        GeneralManager.instance.unitManager.UnitMoney = 50;
        GeneralManager.instance.unitManager.UnitMoney2 = 50;
        // GameSceneÇÉçÅ[Éh
        SceneManager.LoadScene("mainBattleScene(tuusinn)");
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("mainBattleScene(tuusinn)");
    }
}

