using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toilet : MonoBehaviour
{
    [SerializeField] GameObject toiletRoomPanel1;
    [SerializeField] GameObject toiletRoomPanel2;
    [SerializeField] GameObject toiletDoor1;
    [SerializeField] GameObject toiletDoor2;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject player;
    [SerializeField] Collider toiletCollider1;
    [SerializeField] Collider toiletCollider2;
    [SerializeField] TMP_Text text1;
    [SerializeField] TMP_Text text2;
    [SerializeField] Sanity sanity;
    [SerializeField] GameObject gameOverToiletPanel;
    [SerializeField] CameraZoneSwitcherr cameraZoneSwitcherr;
    [SerializeField] GameObject IntruderWayOut;
    [SerializeField] PlayManager playManager;
    public bool toiletEvent1 = false;
    public bool toiletEvent2 = false;
    int tryNum1;
    int tryNum2;
    float chance = 5;
    private void Update()
    {
    

        if (toiletEvent1 == true)
        {
            
           if(Input.GetKeyDown(KeyCode.Space))
           {
             tryNum1++;
             
             float rand = Random.Range(0,10);
             Debug.Log("RAND IS" + rand );
             if (rand >= chance)
             {
                Debug.Log("YOU SUCCEED TO GET OUT" );
                toiletEvent1 = false;
                toiletRoomPanel1.SetActive(false);
                toiletDoor1.SetActive(false);
                toiletCollider1.isTrigger = false;

                player.transform.localPosition = new Vector3(138, -7, -56.5f);
                toiletDoor1.transform.localPosition = new Vector3(3.6f, -0.1f, -0.65f);
                // toiletDoor1.transform.Rotate(-100,180,-90, Space.World);
                sanity.totalSanity = Mathf.Clamp(sanity.totalSanity + sanity.penambahanSanity * Time.deltaTime, 0f, sanity.sanity);
               
                playerMovement.jalan = 2.5f;
                playerMovement.lari = 4;
                for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                    {
                        cameraZoneSwitcherr.nPCChaseList[i].player = player;
                        playManager.player = player;
                    } 
        
             }
             else if(tryNum1 == 3 & rand < chance)
             {
                this.gameObject.SetActive(false);
                toiletRoomPanel1.SetActive(false);
                gameOverToiletPanel.SetActive(true);
                Debug.Log("LOCKED IN TOILET ROOM");

             }
           }
           
        }

        if (toiletEvent2 == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                tryNum2++;
                
                float rand = Random.Range(0,10);
                Debug.Log("RAND IS" + rand );
                if (rand >= chance)
                {
                    Debug.Log("YOU SUCCEED TO GET OUT" );
                    toiletEvent2 = false;
                    toiletRoomPanel2.SetActive(false);
                    toiletDoor2.SetActive(false);
                    toiletCollider2.isTrigger = false;
                    player.transform.localPosition = new Vector3(138, -7, -61.5f);
                    toiletDoor2.transform.localPosition = new Vector3(3.6f, -0.1f, -0.65f);
                    // toiletDoor2.transform.Rotate(-100,180,-90, Space.World);
                    sanity.totalSanity = Mathf.Clamp(sanity.totalSanity + sanity.penambahanSanity * Time.deltaTime, 0f, sanity.sanity);
                    IntruderWayOut.SetActive(false);

                    playerMovement.jalan = 2.5f;
                    playerMovement.lari = 4;   

                    for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                    {
                        cameraZoneSwitcherr.nPCChaseList[i].viewRadius = 1100;
                        cameraZoneSwitcherr.nPCChaseList[i].player = player;
                        playManager.player = player;
                    }   
                }
                else if(tryNum2 == 3 & rand < chance)
                 {
                this.gameObject.SetActive(false);
                toiletRoomPanel2.SetActive(false);
                gameOverToiletPanel.SetActive(true);
                Debug.Log("LOCKED IN TOILET ROOM");

                }
            }   
            
        }
        text1.text = "Try to berge out \nTry count: " + tryNum1 + "/3";
        text2.text = "Try to berge out \nTry count: " + tryNum2 + "/3"; 
    }
            
           


    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("ToiletDoor1"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("TOILET PANEL IS ACTIVE" );
                toiletRoomPanel1.SetActive(true);
                toiletCollider1.isTrigger = true;
                player.transform.localPosition = new Vector3(141.5f, -7, -56.5f);
                toiletDoor1.transform.Rotate(0,0,-26.4f);
                playerMovement.jalan = 0;
                playerMovement.lari = 0;
                toiletEvent1 = true;
                IntruderWayOut.SetActive(true);

                for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                {
                    cameraZoneSwitcherr.nPCChaseList[i].viewRadius = 25;
                    cameraZoneSwitcherr.nPCChaseList[i].player = IntruderWayOut;
                    playManager.player = IntruderWayOut;
                }
                
            }
        }
        if(other.CompareTag("ToiletDoor2"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("TOILET PANEL IS ACTIVE" );
                toiletRoomPanel2.SetActive(true);
                toiletCollider2.isTrigger = true;
                player.transform.localPosition = new Vector3(141.5f, -7, -61.5f);
                toiletDoor2.transform.Rotate(0,0,-26.4f);
                playerMovement.jalan = 0;
                playerMovement.lari = 0;
                toiletEvent2 = true;
                IntruderWayOut.SetActive(true);

                for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                {
                    cameraZoneSwitcherr.nPCChaseList[i].viewRadius = 25;
                    cameraZoneSwitcherr.nPCChaseList[i].player = IntruderWayOut;
                    playManager.player = IntruderWayOut;
                }
                
            }
        }
        
    }
}
