using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestionDialog : MonoBehaviour
{
    [SerializeField]
    Text messageTxt;

    Func<int> callBack;

    public void Popup(string message, Func<int> callBack)
    {
        gameObject.SetActive(true);
        this.messageTxt.text = message;
        this.callBack = callBack;
    }

    public void Yes()
    {
        callBack();
        gameObject.SetActive(false);
    }

    public void No()
    {
        gameObject.SetActive(false);
    }
}