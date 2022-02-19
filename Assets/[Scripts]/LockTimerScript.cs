using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockTimerScript : MonoBehaviour
{
    [SerializeField]
    public LockScript currentLock;

    [SerializeField]
    public TextMeshProUGUI timerText;

    [SerializeField]
    public AnimationCurve lockTimerCurve;

    // Timer variables
    public float timeRemaining { get; private set; }
    public bool timerEnabled { get; private set; }

    private void OnEnable()
    {
        LockPickingEvents.LockChanged += StartTimer;
        LockPickingEvents.SuccessfulPick += StopTimer;
    }

    private void OnDisable()
    {
        LockPickingEvents.LockChanged -= StartTimer;
        LockPickingEvents.SuccessfulPick -= StopTimer;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!timerEnabled)
            return;

        timeRemaining -= Time.fixedDeltaTime;
        CheckTimerEnd();
        UpdateTimerText();
    }

    /// Functions ///

    public void StartTimer(LockScript newLock)
    {
        currentLock = newLock;

        timeRemaining = 15.0f * Mathf.FloorToInt(lockTimerCurve.Evaluate(currentLock.lockDifficulty));
        timerEnabled = true;
    }

    public void StopTimer()
    {
        timerEnabled = false;
    }

    public void CheckTimerEnd()
    {
        if (timeRemaining <= 0.0f)
        {
            timeRemaining = 0.0f;
            LockPickingEvents.InvokeOnTimerDone();
            timerEnabled = false;
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = GetTimerFormatted();
    }

    public string GetTimerFormatted()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60.0f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60.0f);

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

}
