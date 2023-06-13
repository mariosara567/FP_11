using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{

    [SerializeField] List <GameObject> evidenceItemPrefabList;
    [SerializeField] List <EvidenceItem> evidenceItemDataList;
    [SerializeField] List <GameObject> evidenceItemTerrainPositionList;

    [SerializeField] List <GameObject> warningObjectPrefabList;
    [SerializeField] List <WarningObject> warningObjectDataList;    
    [SerializeField] List <GameObject> warningObjectVirtualPositionList;
    [SerializeField] List <GameObject> warningObjectTerrainPositionList;
    
    [SerializeField] List <GameObject> intruderPrefabList;
    [SerializeField] List <Intruder> intruderDataList;
    [SerializeField] List <GameObject> intruderVirtualPositionList;
    [SerializeField] List <GameObject> intruderTerrainPositionList;
    
    [SerializeField] Intruder intruderNPC;
    [SerializeField] float initialTimer = 10;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] MonitorButton monitorButton;
    public Transform virtualParent;
    public Transform terrainParent;
   

    public List <GameObject> intruderAreaLPA;
    public List <GameObject> intruderAreaRPA;
    public List <GameObject> intruderAreaFPA;
    public List <GameObject> intruderAreaLCA;
    public List <GameObject> intruderAreaRCA;
    public List <GameObject> intruderAreaRRA;
    public List <GameObject> intruderAreaLRA;

    public List <Intruder> instantiateIntruderList = new List<Intruder>();
    public List <Intruder> instantiateIntruderTerrainList = new List<Intruder>();


    public List <WarningObject> warningObjectInstantiateList = new List<WarningObject>();

    // posisi-posisi spawn dan move yang berhasil (direset di jam berikutnya )
    public List <GameObject> SpawnPositionList = new List<GameObject>();
    public List <string> AreaPositionList = new List<string>();



    // waktu interval instantiate
    float timer;

    // waktu perpindahan jam (00:00 - 05:00)
    int gameTime = 5; 
    bool timerActive = true;
    bool endGameActive = false;

    // spawn chance
    int evidenceItemSpawnChance = 5;
    int warningObjectSpawnChance = 5;
    int IntruderSpawnChance = 5;
    int cameraBrokenChance = 5;

    // charisma point
    public int charismaPoint;
    public int maxCharisma;

    public CameraZoneSwitcherr cameraZoneSwitcherr;
    public Power power;
    public GameObject player;
    public Transform objectParent;




    public void Start()
    {
        
        timer = initialTimer;
        int randomEvidenceItem = Random.Range(1,3);
        int randomWarningObject = Random.Range(1,3);
        int randomIntruder = Random.Range(1,3);
        // Debug.Log("INTRUDER PREFAB SPAWN COUNT IS: " + randomIntruder);

        //instantiate
        for (int i = randomEvidenceItem; i > 0; i--)
        {
            InstantiateEvidenceItem();
        }

        for (int i = randomWarningObject; i > 0; i--)
        {
            InstantiateWarningObject();          
        }

        for (int i = randomIntruder; i > 0; i--)
        {
            // Debug.Log("INTRUDER PREFAB SPAWN");
            
            IntruderInstantiate();

            SpawnPositionList.Clear();
            AreaPositionList.Clear();
            // Debug.Log("DATA CLEARED");
            
        }
        // Debug.Log(string.Join("," , instantiateIntruderList));

        for (int i = 0; i < monitorButton.terrainCameraLight.Count; i++)
        {
            monitorButton.terrainCameraLight[i].color = new Color(0, 255, 0);
        }
        



        gameTime--;
        timer = 10;
    }

    public void Update()
    {


        // Debug.Log("GAMETIME IS " + gameTime);
        if (gameTime < -1)
            {
                timerActive = false;
                endGameActive = true;
                gameTime = 3;
                // Debug.Log("TIME OVER");
            }

        //instantiate akan berjalan setiap jam gametime
        if(timer <= 0 && timerActive == true)
        {
            // maksimal spawn (1,3)
            int randomEvidenceItem = Random.Range(1,3);
            int randomWarningObject = Random.Range(1,3);
            int randomIntruder = Random.Range(1,3);
            int randomCamera = Random.Range(0,10);
            power.chance = Random.Range(0,10);
            // Debug.Log("INTRUDER PREFAB SPAWN COUNT IS: " + randomIntruder);

            //setiap jam (setelah instantiate) akan di cek intruder yang aktif akan menambah maxCharisma
            for (int i = 0; i < instantiateIntruderList.Count; i++)
            {
                if (instantiateIntruderList[i].gameObject.activeInHierarchy == true)
                {
                    maxCharisma++;
                    // Debug.Log("MAX CHARISMA IS: " + maxCharisma);
                }
            }
            
            //Instantiate
            for (int i = randomEvidenceItem; i > 0; i--)
            {
                InstantiateEvidenceItem();
            }

            for (int i = randomWarningObject; i > 0; i--)
            {
                InstantiateWarningObject();          
            }

            for (int i = randomIntruder; i > 0; i--)
            {
                // Debug.Log("INTRUDER PREFAB SPAWN IN UPDATE");
                
                IntruderInstantiate();

                SpawnPositionList.Clear();
                AreaPositionList.Clear();
                // Debug.Log("DATA CLEARED");                
            }

            // chance camera broken 
            if (randomCamera < cameraBrokenChance)
            {
            // Debug.Log("CAMERA CHANCE IS: " + randomCamera);
            int randomCameraError = Random.Range(0, monitorButton.cameraError.Count - 1);  
            monitorButton.cameraError[randomCameraError].SetActive(true);
            monitorButton.terrainCameraCollider[randomCameraError].SetActive(true);
            monitorButton.terrainCameraLight[randomCameraError].color = new Color(255, 0, 0);
            randomCamera = 10;
            }



            // SpawnPositionList.RemoveRange(0, SpawnPositionList.Count - 1);
    
            gameTime--;
            timer = 10;
        }


        if (timerActive == false && endGameActive == true)
        {
            // Debug.Log("END GAME SCREEN ACTIVE");
            endGamePanel.SetActive(true);
            endGameActive = false;
            
            // Debug.Log(instantiateIntruderList.Count);
            // Debug.Log(string.Join("," , instantiateIntruderList[0].intruderLocation));
            // Debug.Log(string.Join("," , instantiateIntruderList[0].intruderTime));

        
        }

        timer -=Time.deltaTime;
    }

    public void IntruderInstantiate()
    {
            
            int rand = Random.Range(0,intruderPrefabList.Count);
            var prefab = intruderPrefabList[rand];
            var prefabData = intruderDataList[rand];

            

            

            //playManager.gametime = intruder.gametime
            prefabData.gameTime = gameTime;
            intruderNPC.gameTime = gameTime;

            //memasukkan data ke nPCChasePlayer
            intruderNPC.nPCChasePlayer.cameraZoneSwitcherr = cameraZoneSwitcherr;
            intruderNPC.nPCChasePlayer.player = player;
            intruderNPC.nPCChasePlayer.objectParent = objectParent;
 


            int randomChance = Random.Range(0,10);
            if (randomChance >= IntruderSpawnChance)
            {
                if (gameTime >= 0)
                {
                    instantiateIntruderList.Add(Instantiate(prefabData, virtualParent));
                    instantiateIntruderTerrainList.Add(Instantiate(intruderNPC, terrainParent));
                    // instantiateIntruderLeftTerrainList.Add(Instantiate(prefabData, terrainParent));
                    // instantiateIntruderRightTerrainList.Add(Instantiate(prefabData, terrainParent));
                }

            }
            else if (randomChance < IntruderSpawnChance)
                {
                    // Debug.Log("INTRUDER SPAWN NEGATED");
                } 


            //mengirim value spawnPosition dari playManager kepada Intruder
            //nilai yang sebenarnya adalah yang berasal dari hasil cek instantiateIntruderList terakhir
            for (int i = 0; i < instantiateIntruderList.Count; i++)
            {
                int randomInstantiate = Random.Range(0,intruderVirtualPositionList.Count - 1);
                instantiateIntruderList[i].spawnPosition = intruderVirtualPositionList[randomInstantiate];
                instantiateIntruderTerrainList[i].spawnPosition = intruderTerrainPositionList[randomInstantiate];

                // Debug.Log("INTRUDER POSITION LIST [RANDOM INSTANTIATE] IS " + intruderVirtualPositionList[randomInstantiate]);

                int randomIntruderChance = Random.Range(0,10);
                instantiateIntruderList[i].moveChance = randomIntruderChance;
                instantiateIntruderTerrainList[i].moveChance = randomIntruderChance;

                // Debug.Log("INTRUDER CHANCE [RANDOM INSTANTIATE] IS " + intruderVirtualPositionList[randomInstantiate]);

                //detik spawn warning object ( <= 0.5 * one hour)
                int randomActiveTime = Random.Range(0,5);
                instantiateIntruderList[i].intruderActiveTime = randomActiveTime;
                instantiateIntruderTerrainList[i].intruderActiveTime = randomActiveTime;

                //menghitung interval waktu intruder aktif
                int randomIntervalTime = Random.Range(3,5);
                instantiateIntruderList[i].intruderIntervalTime = randomIntervalTime;
                instantiateIntruderTerrainList[i].intruderIntervalTime = randomIntervalTime;

                // menjadikan setiap spawn posisi LPA (A,B,C,D) menjadi satu area (LPA)
                // agar spawn intruderType yang sama tidak bisa spawn di area yang sama
                for (int l = 0; l < intruderAreaLPA.Count; l++)
                {
                    if (intruderVirtualPositionList[randomInstantiate] == intruderAreaLPA[l])
                    {
                        // Debug.Log("AREA LPA");
                        instantiateIntruderList[i].spawnArea = "LPA";
                    }
                    else if(intruderVirtualPositionList[randomInstantiate] == intruderAreaRPA[l])
                    {
                        //  Debug.Log("AREA RPA");
                        instantiateIntruderList[i].spawnArea = "RPA";
                    }
                    else if(intruderVirtualPositionList[randomInstantiate] == intruderAreaFPA[l])
                    {
                        //  Debug.Log("AREA FPA");
                        instantiateIntruderList[i].spawnArea = "FPA";
                    }
                    else if(intruderVirtualPositionList[randomInstantiate] == intruderAreaLCA[l])
                    {
                        //  Debug.Log("AREA LCA");
                        instantiateIntruderList[i].spawnArea = "LCA";
                    }
                    else if(intruderVirtualPositionList[randomInstantiate] == intruderAreaRCA[l])
                    {
                        //  Debug.Log("AREA RCA");
                        instantiateIntruderList[i].spawnArea = "RCA";
                    }
                    else if(intruderVirtualPositionList[randomInstantiate] == intruderAreaRRA[l])
                    {
                        //  Debug.Log("AREA RRA");
                        instantiateIntruderList[i].spawnArea = "RRA";
                    }else if(intruderVirtualPositionList[randomInstantiate] == intruderAreaLRA[l])
                    {
                        //  Debug.Log("AREA LRA");
                        instantiateIntruderList[i].spawnArea = "LRA";
                    }
                }
                // Debug.Log("INSTANTIATE LIST [I] SPAWN AREA IS " + instantiateIntruderList[i].spawnArea );
                

                for (int j = 0; j < SpawnPositionList.Count; j++)
                {
                
                    //objek aktif yang lebih awal spawnlah yang akan mendapatkan location, sedangkan yang terbaru akan di deactive
                    if (instantiateIntruderList[i].gameObject.activeInHierarchy == true 
                    && instantiateIntruderList[i].spawnPosition == SpawnPositionList[j])
                    {
                        // Debug.Log("INTRUDER SPAWN/MOVE IN THE SAME PLACE, SPAWN/MOVE DECLINED");
                        // Debug.Log("INTRUDER IS ACTIVE" + instantiateIntruderList[i].gameObject);
                        // Debug.Log("INTRUDER SPAWN POSITION [i] IS: " + instantiateIntruderList[i].spawnPosition);
                        // Debug.Log("SPAWN POSITION LIST [j] IS: " + SpawnPositionList[j]);
                        instantiateIntruderList[i].spawnPosition = null;
                        instantiateIntruderTerrainList[i].spawnPosition = null;
                        
                    }

                    for (int m = 0; m < instantiateIntruderList.Count; m++)
                    {
                        //untuk mengecek data yang sama maka perlu [i] dan [m]
                        //areaPositionList.count == SpawnPositionList.Count
                        if (i != m
                        && instantiateIntruderList[i].intruderType == instantiateIntruderList[m].intruderType
                        && instantiateIntruderList[i].gameObject.activeInHierarchy == true
                        && instantiateIntruderList[m].gameObject.activeInHierarchy == true 
                        && instantiateIntruderList[i].spawnArea == AreaPositionList[j] 
                            )
                        {
                            // Debug.Log("INTRUDER WITH SAME TYPE SPAWN/MOVE IN THE SAME AREA, SPAWN/MOVE DECLINED");
                            // Debug.Log(string.Join("," , instantiateIntruderList));
                            // Debug.Log("INSTANTIATE LIST INTRUDER TYPE " + i + instantiateIntruderList[i].intruderType );
                            // Debug.Log("INTRUDER TYPE IN INSTANTIATE LIST " + m + instantiateIntruderList[m].intruderType );
                            // Debug.Log("INSTANTIATE LIST SPAWN AREA IS " + i + instantiateIntruderList[i].spawnArea );
                            // Debug.Log("AREA POSITION LIST IS " + j + AreaPositionList[j] );
                            // Debug.Log("INSTANTIATE LIST [i] IS " + i + instantiateIntruderList[i].intruderType );
                            instantiateIntruderList[i].spawnPosition = null;
                            instantiateIntruderTerrainList[i].spawnPosition = null;
                            
                        }
                    }
                            
                }
                    
                    
                // Debug.Log("DATA PASS CHECK");
                SpawnPositionList.Add(intruderVirtualPositionList[randomInstantiate]);
                AreaPositionList.Add(instantiateIntruderList[i].spawnArea);
                
            } 
        
    }


     public void InstantiateWarningObject()
    {
            
            int rand = Random.Range(0,warningObjectPrefabList.Count - 1);
            var prefabDataVirtual = warningObjectDataList[rand];
            var prefabDataTerrain = warningObjectDataList[rand];

            int randomVirtual = Random.Range(0,warningObjectVirtualPositionList.Count - 1);
            prefabDataVirtual.spawnPosition = warningObjectVirtualPositionList[randomVirtual];
            prefabDataTerrain.spawnPosition = warningObjectVirtualPositionList[randomVirtual];

            //detik spawn warning object ( <= 0.5 * one hour)
            int randomActiveTime = Random.Range(0,5);
            prefabDataVirtual.warningObjectActiveTime = randomActiveTime;
            prefabDataTerrain.warningObjectActiveTime = randomActiveTime;

            //playManager.gametime = intruder.gametime
            prefabDataVirtual.gameTime = gameTime;
            prefabDataTerrain.gameTime = gameTime;



            int randomChance = Random.Range(0,10);
            if (randomChance >= warningObjectSpawnChance)
            {
                if (gameTime >= 0)
                {
                    Instantiate(prefabDataVirtual, virtualParent);
                    Instantiate(prefabDataTerrain, terrainParent);
                    // Debug.Log("WARNING OBJECT VIRTUAL POSITION LIST COUNT IS " + warningObjectVirtualPositionList.Count);
                    // Debug.Log("WARNING OBJECT TERRAIN POSITION LIST COUNT IS " + warningObjectTerrainPositionList.Count);
                    warningObjectVirtualPositionList.RemoveAt(randomVirtual);
                    warningObjectTerrainPositionList.RemoveAt(randomVirtual);
                    
                    maxCharisma++;
                }

            }
            else if (randomChance < warningObjectSpawnChance)
                {
                    // Debug.Log("WARNING OBJECT SPAWN NEGATED");
                }  
    }

     public void InstantiateEvidenceItem()
    {
            
            int rand = Random.Range(0, evidenceItemPrefabList.Count - 1);
            var prefabDataTerrain = evidenceItemDataList[rand];

            int randomTerrain = Random.Range(0, evidenceItemTerrainPositionList.Count - 1);
            prefabDataTerrain.spawnPosition = evidenceItemTerrainPositionList[randomTerrain];

            //detik spawn warning object ( <= 0.5 * one hour)
            int randomActiveTime = Random.Range(0,5);
            prefabDataTerrain.evidenceItemActiveTime = randomActiveTime;

            //playManager.gametime = intruder.gametime
            prefabDataTerrain.gameTime = gameTime;

            int randomChance = Random.Range(0,10);
            if (randomChance >= evidenceItemSpawnChance)
            {
                if (gameTime >= 0)
                {
                    Instantiate(prefabDataTerrain, terrainParent);
                    evidenceItemTerrainPositionList.RemoveAt(randomTerrain);

                }

            }
            else if (randomChance < evidenceItemSpawnChance)
                {
                    // Debug.Log("EVIDENCE ITEM SPAWN NEGATED");
                }  
    }
}
    
