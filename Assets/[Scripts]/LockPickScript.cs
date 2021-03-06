using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockPickScript : MonoBehaviour
{
    [SerializeField]
    public GameObject lockpickBody;

    [SerializeField]
    public Animator lockpickAnimator;

    [SerializeField, Range(0.01f, 2.0f)]
    public float rotateSensitivity = 0.02f;

    private float maxAngle;
    public float rotationOffset { get; private set; }

    public float angle { get; private set; }

    private bool canRotate = true;

    private void OnEnable()
    {
        LockPickingEvents.LockChanged += SetupLockPick;

        canRotate = true;
        lockpickAnimator.SetBool("TryingLock", false);
    }

    private void OnDisable()
    {
        LockPickingEvents.LockChanged -= SetupLockPick;
    }

    /// Functions ///

    private void SetupLockPick(LockScript newLock)
    {
        maxAngle = newLock.lockRange;
        rotationOffset = -(maxAngle * 0.5f);

        angle = maxAngle * 0.5f;
        lockpickBody.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle + rotationOffset);
    }

    /// Input System ///

    public void OnRotate(InputValue value)
    {
        if (!LockPickingScript.allowInput)
            return;

        if (!canRotate)
            return;

        angle -= value.Get<float>() * rotateSensitivity;
        angle = Mathf.Clamp(angle, 0, maxAngle);

        lockpickBody.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle + rotationOffset);
    }

    public void OnTryLock(InputValue value)
    {
        if (!LockPickingScript.allowInput)
            return;

        if (value.isPressed)
        {
            lockpickAnimator.SetBool("TryingLock", true);
            LockPickingAudioManager.instance.PlayAudio(LockPickAudioClips.TryLock);
        }

        canRotate = !value.isPressed;
    }

    public void OnAbortTryLock(InputValue value)
    {
        if (!LockPickingScript.allowInput)
            return;

        if (LockPickingAudioManager.instance.GetIsPlayingClip(LockPickAudioClips.TryLock))
            LockPickingAudioManager.instance.StopAudio();

        lockpickAnimator.SetBool("TryingLock", false);
        canRotate = !value.isPressed;
    }
}
