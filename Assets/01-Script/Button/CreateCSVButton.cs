using UnityEngine;
using System.Collections;

public class CreateCSVButton : MonoBehaviour {

    DataManager dataManager;
    
    // Use this for initialization
	void Start ()
    {
        dataManager = GameObject.Find("Main Camera").GetComponent<DataManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateCVS()
    {
        Application.OpenURL("mailto:alexislebel@hotmail.com?subject=BillsKeeper&body=" + dataManager.GetBillData());
    }
}
