using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraZoneSwitcherr : MonoBehaviour
{
    [SerializeField] PlayManager playManager;
    [SerializeField] MonitorButton monitorButton;
    [SerializeField] NPCChasePlayer nPCChasePlayer;
    [SerializeField] Toilet toilet;
    [SerializeField] GameObject pauseMenu;
    
    [SerializeField] Power power;
    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera[] virtualCameras;
    public GameObject playerGameObject;
    public TMP_Text night1Text;

    public List <NPCChasePlayer> nPCChaseList = new List<NPCChasePlayer>();
    public List <NPCChasePlayer> nPCChaseInPostList = new List<NPCChasePlayer>();

    // darimana intruder akan muncul di post room
    public GameObject NPCLeftPostBait;
    public GameObject NPCRightPostBait;
    public GameObject NPCGeneratorBait;
    public bool NPCInPosition = false;
    public bool chaseEvent = false;
    public bool intruderChaseLeftPost = false;
    public bool intruderChaseRightPost = false;
    public bool intruderChaseGenerator = false;
    public float timer;
    public float OutTimer = 1;
    public float timerToPost; 
    public float timerToRemove = 5;
    public float penurunanVisibilitas = 3f;
    public float visibilitas = 255f;
    public float visibleValue;

    bool pauseMenuActive = false;

    private void Start()
    {
        SwitchToCamera(primaryCamera);
        visibleValue = visibilitas;
    }
    
    private void Update()
    {
        //Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuActive == false)
        {
            pauseMenu.SetActive(true);
            pauseMenuActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuActive == true)
        {
            pauseMenu.SetActive(false);
            pauseMenuActive = false;
        }

        if (nPCChaseList.Count == 0 && nPCChaseInPostList.Count == 0)
        {
            chaseEvent = false;
        }

        // intruder menghilang saat kedua pintu terkunci
        if ( monitorButton.door[0].activeInHierarchy == false && monitorButton.door[1].activeInHierarchy == false)
            {
                if (timerToRemove <= 0)
                {
                    for (int i = 0; i < nPCChaseList.Count; i++)
                    {   
                        nPCChaseList[i].gameObject.SetActive(false);

                    }
                }
                timerToRemove -=Time.deltaTime;
            }
        else if (toilet.toiletEvent1 == true || toilet.toiletEvent2 == true)
        {

                if (timerToRemove <= 0)
                {
                    for (int i = 0; i < nPCChaseList.Count; i++)
                    {   
                        nPCChaseList[i].gameObject.SetActive(false);

                    }
                }
                timerToRemove -=Time.deltaTime;
        }
        else
        {
            timerToRemove = 5;
        }

        // "Night 1" di beginning
        float visibleValue = Mathf.Clamp(visibilitas - penurunanVisibilitas * Time.deltaTime, 0, visibilitas);
        night1Text.color = new Color(255, 255, 255, visibleValue);
        

        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pos1"))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera);

            // Mendapatkan referensi komponen Transform pada player
            Transform playerTransform = playerGameObject.transform;

            // Mengubah posisi player ke koordinat yang ditentukan
            playerTransform.position = new Vector3(555, -10, -95);

            // kalau player masuk post room, intruder akan mengejar pintu
            for (int i = 0; i < nPCChaseList.Count; i++)
            {   
                nPCChaseList[i].player = NPCLeftPostBait;
            }

            NPCLeftPostBait.SetActive(true);
            
            // saat player masuk ke pintu kiri, intruder akan masuk ke pos dari posisi pintu kiri
            intruderChaseLeftPost = true;
            timer = 1;
            timerToPost = 1;
        }
        if (other.CompareTag("BackPos1"))
        {
            SwitchToCamera(primaryCamera);

            // Mendapatkan referensi komponen Transform pada player
            Transform playerTransform = playerGameObject.transform;

            // Mengubah posisi player ke koordinat yang ditentukan
            playerTransform.position = new Vector3(39, -7, -105.5f);

            // kalau player keluar dan intruder belum di pintu, intruder akan mengejar player
            for (int i = 0; i < nPCChaseList.Count; i++)
            {   
                nPCChaseList[i].player = playerGameObject;
            }

            //intruder akan keluar pos dengan interval 1 detik antar intruder
            for (int i = 0; i <= nPCChaseInPostList.Count; i++)
            {   
                Debug.Log(nPCChaseInPostList.Count);
                Invoke("IntruderLeftPostOut", OutTimer);
                OutTimer++; 
            }
            NPCLeftPostBait.SetActive(false);
            NPCRightPostBait.SetActive(false);
            OutTimer = 1;
            
        }


        if (other.CompareTag("Pos2"))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera);

            // Mendapatkan referensi komponen Transform pada player
            Transform playerTransform = playerGameObject.transform;

            // Mengubah posisi player ke koordinat yang ditentukan
            playerTransform.position = new Vector3(568, -10, -95);

            for (int i = 0; i < nPCChaseList.Count; i++)
            {   
                nPCChaseList[i].player = NPCRightPostBait;
            }

            NPCRightPostBait.SetActive(true);
            intruderChaseRightPost = true;
            timer = 1;
            timerToPost = 1; 
        }
        if (other.CompareTag("BackPos2"))
        {
            SwitchToCamera(primaryCamera);

            // Mendapatkan referensi komponen Transform pada player
            Transform playerTransform = playerGameObject.transform;

            // Mengubah posisi player ke koordinat yang ditentukan
            playerTransform.position = new Vector3(51, -7, -105.5f);

            for (int i = 0; i < nPCChaseList.Count; i++)
            {   
                nPCChaseList[i].player = playerGameObject;
            }

            for (int i = 0; i <= nPCChaseInPostList.Count; i++)
            {   
                Debug.Log(nPCChaseInPostList.Count);
                Invoke("IntruderRightPostOut", OutTimer);
                OutTimer++; 
            }

            NPCLeftPostBait.SetActive(false);
            NPCRightPostBait.SetActive(false);
            OutTimer = 1;
        }


        if (other.CompareTag("GeneratorRoom1"))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera);

            // Mendapatkan referensi komponen Transform pada player
            Transform playerTransform = playerGameObject.transform;

            // Mengubah posisi player ke koordinat yang ditentukan
            playerTransform.position = new Vector3(565, -10, -42);


            for (int i = 0; i < nPCChaseList.Count; i++)
            {   
                nPCChaseList[i].player = NPCGeneratorBait;
            }

            NPCGeneratorBait.SetActive(true);
            intruderChaseGenerator = true;
            if (power.blackoutActive == false)
            {
                power.generatorSFX.Play();
            }
            
            timer = 1;  
                    
        }
        if (other.CompareTag("BackGeneratorRoom1"))
        {
            SwitchToCamera(primaryCamera);

            // Mendapatkan referensi komponen Transform pada player
            Transform playerTransform = playerGameObject.transform;

            // Mengubah posisi player ke koordinat yang ditentukan
            playerTransform.position = new Vector3(-41, -7, -57.5f);

            for (int i = 0; i < nPCChaseList.Count; i++)
            {   
                nPCChaseList[i].player = playerGameObject;
            }

            for (int i = 0; i <= nPCChaseInPostList.Count; i++)
            {   
                Debug.Log(nPCChaseInPostList.Count);
                Invoke("IntruderGeneratorOut", OutTimer);
                OutTimer++; 
            }
            NPCGeneratorBait.SetActive(false); 
            power.generatorSFX.Stop();
            OutTimer = 1;

                   
        }


        
    }

    private void OnTriggerStay(Collider other)
    {
        //saat player dikejar, intruder akan dapat masuk ke post room
        if (other.CompareTag("Player in post room") && chaseEvent == true)
        {


            //intruder yang telah menyentuh pintu akan berpindah
            //intruder kedua dst tidak perlu menyentuh pintu dan akan langsung berpindah 1 detik setelah intruder sebelumnya.
            // Debug.Log("CHASE EVENT: " + intruderChaseLeftPost + NPCInPosition + timer + nPCChaseList.Count);
            if (intruderChaseLeftPost == true && NPCInPosition == true)
            {
                
                if(timer <= 0 && nPCChaseList.Count >= 1)
                {

                            nPCChaseList[0].gameObject.transform.position = new Vector3 (555, -9.2f, -95);
                            nPCChaseList[0].moveSpeed = 30;
                            nPCChaseInPostList.Add(nPCChaseList[0]);
                            nPCChaseList.RemoveAt(0);
                            timer = 1;

                            NPCLeftPostBait.SetActive(false);
                }
                
                
                timer -=Time.deltaTime;
            }
            else if (intruderChaseRightPost == true && NPCInPosition == true )
            {
                
                if(timer <= 0 && nPCChaseList.Count >= 1)
                {

                            nPCChaseList[0].gameObject.transform.position = new Vector3 (568, -9.2f, -95);
                            nPCChaseList[0].moveSpeed = 30;
                            nPCChaseInPostList.Add(nPCChaseList[0]);
                            nPCChaseList.RemoveAt(0);
                            timer = 1;

                            NPCRightPostBait.SetActive(false);
                }
                    
                timer -=Time.deltaTime;
            } 
            else if (intruderChaseGenerator == true && NPCInPosition == true )
            {
                
                if(timer <= 0 && nPCChaseList.Count >= 1)
                {
                        nPCChaseList[0].gameObject.transform.position = new Vector3 (565, -9.2f, -42);
                        nPCChaseList[0].moveSpeed = 30;
                        nPCChaseInPostList.Add(nPCChaseList[0]);
                        nPCChaseList.RemoveAt(0);
                        timer = 1;

                        NPCGeneratorBait.SetActive(false);
                }
                    
                timer -=Time.deltaTime;
            } 
        }

        if (other.CompareTag("Player in post room") && chaseEvent == false)
        {
            //intruder akan masuk post saat terspawn di LPA atau RPA
            for (int i = 0; i < playManager.instantiateIntruderTerrainList.Count; i++)
            {
                if (playManager.instantiateIntruderList[i].objectChild.activeInHierarchy == true
                && playManager.instantiateIntruderList[i].spawnArea == "LPA")
                {
                    if (timerToPost <= 0 && monitorButton.door[0].activeInHierarchy == true)
                    {
                        Debug.Log("LPA ACTIVE " + monitorButton.door[0].activeInHierarchy );
                        playManager.instantiateIntruderTerrainList[i].objectChild.transform.parent = playManager.instantiateIntruderTerrainList[i].nPCChasePlayer.objectParent;
                        playManager.instantiateIntruderTerrainList[i].objectChild.transform.position = new Vector3 (555, -9.2f, -95);
                        playManager.instantiateIntruderTerrainList[i].nPCChasePlayer.moveSpeed = 30;
                        nPCChaseInPostList.Add(playManager.instantiateIntruderTerrainList[i].nPCChasePlayer);
                        NPCLeftPostBait.SetActive(false);
                        playManager.instantiateIntruderTerrainList[i].gameObject.SetActive(false);
                        timerToPost = 10;
                    }  
                }

                if (playManager.instantiateIntruderList[i].objectChild.activeInHierarchy == true
                && playManager.instantiateIntruderList[i].spawnArea == "RPA")
                {
                    if (timerToPost <= 0 && monitorButton.door[1].activeInHierarchy == true)
                    {
                        Debug.Log("RPA ACTIVE " + monitorButton.door[1].activeInHierarchy);
                        playManager.instantiateIntruderTerrainList[i].objectChild.transform.parent = playManager.instantiateIntruderTerrainList[i].nPCChasePlayer.objectParent;
                        playManager.instantiateIntruderTerrainList[i].objectChild.transform.position = new Vector3 (568, -9.2f, -95);
                        playManager.instantiateIntruderTerrainList[i].nPCChasePlayer.moveSpeed = 30;
                        nPCChaseInPostList.Add(playManager.instantiateIntruderTerrainList[i].nPCChasePlayer);
                        NPCRightPostBait.SetActive(false);
                        playManager.instantiateIntruderTerrainList[i].gameObject.SetActive(false);
                        timerToPost = 10;
                    }
                }
            }
            timerToPost -=Time.deltaTime;
        }
    }

        



    private void IntruderLeftPostOut()
    {
        if( nPCChaseInPostList.Count >= 1 )
        {
            Debug.Log(nPCChaseInPostList[0]);
            nPCChaseInPostList[0].gameObject.transform.position = new Vector3 (39, -6.2f, -105.5f);
            nPCChaseInPostList[0].moveSpeed = 14;
            nPCChaseList.Add(nPCChaseInPostList[0]);
            nPCChaseInPostList.RemoveAt(0);
            
            intruderChaseLeftPost = false;
            intruderChaseRightPost = false;
            NPCInPosition = false;
            
        }      

    }

    private void IntruderRightPostOut()
    {

        if( nPCChaseInPostList.Count >= 1 )
        {
            Debug.Log(nPCChaseInPostList[0]);
            nPCChaseInPostList[0].gameObject.transform.position = new Vector3 (51, -6.2f, -105.5f);
            nPCChaseInPostList[0].moveSpeed = 14;
            nPCChaseList.Add(nPCChaseInPostList[0]);
            nPCChaseInPostList.RemoveAt(0);
            
            intruderChaseLeftPost = false;
            intruderChaseRightPost = false;
            NPCInPosition = false;
        }    

    }

    private void IntruderGeneratorOut()
    {

        if( nPCChaseInPostList.Count >= 1 )
        {
            Debug.Log(nPCChaseInPostList[0]);
            nPCChaseInPostList[0].gameObject.transform.position = new Vector3 (-41, -6.2f, -57.5f);
            nPCChaseInPostList[0].moveSpeed = 14;
            nPCChaseList.Add(nPCChaseInPostList[0]);
            nPCChaseInPostList.RemoveAt(0);
            
            intruderChaseLeftPost = false;
            intruderChaseRightPost = false;
            NPCInPosition = false;
            
        }    

    }

        private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (CinemachineVirtualCamera camera in virtualCameras)
        {
            camera.Priority = camera == targetCamera ? 10 : 0;
        }
    }
}

