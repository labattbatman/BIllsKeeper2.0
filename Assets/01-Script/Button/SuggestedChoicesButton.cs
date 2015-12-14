using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SuggestedChoicesButton : MonoBehaviour
{

    SuggestedChoicesManager suggestedChoicesManager;

    // Use this for initialization
    void Start ()
    {
	    suggestedChoicesManager = GameObject.Find("Main Camera").GetComponent<SuggestedChoicesManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        suggestedChoicesManager.inputFieldRelated.text = transform.GetChild(0).GetComponent<Text>().text;
        suggestedChoicesManager.HideChoices();
    }
}
