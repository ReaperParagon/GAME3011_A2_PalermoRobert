using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKeyScript : MonoBehaviour
{
    [SerializeField]
    public GameObject angleIndicator;

    private void OnEnable()
    {
        LockPickingEvents.TargetAngleChanged += UpdateAngleIndicator;
        ToggleKey(false);
    }
    private void OnDisable()
    {
        LockPickingEvents.TargetAngleChanged -= UpdateAngleIndicator;
    }

    /// Functions ///

    public void ToggleKey(bool enable)
    {
        angleIndicator.SetActive(enable);
    }

    public void UpdateAngleIndicator(float angle)
    {
        angleIndicator.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }
}
