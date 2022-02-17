using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickingEvents
{
    public delegate void OnLockChange(LockScript newLock);

    public static event OnLockChange LockChanged;

    public static void InvokeOnLockChange(LockScript newLock)
    {
        LockChanged?.Invoke(newLock);
    }


    public delegate void OnTargetAngleChange(float targetAngle, float targetRange);

    public static event OnTargetAngleChange TargetAngleChanged;

    public static void InvokeOnTargetAngleChange(float targetAngle, float targetRange)
    {
        TargetAngleChanged?.Invoke(targetAngle, targetRange);
    }
}
