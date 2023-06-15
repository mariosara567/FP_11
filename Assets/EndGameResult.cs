using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameResult : MonoBehaviour
{
    public Slider sliderGold;
    public Slider sliderMythril;
    [SerializeField] GameObject fillGold;
    [SerializeField] GameObject fillMythril;
    [SerializeField] Image star;
    [SerializeField] PlayManager playManager;
    // PlayManager playManager;

    float valueGold;
    float valueMythril;
    // menormalize(0,1) max charisma
    float charismaStatus;
    float charismaPoint;

    public int valueIRA;
    public int valueWOC;
    public int valueEIF;
    public int valueINRTotal;
    public int valueIRD;
    public int valueWONCTotal;
    int valueINR;
    int valueWONC;

    public TMP_Text valueIRAText;
    public TMP_Text valueWOCText;
    public TMP_Text valueEIFText;
    public TMP_Text valueINRText;
    public TMP_Text valueIRDText;
    public TMP_Text valueWONCText;

    public GameObject extendContract;
    public GameObject Fired;



    void Update()
    {
        charismaStatus = Mathf.InverseLerp(0,1,playManager.maxCharisma);


        charismaPoint = 1f / playManager.maxCharisma;
        sliderGold.value = playManager.charismaPoint * charismaPoint;
        sliderGold.maxValue = charismaStatus/2;
        sliderMythril.maxValue = charismaStatus/2;
         

        valueGold = sliderGold.value;
        fillGold.transform.localPosition = new Vector3(valueGold, fillGold.transform.localPosition.y, fillGold.transform.localPosition.z);
        Debug.Log("VALUE GOLR: " + valueGold);

        
        if (sliderMythril.value >= sliderMythril.maxValue)
        {
            Debug.Log("BEST SECURITY OF RIVERCAMP");
            star.color = new Color(255, 0, 255);
            extendContract.SetActive(true);
        }
        else if (sliderGold.value >= sliderGold.maxValue)
        {
            Debug.Log("PROLONG CONTRACT");
            star.color = new Color (255, 255, 0);
            extendContract.SetActive(true);
        }
        else
        {
            Debug.Log("FIRED");
            star.color = new Color(200, 200, 200);
            extendContract.SetActive(false);
        }

        sliderMythril.value = playManager.charismaPoint * charismaPoint - sliderGold.maxValue;
        Debug.Log("SLIDERMYTHRIL VALUE: " + sliderMythril.value);

        valueMythril = sliderMythril.value;
        Debug.Log("VALUE MYTHRIL: " + valueMythril);

        fillMythril.transform.localPosition = new Vector3(valueMythril, fillMythril.transform.localPosition.y, fillMythril.transform.localPosition.z);


        valueINR = valueINRTotal - valueIRA;
        valueWONC = valueWONCTotal - valueWOC;

        valueIRAText.text = valueIRA.ToString();
        valueWOCText.text = valueWOC.ToString();
        valueEIFText.text = valueEIF.ToString();
        valueINRText.text = valueINR.ToString();
        valueIRDText.text = valueIRD.ToString();
        valueWONCText.text = valueWONC.ToString();



    }
}
