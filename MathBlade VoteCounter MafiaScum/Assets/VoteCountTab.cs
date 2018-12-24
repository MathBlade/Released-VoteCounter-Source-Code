using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteCountTab : MonoBehaviour {

    private VoteCountTabManager Manager;

    private Color _oldColor;
    private static Color mouseOverColor = new Color(0.1829585f, 0.1222967f, 0.6397059f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void TabClicked()
    {
        GameObject.Find("Master Panel").GetComponent<VoteCountTabManager>().SetTabVisible(this);
    }

    public void SetVisible(bool isVisible)
    {
        //gameObject.SetActive(isVisible);
        gameObject.transform.FindChildByRecursion("Content").gameObject.SetActive(isVisible);
        Button button = gameObject.transform.FindChildByRecursion("Button").GetComponent<Button>();
        ColorBlock tabButtonColors = button.colors;       
        tabButtonColors.normalColor = (isVisible) ? Color.white : Color.gray;
        
        button.colors = tabButtonColors;

    }
}

public static class TabManagerHelper
{
    public static Transform FindChildByRecursion(this Transform transform, string childName)
    {
        if (transform == null) return null;
        var result = transform.Find(childName);
        if (result != null)
            return result;
        foreach (Transform child in transform)
        {
            result = child.FindChildByRecursion(childName);
            if (result != null)
                return result;
        }
        return null;
    }
}
