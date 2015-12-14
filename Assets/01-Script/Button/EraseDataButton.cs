using UnityEngine;
using System.Collections;

public class EraseDataButton : MonoBehaviour {

    DataManager dataManager;

    public GameObject confirmEraseDataButton;

    // Use this for initialization
    void Start()
    {
        dataManager = GameObject.Find("Main Camera").GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void ShowEraseData()
    {
        confirmEraseDataButton.active = !confirmEraseDataButton.active;
    }

    public void EraseData()
    {
        dataManager.EraseData();
    }
}
