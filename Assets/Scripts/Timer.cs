using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    private float timer = 0;
    public bool isTimerActive = false;
    [SerializeField] private TextMeshProUGUI timerText;
    private int sec = 0;
    private int min = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isTimerActive = true;
    }

    private void Update()
    {
        if (isTimerActive)
        {
            timer += Time.deltaTime;
            sec = Mathf.FloorToInt(timer % 60);
            min = Mathf.FloorToInt(timer / 60);
            if(sec < 10)
                timerText.text = min + ":0" + sec ;
            else
                timerText.text = min + ":" + sec ;
        }
    }

    public void StopTimer()
    {
        isTimerActive = false;
    }
}
