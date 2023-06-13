using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    [SerializeField] CameraZoneSwitcherr cameraZoneSwitcherr;
    [SerializeField] MonitorButton monitorButton;

    public float power = 100f;
    public float penurunanPower = 10f;
    public float penambahanPower = 5f;
    public Slider powerSlider;

    public float totalPower;

    //blackout
    [SerializeField] AccessNotebook accessMonitor;
    [SerializeField] GameObject charger;
    [SerializeField] GameObject chargerLamp;
    [SerializeField] GameObject RoomLight;
    [SerializeField] GameObject monitorCollider;
    public List<GameObject> generatorLight = new List<GameObject>();
    public int chance = 10;
    int blackoutChance = 5;
    bool blackoutActive = false;

    


    private void Start()
    {
        totalPower = power;
    }

    private void Update()
    {
        totalPower = Mathf.Clamp(totalPower - penurunanPower * Time.deltaTime, 0f, power);

        if (totalPower <= 0 || chance < blackoutChance)
        {
                //charger
                charger.SetActive(false);
                chargerLamp.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                //lampu ruangan
                RoomLight.SetActive(false);
                //lampu taman
                for (int i = 0; i < monitorButton.virtualLampPost.Count; i++)
                {
                monitorButton.virtualLampPost[i].SetActive(false);
                monitorButton.terrainLampPost[i].SetActive(false);
                monitorButton.lampMaterial[i].color = new Color(0,0,0);;
                monitorButton.lampCollider[i].SetActive(false);
                monitorButton.lampUI[i].color = new Color (255, 0, 0);
                }
                //post door
                for (int i = 0; i < monitorButton.door.Count; i++)
                {
                monitorButton.door[i].SetActive(true);
                monitorButton.areaToEnterPos[i].isTrigger = true;
                monitorButton.doorLamp[i].GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                monitorButton.doorUI[i].color = new Color (255,0 , 0);
                }
                if (cameraZoneSwitcherr.intruderChaseLeftPost == true || cameraZoneSwitcherr.intruderChaseRightPost == true || monitorButton.intruderPostWayOut == true)
                {
                    cameraZoneSwitcherr.NPCLeftPostBait.SetActive(true);
                    cameraZoneSwitcherr.NPCRightPostBait.SetActive(true);
                    monitorButton.NPCPostMediumToRight.SetActive(false);
                    monitorButton.NPCPostMediumToLeft.SetActive(false);
                    monitorButton.intruderPostWayOut.SetActive(false);
                    for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
                        {   
                        cameraZoneSwitcherr.nPCChaseList[i].player = cameraZoneSwitcherr.NPCLeftPostBait;
                        }
                } 
                //monitor UI
                monitorCollider.SetActive(false);
                accessMonitor.monitor.SetActive(false);
                accessMonitor.monitorActive = false;
                accessMonitor.virtualCamera.SetActive(false);
                accessMonitor.playerMovement.jalan = 2.5f;
                accessMonitor.playerMovement.lari = 10;
                //generator Light
                for (int i = 0; i < generatorLight.Count; i++)
                {
                    generatorLight[i].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }

                if (totalPower > 0)
                {
                    blackoutActive = true;
                    chance = 10;
                }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Generator"))
        {

            if (Input.GetKeyDown(KeyCode.F) && blackoutActive == true)
            {
                blackoutActive = false;
                //charger
                charger.SetActive(true);
                chargerLamp.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                //lampu ruangan
                RoomLight.SetActive(true);
                //lampu taman
                for (int i = 0; i < monitorButton.virtualLampPost.Count; i++)
                {
                monitorButton.virtualLampPost[i].SetActive(false);
                monitorButton.terrainLampPost[i].SetActive(false);
                monitorButton.lampMaterial[i].color = new Color(0,0,0);;
                monitorButton.lampCollider[i].SetActive(false);
                monitorButton.lampUI[i].color = new Color (255, 0, 0);
                }
                //post door
                for (int i = 0; i < monitorButton.door.Count; i++)
                {
                monitorButton.door[i].SetActive(true);
                monitorButton.areaToEnterPos[i].isTrigger = true;
                monitorButton.doorLamp[i].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                monitorButton.doorUI[i].color = new Color (255,0 , 0);
                }
                //monitor UI
                monitorCollider.SetActive(true);
                accessMonitor.monitor.SetActive(false);
                accessMonitor.monitorActive = false;
                accessMonitor.virtualCamera.SetActive(false);
                accessMonitor.playerMovement.jalan = 2.5f;
                accessMonitor.playerMovement.lari = 10;
                //generator Light
                for (int i = 0; i < generatorLight.Count; i++)
                {
                    generatorLight[i].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                }
                
            }

            
        }
    }


    private void FixedUpdate()
    {
        powerSlider.value = totalPower / power;
    }

   
}
