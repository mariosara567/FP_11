using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoteBookText : MonoBehaviour
{
     [SerializeField] public TMP_Text text;
     [SerializeField] PlayManager playManager;
     //untuk men-instantiate recordedText
     [SerializeField] GameObject recordedText;
     //untuk mengirim submitCount Notebook pada submitCount recordedTextData
     [SerializeField] SubmitedText recordedTextData;

    // untuk mengaktifkan deleteButton ke x|x = submitCount 
    [SerializeField] public List <GameObject> deleteButton;

    //untuk mengirim submitCount Notebook pada submitCount DeleteButton
    [SerializeField] public List <DeleteButton> deleteButtonData;
    [SerializeField] Button submitButton;

     Intruder intruder;

     public Transform recordParent;
 
     string intruderType;

     string intruderLocationToText;
     //location list terdiri dari A yang mencakup A1, A2, A3, dan A4, yang datanya diambil dari input untuk di check
     List <string> locationList = new List<string>();
    //  string matchLocation;
    //  string intruderLocation;

     string intruderTimeToText;
     //intruder time terdiri dari 5,4,3,2,1,dan 0 yang menandakan jam (5 == 00:00)
     int intruderTime;


    
    bool submitActive = false;
    bool intruderTypeTextInput = false;
    bool intruderLocationTextInput = false;
    bool intruderTimeTextInput = false;

    //submitCount = 1 karena instantiate dipanggil terlebih dahulu sebelum submitCount++
    public int submitCount = 1;
    public List <GameObject> submitTextList = new List<GameObject>();
    public List <string> submitTextListData = new List<string>();


    //membagi deleteButton yang aktif dengan datanya masing-masing sehingga data CP tidak akan keliru pada deleteButton yang mana.
    public int deleteButtonPositionInList = 0;

    bool inputActive = true;
    string doubleCheckText;

     
    private void Update()
    {
        text.text = intruderType + " / " + intruderLocationToText + " / " + intruderTimeToText;

        if (intruderTypeTextInput == true && intruderLocationTextInput == true && intruderTimeTextInput == true && inputActive == true )
        {
            submitButton.interactable = true;
        }
        
        for (int m = 0; m < submitTextList.Count ; m++)
        {
            if (text.text == submitTextListData[m] )
            {
                submitButton.interactable = false;
                inputActive = false;
                doubleCheckText = text.text;
                Debug.Log("DATA ALREADY REPORTED, CANNOT DO IT TWICE");
            }
        }

        if (submitCount >= 10)
        {
            submitButton.interactable = false;
            inputActive = false;
            Debug.Log("DATA HAS REACHED LIMIT");
        }

        if (text.text != doubleCheckText)
            inputActive = true;

                
    }

    public void UpdateTypeText(string value)
    {
        intruderType = value;
        intruderTypeTextInput = true;
    }
    public void UpdateLocationText(string value)
    {
        intruderLocationToText = value;
        intruderLocationTextInput = true;
    }
    public void UpdateTimeText(string value)
    {
        intruderTimeToText = value;
        intruderTimeTextInput = true;
    }
    public void UpdateLocation(string value)
    {
        locationList.Add(value);
    }
    public void UpdateTime(int value)
    {
        intruderTime = value;
    }
    public void UpdateSubmitActive()
    {
        submitActive = true;

        if (submitActive == true)
        {
            Debug.Log("SUBMITACTIVE");
            IntruderCheck();
            

            recordedTextData.submitCount = submitCount;
            recordedTextData.submitText.text = text.text;
            var addRecordedText = Instantiate(recordedText, recordParent);
            // addRecordedText.transform.parent = GameObject.Find("NoteBook").transform;
            submitTextList.Add(addRecordedText);
            submitTextListData.Add(recordedTextData.submitText.text);
            // mereset location list yang di input setelah submit dan pengecekan
            locationList.Clear();

            // mengaktifkan delete button terpilih
            for (int i = 0; i < submitCount; i++)
            {
                deleteButtonData[i].submitCount = submitCount;
                deleteButton[i].SetActive(true);  
            }

            intruderTypeTextInput = false;
            intruderLocationTextInput = false;
            intruderTimeTextInput = false;
            submitButton.interactable = false;
            intruderType = "";
            intruderLocationToText = "";
            intruderTimeToText = "";
            Debug.Log("INTRUDER TO TEXT DATA IS SET TO FALSE");
            
            submitActive = false;
            submitCount++;

            
        }
    }

    



    // memindahkan data terinput ke recorded data
    public void IntruderCheck()
    {
        Debug.Log(string.Join("," , locationList));
  
    for (int i = 0; i < playManager.instantiateIntruderList.Count; i++)
            {
                // Debug.Log("DATA CHECKING...");
                var matchType = playManager.instantiateIntruderList[i];
                var intruderTypeInput = intruderType;     

                Debug.Log("MATCHTYPE: " + matchType.intruderType);
                Debug.Log("INTRUDERTYPEINPUT: " + intruderTypeInput);

                if (matchType.intruderType == intruderTypeInput && submitActive == true)
                {
                    Debug.Log("LEVEL 1: INTRUDER TYPE MATCH");
                    for (int j = 0; j < matchType.intruderLocation.Count; j++)
                    {
                        var matchLocation = matchType.intruderLocation[j].ToString();

                        for (int k = 0; k < locationList.Count; k++)
                        {
                            var matchLocationInput = locationList[k] + " (UnityEngine.GameObject)";
                            Debug.Log("MATCHLOCATION: " + matchLocation);
                            Debug.Log("MATCHLOCATIONINPUT: " + matchLocationInput);
                            
                            
                            if (matchLocation == matchLocationInput && submitActive == true)
                            {
                                Debug.Log("LEVEL 2: INTRUDER LOCATION MATCH");
                                for (int l = 0; l < matchType.intruderTime.Count; l++)
                                {
                                    // Debug.Log("MATCHTYPE.INTRUDERTIME.COUNT IS: " + matchType.intruderTime.Count);
                                    
                                    var matchTime = matchType.intruderTime[l];
                                    var intruderTimeInput = intruderTime;
                                    Debug.Log("MATCHTIME: " + matchTime);
                                    Debug.Log("INTRUDERTIMEINPUT: " + intruderTimeInput);
                                    if (matchTime == intruderTimeInput && submitActive == true)
                                    {
                                        //charisma point bertambah
                                        Debug.Log("REPORT ACCEPTED");
                                        Debug.Log("DELETEBUTTONPOSITIONINLIST IS: " + deleteButtonPositionInList);
                                        playManager.charismaPoint++;
                                        deleteButtonData[deleteButtonPositionInList].charismaPointStatus = true;
                                        deleteButtonPositionInList++;
                                        Debug.Log("CHARISMA POINT IS: " + playManager.charismaPoint);
                                        submitActive = false;

                                        //menghapus data yang berhasil dan benar
                                        // matchType.intruderTime.RemoveAt(l);
                                        // matchType.intruderLocation.RemoveAt(j);

                                        Debug.Log("TEXT IS: " + text.text);
                                        // input yang sama tidak dapat dipanggil kedua kalinya
                                        

                                    }
                                    else if(matchTime != intruderTimeInput
                                        && l == matchType.intruderTime.Count - 1 
                                        && submitActive == true)
                                    {
                                        //charisma point berkurang
                                        Debug.Log("REPORT DECLINED IN TIME");
                                        playManager.charismaPoint--;
                                        deleteButtonData[deleteButtonPositionInList].charismaPointStatus = false;
                                        deleteButtonPositionInList++;
                                        Debug.Log("CHARISMA POINT IS: " + playManager.charismaPoint);
                                        submitActive = false;
                                    }
                                }
                            }
                            else if(matchLocation != matchLocationInput 
                                    && k == locationList.Count - 1 
                                    && j == matchType.intruderLocation.Count - 1 
                                    && i == playManager.instantiateIntruderList.Count - 1
                                    && submitActive == true)
                                {
                                    //charisma point berkurang
                                    Debug.Log("REPORT DECLINED UNTIL LAST INSTANTIATE LIST BUT DECLINED IN POSITION");
                                    playManager.charismaPoint--;
                                    deleteButtonData[deleteButtonPositionInList].charismaPointStatus = false;
                                    deleteButtonPositionInList++;
                                    Debug.Log("CHARISMA POINT IS: " + playManager.charismaPoint);
                                    submitActive = false;
                                    
                                }
                            
                        } 
                    }
                    // count - 1, karena dicek sampai range data terakhir (bukan list.count nya)
                    // j adalah poaitionList.count dari intruder, yang bisa saja nilainya nol karena tidak aktif namun terinstantiate
                    if( matchType.intruderLocation.Count == 0
                            && i == playManager.instantiateIntruderList.Count - 1 
                            && submitActive == true)
                                {
                                    Debug.Log("REPORT DECLINED UNTIL LAST INSTANTIATE LIST BUT TYPE'S MATCH");
                                    playManager.charismaPoint--;
                                    deleteButtonData[deleteButtonPositionInList].charismaPointStatus = false;
                                    deleteButtonPositionInList++;
                                    Debug.Log("CHARISMA POINT IS: " + playManager.charismaPoint);
                                    submitActive = false;
                                }
                }
                else if(matchType.intruderType != intruderTypeInput 
                && i == playManager.instantiateIntruderList.Count - 1 
                && submitActive == true)
                            {
                                //charisma point berkurang
                                Debug.Log("REPORT DECLINED UNTIL LAST INSTANTIATE LIST");
                                playManager.charismaPoint--;
                                deleteButtonData[deleteButtonPositionInList].charismaPointStatus = false;
                                deleteButtonPositionInList++;
                                Debug.Log("CHARISMA POINT IS: " + playManager.charismaPoint);
                                submitActive = false;
                            }
            }  
    }  
}
