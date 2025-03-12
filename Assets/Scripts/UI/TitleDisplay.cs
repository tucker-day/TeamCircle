using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TitleDisplay : MonoBehaviour
{
    public Title title;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
       titleText.text=title.title;
       descriptionText.text=title.description;
       scoreText.text= title.score.ToString();
    }

   
}
