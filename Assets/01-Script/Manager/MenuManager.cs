using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject billInfoMenu;
    public GameObject inputFields;

    // Use this for initialization
    void Start () {

        ShowMainMenu();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowMainMenu()
    {
        ResetInputFields();
        billInfoMenu.active = false;
        mainMenu.active = true;
    }

    public void ShowBillInfoMenu()
    {
        billInfoMenu.active = true;
        mainMenu.active = false;
    }

    public InputField GetInputField(InputFieldType.InfoType type)
    {
        for(int i = 0; i < inputFields.transform.childCount; i++)
        {
            if(inputFields.transform.GetChild(i).GetComponent<InputFieldType>().infoType == type)
            {
                return inputFields.transform.GetChild(i).GetChild(0).GetComponent<InputField>();
            }
        }

        return null;
    }

    public void BillInfoEnter()
    {
        string billInfo = string.Empty;

        for (int i = 0; i < inputFields.transform.childCount; i++)
        {
            if (inputFields.transform.GetChild(i).GetChild(0).GetComponent<InputField>().enabled)
                return;

            string info = inputFields.transform.GetChild(i).GetChild(0).GetComponent<InputField>().text;
            billInfo += info + ",";

            if (i == 0)
            {
                GetComponent<DataManager>().AddNewStore(info);
            }
            else if (i == 1)
            {
                GetComponent<DataManager>().AddNewCategory(inputFields.transform.GetChild(0).GetChild(0).GetComponent<InputField>().text, info);
            }

        }
        GetComponent<DataManager>().AddNewBill(billInfo);
        ResetInputFields();
    }

    void ResetInputFields()
    {
        for (int i = 0; i < inputFields.transform.childCount; i++)
        {
            inputFields.transform.GetChild(i).GetChild(1).GetComponent<InputFieldButton>().OnClick();
            inputFields.transform.GetChild(i).GetChild(0).GetComponent<InputField>().text = string.Empty;
        }
    }


}
