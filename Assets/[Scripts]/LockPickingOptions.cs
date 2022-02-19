using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LockPickingOptions : MonoBehaviour
{
    [SerializeField]
    public LockScript currentLock;

    [SerializeField]
    public PlayerSkill playerSkill;

    [Header("Labels")]
    [SerializeField]
    private TextMeshProUGUI difficultyLabel;
    [SerializeField]
    private TextMeshProUGUI rangeLabel;
    [SerializeField]
    private TextMeshProUGUI skillLabel;

    [Header("Sliders")]
    [SerializeField]
    private Slider difficultySlider;
    [SerializeField]
    private Slider rangeSlider;
    [SerializeField]
    private Slider skillSlider;

    private void OnEnable()
    {
        OnChangePlayerSkill(playerSkill.lockpickingSkill);
        skillSlider.value = playerSkill.lockpickingSkill;
        OnChangeDifficulty(currentLock.lockDifficulty);
        difficultySlider.value = currentLock.lockDifficulty;
        OnChangeRange(currentLock.lockRange);
        rangeSlider.value = currentLock.lockRange;
    }

    public void OnChangeDifficulty(float value)
    {
        currentLock.lockDifficulty = value;

        difficultyLabel.text = "Difficulty: " + currentLock.GetDifficultyName();
    }

    public void OnChangeRange(float value)
    {
        currentLock.lockRange = value;

        rangeLabel.text = "Lock Range (Degrees): " + ((int)value).ToString();
    }

    public void OnChangePlayerSkill(float value)
    {
        playerSkill.lockpickingSkill = (int)value;

        skillLabel.text = "Player Lock Picking Skill: " + ((int)value).ToString();
    }

}
