using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    [SerializeField] Text text;
    [SerializeField] Transform carTransform;
    [SerializeField] Transform StartLineTransform;
    [SerializeField] Transform FinishLineTransform;

    private bool started;

    private float timeValue;
    private float collideDistance;
    private float minTime;

    private string seconds;
    private string minutes;
    private string hours;

    public void Restart()
    {
        minTime = timeValue + 20f;
    }

    // Start is called before the first frame update
    void Start()
    {
        seconds = "00";
        minutes = "00";
        hours = "00";

        started = false;
        timeValue = 0f;
        collideDistance = 4f;
        minTime = 20f;

        text.text = "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            timeValue += Time.deltaTime;

            UpdateTimerDisplay();

            checkForFinishing();
        }
        else
        {
            checkForStarting();
        }
    }

    private void checkForStarting()
    {
        float dist = Vector3.Distance(carTransform.position, StartLineTransform.position);
        Debug.Log("Distance to line : " + dist);

        if(dist < collideDistance)
        {
            started = true;
        }
    }

    private void checkForFinishing()
    {
        float dist = Vector3.Distance(carTransform.position, FinishLineTransform.position);
        Debug.Log("Distance to line : " + dist);

        if(dist < collideDistance && timeValue > minTime)
        {
            started = false;
            timeValue = 0f;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        float time = timeValue;

        // hours
        int hoursValue = (int)Math.Floor(time / 3600);
        time -= (hoursValue * 3600);

        if(hoursValue < 10)
        {
            hours = "0" + hoursValue.ToString();
        }
        else
        {
            hours = hoursValue.ToString();
        }

        // minutes
        int minutesValue = (int)Math.Floor(time / 60);
        time -= (minutesValue * 60);

        if(minutesValue < 10)
        {
            minutes = "0" + minutesValue.ToString();
        }
        else
        {
            minutes = minutesValue.ToString();
        }

        // seconds
        int secondsValue = (int)Math.Floor(time);

        if(secondsValue < 10)
        {
            seconds = "0" + secondsValue.ToString();
        }
        else
        {
            seconds = secondsValue.ToString();
        }


        text.text = hours + ":" + minutes + ":" + seconds;
    }
}
