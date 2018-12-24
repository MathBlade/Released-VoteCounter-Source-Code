using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteCountTabManager : MonoBehaviour {

    public VoteCountTab[] AllTabs;
	// Use this for initialization
	void Start () {
        SetTabVisible(AllTabs[0]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void SetTabVisible(VoteCountTab tab)
    {
        foreach(VoteCountTab aTab in AllTabs)
        {
            aTab.SetVisible(aTab == tab);
        }
    }
}
