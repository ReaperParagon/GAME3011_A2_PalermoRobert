using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NameSet", menuName = "ScriptableObject/NameSet")]
public class NameSet : ScriptableObject
{
    [SerializeField]
    private List<string> names;

    public string GetName(int index)
    {
        if (index >= 0 && index < names.Count)
        {
            return names[index];
        }

        return "Not in range";
    }
}
