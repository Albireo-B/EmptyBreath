using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinScreen : MonoBehaviour
{
    public Text txt;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        txt.text = score.ToString() + "conserves récupérés";
    }
}
