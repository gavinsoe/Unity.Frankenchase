using UnityEngine;
using System.Collections;

public class GUIBaseClass :MonoBehaviour 
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
