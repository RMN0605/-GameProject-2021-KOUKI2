using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTitle : MonoBehaviour
{
    public void OnClickStartButton()
    {
        //SceneManager.LoadScene("Title");
        SceneManager.LoadScene("mainBattleScene(tuusinn)");
    }
}
