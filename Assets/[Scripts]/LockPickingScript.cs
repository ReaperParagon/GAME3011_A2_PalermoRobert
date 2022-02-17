using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LockPickingScript : MonoBehaviour
{
    [SerializeField]
    public LockScript currentLock;

    [SerializeField]
    public LockPickScript lockPick;

    [SerializeField]
    public GameObject lockBody;

    // Lock Variables
    private float targetAngle;
    private float targetRange;

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
        targetRange = 20 * currentLock.lockDifficulty;

        lockBody.GetComponent<Image>().color = Color.Lerp(Color.red, Color.white, currentLock.lockDifficulty);

        // Send New Lock Event
        LockPickingEvents.InvokeOnLockChange(currentLock);

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

    /// Input System ///

    public void OnTryLock(InputValue value)
    {
        if (value.isPressed)
        {
            LockPickingEvents.InvokeOnTryLock(CheckCurrentAngle(), CheckAngleProximity());

            // PrintTryLockInfo();
        }
    }

    /// Debug ///

    public void PrintTryLockInfo()
    {
        print("Unlocked? : " + CheckCurrentAngle() + "... Proximity : " + CheckAngleProximity() + "...");
    }
}
