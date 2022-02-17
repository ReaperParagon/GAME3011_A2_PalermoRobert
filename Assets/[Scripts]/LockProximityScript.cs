using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockProximityScript : MonoBehaviour
{
    [SerializeField]
    public Gradient barFillGradient;

    [SerializeField]
    public Image fillImage;

    private float currentProximity = 0;
    private IEnumerator barFillCoroutine;
    private void OnEnable()
    {
        LockPickingEvents.TryLock += UpdateIndicator;
    }

    private void OnDisable()
    {
        LockPickingEvents.TryLock -= UpdateIndicator;
    }

    /// Functions ///

    public void UpdateIndicator(bool success, float proximity)
    {
        if (barFillCoroutine != null)
            StopCoroutine(barFillCoroutine);

        barFillCoroutine = UpdateIndicatorCoroutine(proximity);

        StartCoroutine(barFillCoroutine);
    }

    /// Coroutines ///

    private IEnumerator UpdateIndicatorCoroutine(float proximity)
    {
        float dist = Mathf.Abs(proximity - currentProximity);

        if (dist < 0.01f)
        {
            barFillCoroutine = null;
            yield break;
        }

        for (float pos = currentProximity; Mathf.Abs(pos - proximity) > 0.01f; pos += (proximity - pos) * 0.1f)
        {
            fillImage.color = barFillGradient.Evaluate(pos);
            currentProximity = pos;

            yield return new WaitForFixedUpdate();
        }

        barFillCoroutine = null;
    }

}
