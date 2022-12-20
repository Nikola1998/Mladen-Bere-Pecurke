using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkinDatabase : ScriptableObject
{
    public Skin[] skin;

    public int skinCount
    {
        get
        {
            return skin.Length;
        }
    }

    public Skin GetSkin(int index)
    {
        return skin[index];
    }
}
