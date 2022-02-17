using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkeletonKeyScript : MonoBehaviour
{
    [SerializeField]
    public GameObject angleIndicatorPrefab;

    private GameObject indicatorLeft;
    private GameObject indicatorRight;

    private void OnEnable()
    {
        // Create the left and right indicators
        indicatorLeft = Instantiate(angleIndicatorPrefab, transform);
        indicatorRight = Instantiate(angleIndicatorPrefab, transform);

        LockPickingEvents.TargetAngleChanged += UpdateAngleIndicator;
        SetKeyEnabled(false);
    }
    private void OnDisable()
    {
        LockPickingEvents.TargetAngleChanged -= UpdateAngleIndicator;
    }

    /// Functions ///

    public void ToggleKey()
    {
        SetKeyEnabled(!indicatorLeft.activeSelf);
    }

    public void SetKeyEnabled(bool enable)
    {
        indicatorLeft.SetActive(enable);
        indicatorRight.SetActive(enable);
    }

    public void UpdateAngleIndicator(float angle, float range)
    {
        indicatorLeft.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle + range);
        indicatorRight.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - range);
    }

    /// Input System ///

    public void OnToggleSkeletonKey(InputValue value)
    {
        ToggleKey();
    }

}
