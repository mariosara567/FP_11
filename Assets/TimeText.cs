using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{

    [SerializeField] TMP_Text timeText;
    [SerializeField] PlayManager playManager;
    void Update()
    {
 
        timeText.text = "0" + playManager.timeText + ":00";
    }
}
