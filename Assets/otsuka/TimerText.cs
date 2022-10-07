using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    //���ԁA���A�b��\������p
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

        //60����1���Ԃɒu������
        if (minute >= 60)
        {
            hour++;
            minute = 0;
        }
        //60�b��1���ɒu������
        if (second > 60f)
        {
            minute++;
            second -= 60f;
        }

        //���Ԃ�\��
        timerText.text = hour.ToString() + ":" + minute.ToString("00") + ":" + second.ToString("f2");
    }
}
