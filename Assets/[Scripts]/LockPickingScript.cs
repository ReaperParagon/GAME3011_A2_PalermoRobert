using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockPickingScript : MonoBehaviour
{
    [SerializeField]
    public LockScript currentLock;

    [SerializeField]
    public GameObject lockPick;

    [SerializeField]
    public GameObject lockBody;

    // Lock Variables
    private float targetAngle;
    private float targetRange;
    private float hintRange;


    // Pick Variables
    private float currentAngle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = lockPick.transform.eulerAngles.z;

        print(CheckCurrentAngle());
    }

    /// Functions ///

    public void GetNewLock()
    {
        targetAngle = currentLock.GetNewLockAngle();

        // Setup difficulty of the lock
        targetRange = 5 * currentLock.lockDifficulty;
        hintRange = 20 * currentLock.lockDifficulty;

        lockBody.GetComponent<Image>().color = Color.Lerp(Color.red, Color.white, currentLock.lockDifficulty);

        // Send New Lock Event
        LockPickingEvents.InvokeOnLockChange(currentLock);
    }

    public bool CheckCurrentAngle()
    {
        if (Mathf.Abs(targetAngle - currentAngle) < targetRange)
            return true;

        return false;
    }

    public float GetCurrentAngleProximity()
    {
        float distanceToTarget = Mathf.Abs(targetAngle - currentAngle);

        return 0.0f;
    }

}
