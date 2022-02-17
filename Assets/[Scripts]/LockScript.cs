using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)]
    public float lockDifficulty = 0.5f;

    [SerializeField, Range(30.0f, 350.0f)]
    public float lockRange = 180.0f;

    public float lockAngle { get; private set; }

    private void Awake()
    {
        GetNewLockAngle();
    }

    /// Functions ///

    public float GetNewLockAngle()
    {
        lockAngle = Random.Range(10.0f, lockRange - 10.0f);
        return lockAngle;
    }
}
