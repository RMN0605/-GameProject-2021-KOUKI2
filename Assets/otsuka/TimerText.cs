using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    //時間、分、秒を表示する用
    private float second;
    private int minute;
    private int hour;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        second += Time.deltaTime;

        //60分→1時間に置き換え
        if (minute >= 60)
        {
            hour++;
            minute = 0;
        }
        //60秒→1分に置き換え
        if (second > 60f)
        {
            minute++;
            second -= 60f;
        }

        //時間を表示
        timerText.text = hour.ToString() + ":" + minute.ToString("00") + ":" + second.ToString("f2");
    }
}
