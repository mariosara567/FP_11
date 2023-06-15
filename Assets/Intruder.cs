using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intruder : MonoBehaviour
{
    // [SerializeField] Material intruderType;
    // [SerializeField] int intruderTime;
    [SerializeField] List <GameObject> intruderLocationList;
    [SerializeField] float initialTimer = 20;
    [SerializeField] public string intruderType;
    [SerializeField] public NPCChasePlayer nPCChasePlayer;
    public List <GameObject> intruderLocation = new List<GameObject>();
    public List <int> intruderTime = new List<int>();
    public GameObject objectChild;

    // posisi spawn intruder dari play manager
    public GameObject spawnPosition;
    // waktu interval perpindahan jam
    public float timer;
    // waktu perpindahan jam (00:00 - 05:00)
    public int gameTime; 
    // peluang intruder move
    public int moveChance;
    // menentukan kapan game berakhir dan move tidak akan terpanggil setelahnya
    bool timerActive = true;
    // kapan intruder spawn
    public float intruderActiveTime;
    // berapa lama intruder aktif
    public float intruderIntervalTime;
    // menentukan spawn area intruder
    public string spawnArea;
    // saat spawnPositin = null, fungsi intruderactivetime tidak berjalan
    bool intruderActiveTimeActive = true;





    private void Start()
    {
        if (spawnPosition == null)
        {
            // Debug.Log("INTRUDERTYPE IS: " + intruderType);
            // Debug.Log("SPAWN POSITION IN NEGATED IS: " + spawnPosition);
            //negate saat tidak memiliki spawn position
            intruderActiveTimeActive = false;
            this.gameObject.SetActive(false);
        }
        else if (spawnPosition != null)
        {
            timer = initialTimer;
            intruderTime.Add(gameTime);
            // Debug.Log(string.Join("," , intruderTime));

            if (gameTime >= 0)
            {
                // Debug.Log("INTRUDER SPAWN POSITION IS: " + spawnPosition);
                SpawnPosition();

            }
                
        }
 
        gameTime--;
        // Debug.Log("SPAWN CHANCE IS: " + rand);
    }

    private void Update()
    {
        // ketika ada intruder lain dengan spawnPosition yang sama, maka yang lebih awal terspawn/move akan menghilang
        if (spawnPosition == null)
        {   
            // Debug.Log("INTRUDERTYPE IS: " + intruderType);
            // Debug.Log("MOVE POSITION IN NEGATED IS: " + spawnPosition);
            //negate saat tidak mendapatkan spawn positon yang baru di jam berikutnya
            intruderActiveTimeActive = false;
            this.gameObject.SetActive(false);
        }
        else if (spawnPosition != null )
        {
            if(timer <= 0 && timerActive == true)
            {
                if (moveChance >= 5)
                {
                    //move tidak akan terjadi setelah gametime < 0
                    if (gameTime >= 0)
                    {
                        // Debug.Log("INTRUDER MOVE POSITION IS: " + spawnPosition);
                        intruderTime.Add(gameTime);
                        SpawnPosition();
                        
                    }
                    else if (gameTime < 0)
                    {
                        timerActive = false;
                        // Debug.Log("GAME OVER");  
                    }
                    // Debug.Log(string.Join("," , intruderTime));
            
                    timer = 20;
                    gameTime--;      
                }
                else if (moveChance < 5)
                {
                    // Debug.Log("MOVE NEGATED");
                    intruderActiveTimeActive = false;
                    this.gameObject.SetActive(false);
                }
            }
            

            if (intruderActiveTimeActive == true)
            {
                //menghitung berapa lama intruder aktif
                float timeSpawn = 20 - intruderActiveTime;
                if (timer <= timeSpawn)
                {
                    //Active: CCTV and Chase()
                    objectChild.SetActive(true);
                    if (intruderIntervalTime <= 0)
                        {
                            objectChild.SetActive(false);
                        }
                    intruderIntervalTime -= Time.deltaTime;
                }
            }
            

            timer -=Time.deltaTime;
        }
    }


    private void SpawnPosition()
    {

        var x = spawnPosition.transform.localPosition.x;
        var z = spawnPosition.transform.localPosition.z;

        intruderLocation.Add(spawnPosition);        

        this.transform.localPosition = new Vector3(x,2,z);
      
    }

}
