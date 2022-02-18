using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickingEvents
{
    /// Lock Change ///

    public delegate void OnLockChange(LockScript newLock);

    public static event OnLockChange LockChanged;

    public static void InvokeOnLockChange(LockScript newLock)
    {
        LockChanged?.Invoke(newLock);
    }


    /// Target Angle Change ///

    public delegate void OnTargetAngleChange(float targetAngle, float targetRange);

    public static event OnTargetAngleChange TargetAngleChanged;

    public static void InvokeOnTargetAngleChange(float targetAngle, float targetRange)
    {
        TargetAngleChanged?.Invoke(targetAngle, targetRange);
    }


    /// Try Lock ///

    public delegate void OnTryLock(bool unlocked, float proximity);

    public static event OnTryLock TryLock;

    public static void InvokeOnTryLock(bool unlocked, float proximity)
    {
        TryLock?.Invoke(unlocked, proximity);
    }

    /// Timer Done ///

    public delegate void OnTimerDone();

    public static event OnTimerDone TimerDone;

    public static void InvokeOnTimerDone()
    {
        TimerDone?.Invoke();
    }
}
