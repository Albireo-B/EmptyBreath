using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    int score = 0;
    // Start is called before the first frame update
    private void Start()
    {
        scoreText.text = "Nourriture : " + score;
    }

    public void MorePoint()
    {
      
        score++;
        scoreText.text = "Nourriture : " + score;
    }

 
}
