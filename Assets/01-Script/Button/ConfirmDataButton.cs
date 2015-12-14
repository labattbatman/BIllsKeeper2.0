using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

public class ConfirmDataButton : MonoBehaviour {

    public GameObject inputFields;
    List<GameObject> inputFieldsList;

    // Use this for initialization
    void Start ()
    {
        inputFieldsList = new List<GameObject>();

        for (int i = 0; i < inputFields.transform.childCount; i++)
        {
            if (inputFields.transform.GetChild(i).name.Contains("Input"))
                inputFieldsList.Add(inputFields.transform.GetChild(i).gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
