using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkill", menuName = "ScriptableObject/PlayerSkill")]
public class PlayerSkill : ScriptableObject
{
    [SerializeField, Range(1, 100)]
    public int lockpickingSkill;
}
