using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class LockPickingScript : MonoBehaviour
{
    [SerializeField]
    public PlayerSkill playerSkill;

    [SerializeField]
    public LockScript currentLock;

    [SerializeField]
    public LockPickScript lockPick;

    [SerializeField]
    public AnimationCurve skillDifficultyCurve;

    [SerializeField]
    public AnimationCurve lockDifficultyCurve;

    [SerializeField]
    public AnimationCurve numberOfPinsCurve;

    [SerializeField]
    public TextMeshProUGUI remainingPinsLabel;

    // Lock Variables
    private float targetAngle;
    private float targetRange;
    private int numPins;
    private int currentPin;

    // Pick Variables
    private float currentAngle 
    {
        get
        {
            return lockPick.angle;
        }
    }

    // Minigame Variables

    public static bool allowInput { get; set; }


    /// Functions ///

    public void GetNewLock()
    {
        targetAngle = currentLock.GetNewLockAngle();

        // Setup difficulty of the lock
        float lockDiff = lockDifficultyCurve.Evaluate(currentLock.lockDifficulty);
        float skillDiff = skillDifficultyCurve.Evaluate(playerSkill.lockpickingSkill * 0.01f);
        float rangeDiff = currentLock.lockRange * 0.25f;

        targetRange = rangeDiff * lockDiff * skillDiff;

        numPins = Mathf.FloorToInt(numberOfPinsCurve.Evaluate(currentLock.lockDifficulty));
        currentPin = 1;
        remainingPinsLabel.text = numPins.ToString();

        // Send New Lock Event
        LockPickingEvents.InvokeOnLockChange(currentLock);

        // Send Target Angle Changed Event
        LockPickingEvents.InvokeOnTargetAngleChange(currentLock.lockAngle + lockPick.rotationOffset, targetRange);
    }

    public void GetNewPin()
    {
        targetAngle = currentLock.GetNewLockAngle();

        // Display information on Number of pins done and pins remaining
        remainingPinsLabel.text = (numPins - currentPin).ToString();

        if (currentPin < numPins)
            ++currentPin;

        // Send Target Angle Changed Event
        LockPickingEvents.InvokeOnTargetAngleChange(currentLock.lockAngle + lockPick.rotationOffset, targetRange);
    }

    public bool CheckCurrentAngle()
    {
        if (Mathf.Abs(targetAngle - currentAngle) < targetRange)
            return true;

        return false;
    }

    public float CheckAngleProximity()
    {
        float distanceToTarget = Mathf.Abs(targetAngle - currentAngle);

        if (distanceToTarget < targetRange)
            return 1.0f;

        return 1.0f - ((distanceToTarget - targetRange) / currentLock.lockRange);
    }

    public bool CheckDonePins()
    {
        if (currentPin >= numPins)
        {
            remainingPinsLabel.text = (numPins - currentPin).ToString();
            return true;
        }

        // Next Pin
        GetNewPin();

        return false;
    }

    /// Input System ///

    public void OnTryLock(InputValue value)
    {
        if (!LockPickingScript.allowInput)
            return;

        if (value.isPressed)
        {
            LockPickingEvents.InvokeOnTryLock(CheckCurrentAngle(), CheckAngleProximity());

            if (CheckCurrentAngle() && CheckDonePins())
            {
                // Lock is Done
                LockPickingEvents.InvokeOnSuccessfulPick();
            }
        }
    }

    /// Debug ///

    public void PrintTryLockInfo()
    {
        print("Unlocked? : " + CheckCurrentAngle() + "... Proximity : " + CheckAngleProximity() + "...");
    }
}
