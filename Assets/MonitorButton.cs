using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorButton : MonoBehaviour
{

    [SerializeField] public List <GameObject> virtualCamera;
    [SerializeField] public List <GameObject> terrainCameraCollider;
    [SerializeField] public List <Light> terrainCameraLight;
    [SerializeField] public List <GameObject> cameraError;

    [SerializeField] public List <GameObject> virtualLampPost;
    [SerializeField] public List <GameObject> terrainLampPost;
    [SerializeField] public List <Material> lampMaterial;
    [SerializeField] public List <GameObject> lampCollider;
    [SerializeField] public List <Image> lampUI;

    [SerializeField] public List <GameObject> door;
    [SerializeField] public List <Collider> areaToEnterPos;
    [SerializeField] public List <GameObject> doorLamp;
    [SerializeField] public List <Image> doorUI;

    [SerializeField] CameraZoneSwitcherr cameraZoneSwitcherr;
    public PlayManager playManager;
    public GameObject intruderPostWayOut;
    public GameObject NPCPostMediumToRight;
    public GameObject NPCPostMediumToLeft;


    

    public void UpdateVirtualCamera(int value)
    {
        

        for (int i = 0; i < virtualCamera.Count; i++)
        {
            virtualCamera[i].SetActive(false);
        }

        virtualCamera[value].SetActive(true);

        
    }

    public void UpdateLampPost(int value)
    {
        if (virtualLampPost[value].activeInHierarchy == false)
        {

            virtualLampPost[value].SetActive(true);   
            terrainLampPost[value].SetActive(true);
            lampCollider[value].SetActive(true);
            lampUI[value].color = new Color (0, 255, 0);

            // lampMaterial[value].SetColor("_Color", Color.white);
            lampMaterial[value].color = new Color(255,255,255);
        }
        else if (virtualLampPost[value].activeInHierarchy == true)
        {
            virtualLampPost[value].SetActive(false);   
            terrainLampPost[value].SetActive(false);
            lampMaterial[value].color = new Color(0,0,0);
            lampCollider[value].SetActive(false);
            lampUI[value].color = new Color (255, 0, 0);
        }   
    }

    public void UpdateDoorLock(int value)
    {
        if (door[value].activeInHierarchy == true)
        {
            //door adalah collider yang memmbuat player bisa/tidak keluar post
            door[value].SetActive(false);
            areaToEnterPos[value].isTrigger = false;
            doorLamp[value].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            doorUI[value].color = new Color (0, 255, 0);


            if (door[0].activeInHierarchy == false && door[1].activeInHierarchy == false)
            {
                cameraZoneSwitcherr.NPCLeftPostBait.SetActive(false);
                cameraZoneSwitcherr.NPCRightPostBait.SetActive(false);
                NPCPostMediumToRight.SetActive(false);
                NPCPostMediumToLeft.SetActive(false);
                intruderPostWayOut.SetActive(true);
                for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                {   
                cameraZoneSwitcherr.nPCChaseList[i].player = intruderPostWayOut;
                }
                
            }
            else if (door[0].activeInHierarchy == false)
            {
                cameraZoneSwitcherr.NPCLeftPostBait.SetActive(false);
                NPCPostMediumToRight.SetActive(true);
                cameraZoneSwitcherr.NPCRightPostBait.SetActive(true);
                for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                {   
                cameraZoneSwitcherr.nPCChaseList[i].player = NPCPostMediumToRight;
                }
            }
            else if (door[1].activeInHierarchy == false)
            {
                cameraZoneSwitcherr.NPCRightPostBait.SetActive(false);
                NPCPostMediumToLeft.SetActive(true);
                cameraZoneSwitcherr.NPCLeftPostBait.SetActive(true);
                for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                {   
                cameraZoneSwitcherr.nPCChaseList[i].player = NPCPostMediumToLeft;
                }
            }
            
            

        } 
        else if (door[value].activeInHierarchy == false)
        {
            door[value].SetActive(true);
            areaToEnterPos[value].isTrigger = true;
            doorLamp[value].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            doorUI[value].color = new Color (255, 0, 0);

            if (door[0].activeInHierarchy == true && door[1].activeInHierarchy == true)
            {
                cameraZoneSwitcherr.NPCLeftPostBait.SetActive(true);
                cameraZoneSwitcherr.NPCRightPostBait.SetActive(true);
                NPCPostMediumToRight.SetActive(false);
                NPCPostMediumToLeft.SetActive(false);
                
                for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                {   
                cameraZoneSwitcherr.nPCChaseList[i].player = cameraZoneSwitcherr.NPCLeftPostBait;
                }
                
            }
            else if (door[0].activeInHierarchy == true)
            {
                cameraZoneSwitcherr.NPCLeftPostBait.SetActive(true);
                intruderPostWayOut.SetActive(false);
                NPCPostMediumToRight.SetActive(false);
                
                for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                {   
                cameraZoneSwitcherr.nPCChaseList[i].player = cameraZoneSwitcherr.NPCLeftPostBait;
                }
            }
            else if (door[1].activeInHierarchy == true)
            {
                cameraZoneSwitcherr.NPCRightPostBait.SetActive(true);
                intruderPostWayOut.SetActive(false);
                NPCPostMediumToLeft.SetActive(false);
                for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                {   
                cameraZoneSwitcherr.nPCChaseList[i].player = cameraZoneSwitcherr.NPCRightPostBait;
                }
            }
        }
    }

}
