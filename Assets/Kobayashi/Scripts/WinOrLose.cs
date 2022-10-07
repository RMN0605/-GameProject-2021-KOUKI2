using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOrLose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void Win_Or_Lose(bool isWinLose)
    {
        if (isWinLose == false)
        {
            SceneManager.LoadScene("Result");
            TextChange.is_witch = false;
        }
        else if (isWinLose == true)
        {
            SceneManager.LoadScene("Result");
            TextChange.is_witch = true;
        }
    }
}
