using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LockDifficulty
{
    Simple = 1,
    Sturdy,
    Tricky,
    Complex,
    Elaborate,
    Impossible
}

public class LockScript : MonoBehaviour
{
    [SerializeField]
    public AnimationCurve difficultyNameCurve;

    [SerializeField]
    public NameSet difficultyNames;

    [SerializeField, Range(0.0f, 1.0f)]
    public float lockDifficulty = 0.5f;

    [SerializeField, Range(30.0f, 350.0f)]
    public float lockRange = 180.0f;
    public float lockAngle { get; private set; }

    /// Functions ///

    public float GetNewLockAngle()
    {
        lockAngle = Random.Range(lockRange * 0.05f, lockRange - (lockRange * 0.05f));
        return lockAngle;
    }

    public LockDifficulty GetDifficulty()
    {
        return (LockDifficulty)Mathf.FloorToInt(difficultyNameCurve.Evaluate(lockDifficulty));
    }

    public string GetDifficultyName()
    {
        return difficultyNames.GetName((int)GetDifficulty() - 1);
    }
}
