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
    public GameObject lockBody;

    [SerializeField]
    public AnimationCurve skillDifficultyCurve;

    [SerializeField]
    public AnimationCurve lockDifficultyCurve;

    [SerializeField]
    public AnimationCurve numberOfPinsCurve;

    [SerializeField]
    public TextMeshProUGUI pinsText;

    // Lock Variables
    private float targetAngle;
    private float targetRange;
    private int numPins;
    private int currentPin;

    // Pick Variables
    private float currentAngle;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GetNewLock();
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = lockPick.angle;
    }

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
        pinsText.text = numPins.ToString();

        lockBody.GetComponent<Image>().color = Color.Lerp(Color.white, Color.red, currentLock.lockDifficulty);

        // Send New Lock Event
        LockPickingEvents.InvokeOnLockChange(currentLock);

        // Send Target Angle Changed Event
        LockPickingEvents.InvokeOnTargetAngleChange(currentLock.lockAngle + lockPick.rotationOffset, targetRange);
    }

    public void GetNewPin()
    {
        targetAngle = currentLock.GetNewLockAngle();

        // Display information on Number of pins done and pins remaining
        pinsText.text = (numPins - currentPin).ToString();

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

    public bool CheckCurrentPin()
    {
        if (currentPin >= numPins)
            return true;

        return false;
    }

    /// Input System ///

    public void OnTryLock(InputValue value)
    {
        if (value.isPressed)
        {
            LockPickingEvents.InvokeOnTryLock(CheckCurrentAngle(), CheckAngleProximity());

            if (CheckCurrentAngle())
            {
                if (CheckCurrentPin())
                {
                    // Lock is Done
                    pinsText.text = (numPins - currentPin).ToString();

                    print("Unlocked!");
                    GetNewLock();
                }
                else
                {
                    // Next Pin
                    GetNewPin();
                }
            }
        }
    }

    /// Debug ///

    public void PrintTryLockInfo()
    {
        print("Unlocked? : " + CheckCurrentAngle() + "... Proximity : " + CheckAngleProximity() + "...");
    }
}
