using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InputSchema")]
public class Inputschema : ScriptableObject
{
    public string horizontal_axis;
    public string vertical_axis;

    public KeyCode[] play;
    public KeyCode[] back;


    public bool GetKey(KeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (Input.GetKey(key)) return true;
        }
        return false;
    }
}
