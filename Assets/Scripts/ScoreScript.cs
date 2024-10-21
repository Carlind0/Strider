using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public static int ScoreValue = 0;
    TMP_Text score;
    // Start is called before the first frame update
     void Start()
    {
        ScoreValue=0;
        score = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score:" + ScoreValue;
    }
    
}
