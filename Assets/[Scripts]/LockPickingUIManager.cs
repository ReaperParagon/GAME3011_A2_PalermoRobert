using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockPickingUIManager : MonoBehaviour
{
    [Header("Lock Picking Minigame")]
    [SerializeField]
    public GameObject lockPickingUI;

    [SerializeField]
    public GameObject lockPickingOptionsUI;

    [SerializeField]
    public LockPickingScript lockPickingMiniGame;

    [Header("Results Properties")]
    [SerializeField]
    public GameObject resultsUI;

    [SerializeField]
    public TextMeshProUGUI resultsLabel;

    [SerializeField]
    public LockTimerScript lockTimer;

    [Header("Lock Properties")]
    [SerializeField]
    public GameObject lockBody;

    [Header("Player Skill Properties")]
    [SerializeField]
    public PlayerSkill playerSkill;

    [SerializeField]
    public TextMeshProUGUI playerSkillLevelLabel;

    private void OnEnable()
    {
        LockPickingEvents.ChangeCursorVisible += ToggleCursor;
        LockPickingEvents.LockChanged += SetUIInformation;
        LockPickingEvents.TimerDone += FailedPicking;
        LockPickingEvents.SuccessfulPick += SucceedPicking;
    }
    private void OnDisable()
    {
        LockPickingEvents.ChangeCursorVisible -= ToggleCursor;
        LockPickingEvents.LockChanged -= SetUIInformation;
        LockPickingEvents.TimerDone -= FailedPicking;
        LockPickingEvents.SuccessfulPick -= SucceedPicking;
    }


    /// Functions ///

    public void ToggleCursor(bool enable)
    {
        if (enable)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void EnablePickingOptionsUI(bool enable)
    {
        lockPickingOptionsUI.SetActive(enable);

        LockPickingEvents.InvokeOnChangeCursorVisible(enable);
    }

    public void EnablePickingUI(bool enable)
    {
        lockPickingUI.SetActive(enable);
        LockPickingScript.allowInput = enable;

        if (enable)
        {
            lockPickingMiniGame.GetNewLock();
            LockPickingEvents.InvokeOnChangeCursorVisible(false);
        }
    }

    public void EnableResultsUI(bool enable)
    {
        resultsUI.SetActive(enable);

        // Remove Check if you want to go back to no cursor after exiting results
        if (enable)
            LockPickingEvents.InvokeOnChangeCursorVisible(enable);
    }

    public void FailedPicking()
    {
        LockPickingScript.allowInput = false;

        resultsLabel.text = "You ran out of time!";
        EnableResultsUI(true);
    }

    public void SucceedPicking()
    {
        LockPickingScript.allowInput = false;

        resultsLabel.text = "Successfully picked the lock with " + lockTimer.GetTimerFormatted() + " remaining!";
        EnableResultsUI(true);
    }

    public void SetUIInformation(LockScript currentLock)
    {
        // Set Player Skill
        playerSkillLevelLabel.text = playerSkill.lockpickingSkill.ToString();

        // Set Colour
        lockBody.GetComponent<Image>().color = Color.Lerp(Color.white, Color.red, currentLock.lockDifficulty);
    }
}
