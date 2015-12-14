using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

public class InputFieldButton : MonoBehaviour {

    public GameObject inputField;
    public Sprite editSprite;
    public Sprite confirmSprite;

    SuggestedChoicesManager suggestedChoicesManager;
    DataManager dataManager;

    // Use this for initialization
    void Start ()
    {
        dataManager = GameObject.Find("Main Camera").GetComponent<DataManager>();
        suggestedChoicesManager = GameObject.Find("Main Camera").GetComponent<SuggestedChoicesManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        bool isEnable = !inputField.GetComponent<InputField>().enabled;

        if (!ValidateInput())
            return;

        inputField.GetComponent<InputField>().enabled = isEnable;

        if (isEnable)
        {
            SetSpriteToConfirm();          
        }
        else
        {
            inputField.GetComponent<Image>().color = Color.gray;
            GetComponent<Image>().sprite = editSprite;

            if (transform.parent.GetComponent<InputFieldType>().infoType == InputFieldType.InfoType.Store)
            {
                suggestedChoicesManager.FindSuggestedChoices(InputFieldType.InfoType.Category, "");
            }

            if (transform.parent.GetComponent<InputFieldType>().infoType == InputFieldType.InfoType.Price)
            {
                suggestedChoicesManager.FindSuggestedChoices(InputFieldType.InfoType.Date, "");
            }
        }       
    }

    bool ValidateInput()
    {
        string input = inputField.GetComponent<InputField>().text;
        if (string.IsNullOrEmpty(input))
            return false;

        switch (transform.parent.GetComponent<InputFieldType>().infoType.ToString())
        {
            case "Date":
                {
                    var regex = new Regex(@"\b\d{2}\/\d{2}\/\d{4}\b");
                    foreach (Match m in regex.Matches(input))
                    {
                        DateTime dt;
                        if (DateTime.TryParseExact(m.Value, "dd/MM/yyyy", null, DateTimeStyles.None, out dt))
                            return true;
                    }
                    return false;
                }
            case "Price":
                {
                    var regex = new Regex(@"^\d+\.\d{2}$");
                    foreach (Match m in regex.Matches(input))
                    {
                        return true;
                    }
                    foreach (Match m in regex.Matches(input + ".00"))
                    {
                        inputField.GetComponent<InputField>().text += ".00";
                        return true;
                    }
                    return false;
                }
            default:
                {
                    string newText = RemplaceAccentChar(inputField.GetComponent<InputField>().text);
                    inputField.GetComponent<InputField>().text = char.ToUpper(newText[0]) + newText.Substring(1);
                    break;
                }
        }

        return true;
    }

    string RemplaceAccentChar(string text)
    {
        string accent = "ÀÁÂÃÄÅÇÐÈÉÊËÌÍÎÏÑÒÓÔÕÖŠÙÚÛÜÝŸŽ";
        string remplace = "AAAAAACDEEEEIIIINOOOOOSUUUUYYZ";

        for (int i = 0; i < accent.Length; i++)
        {
            text = text.ToUpper().Replace(accent[i], remplace[i]);
        }

        return text.ToLower();
    }

    public void SetSpriteToConfirm()
    {
        inputField.GetComponent<Image>().color = Color.white;
        GetComponent<Image>().sprite = confirmSprite;
    }


}
