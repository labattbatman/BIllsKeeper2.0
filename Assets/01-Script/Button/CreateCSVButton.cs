using UnityEngine;
using System.Collections;

public class CreateCSVButton : MonoBehaviour {

    DataManager dataManager;
    StatManager statManager;
    
    // Use this for initialization
	void Start ()
    {
        dataManager = GameObject.Find("Main Camera").GetComponent<DataManager>();
        statManager = GameObject.Find("Main Camera").GetComponent<StatManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateCVS()
    {
        statManager.AnalazeInfoBill();
        string body = dataManager.GetBillData() + "\n" + dataManager.GetSavedStats();
        Application.OpenURL("mailto:alexislebel@hotmail.com?subject=BillsKeeper&body=" + );
    }
}
