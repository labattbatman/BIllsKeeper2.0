using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputFieldType : MonoBehaviour {


	
    public enum InfoType
    {
        None,
        Store,
        Date,
        Price,
        Category,
    }

    public InfoType infoType;
    SuggestedChoicesManager suggestedChoicesManager;
    InputField inputField;

    // Use this for initialization
    void Start()
    {
        suggestedChoicesManager = GameObject.Find("Main Camera").GetComponent<SuggestedChoicesManager>();
        inputField = transform.GetChild(0).GetComponent<InputField>();

        inputField.placeholder.GetComponent<Text>().text = "Enter " + infoType + "...";
    }

    public void OnInputEnter()
    {
        suggestedChoicesManager.FindSuggestedChoices(infoType, inputField.text);
    }

    
}
