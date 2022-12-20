using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private string SELECTED_OPTION = "selectedOption";

    public SkinDatabase SkinDatabase;

    public TextMeshProUGUI nameText;
    private GameObject skin;

    [SerializeField]
    private GameObject skinForChanging, parent;

    private int selectedOption = 0;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(SELECTED_OPTION))
        {
            selectedOption = 0;
            UpdateCharacter(0);
        }
        else
        {
            Load();
            UpdateCharacter(selectedOption);
        }
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= SkinDatabase.skinCount)
            selectedOption = 0;

        UpdateCharacter(selectedOption);
        Save();
    }

    public void UpdateCharacter(int index)
    {
        Skin skin = SkinDatabase.GetSkin(index);
        GameObject newSkin = Instantiate(skin.characterSkin.gameObject, skinForChanging.transform.position, skinForChanging.transform.rotation);
        Destroy(skinForChanging);
        skinForChanging = newSkin;
        if (nameText != null)
            nameText.SetText(skin.characterName);
    }

    public void PreviousOption()
    {
        selectedOption--;
        if (selectedOption < 0)
            selectedOption = SkinDatabase.skinCount - 1;

        UpdateCharacter(selectedOption);
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(SELECTED_OPTION, selectedOption);
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt(SELECTED_OPTION);

    }
}
