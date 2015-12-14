using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class SuggestedChoicesManager : MonoBehaviour {

    public GameObject suggestedChoicesBox;
    public InputField inputFieldRelated;

    MenuManager menuManager;
    const int MAX_SUGGESTED_CHOICES = 6;

    // Use this for initialization
    void Start ()
    {
        suggestedChoicesBox.active = false;
        menuManager = GameObject.Find("Main Camera").GetComponent<MenuManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void ShowSuggestedChoices(List<string> choices)
    {
        if(choices.Count == 0)
        return;

        suggestedChoicesBox.active = true;
        HideChoices();

        for(int i = 0; i < choices.Count; i++)
        {
            GameObject choiceBox = suggestedChoicesBox.transform.GetChild(i).gameObject;
            choiceBox.active = true;
            choiceBox.transform.GetChild(0).gameObject.GetComponent<Text>().text = choices[i];
        }
    }

    public void HideChoices()
    {
        for (int i = 0; i < suggestedChoicesBox.transform.childCount; i++)
        {
            suggestedChoicesBox.transform.GetChild(i).gameObject.active = false;
        }
    }

    void FindInputfieldRelated(InputFieldType.InfoType type)
    {
        inputFieldRelated = menuManager.GetInputField(type);
    }

    public void FindSuggestedChoices(InputFieldType.InfoType type, string input)
    {
        /*if (string.IsNullOrEmpty(input))
        {
            HideChoices();
            return;
        }*/

        if (type == InputFieldType.InfoType.Price)
            return;

        FindInputfieldRelated(type);
        string test = "Banane,Bonne,Bot,Raynor,Rideau,Rot";

        if (type == InputFieldType.InfoType.Date)
        {
            List<string> queryResult = new List<string>();
            for (int i = 0; i < MAX_SUGGESTED_CHOICES; i++)
                queryResult.Add( DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy"));

            ShowSuggestedChoices(queryResult);
        }
        else
        {      
            string[] choicesPool = test.Split(',');

            List<string> queryResult = (
            from w in choicesPool
            where input.Length <= w.Length && input.ToLower() == w.Substring(0, input.Length).ToLower()
            select w).Take(MAX_SUGGESTED_CHOICES).ToList();
            ShowSuggestedChoices(queryResult);
        }   
    }

    
}
