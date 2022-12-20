using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    public GameObject[] skinPrefabs;

    private void Start()
    {
        int index = PlayerPrefs.GetInt("selectedOption");
        GameObject prefab = skinPrefabs[index];
        for(int i = 0; i < skinPrefabs.Length; i++)
        {
            if (i != index)
                skinPrefabs[i].SetActive(false);
        }
    }
}
