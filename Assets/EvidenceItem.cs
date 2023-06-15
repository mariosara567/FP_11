using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceItem : MonoBehaviour
{

    //initialTimer adalah waktu dalam one hour gametime (3 menit realtime)
    [SerializeField] float initialTimer = 180;

    public GameObject ObjectChild;
    public GameObject spawnPosition;
    public string evidenceItemName;


    // waktu interval perpindahan jam
    float timer;
    // waktu perpindahan jam (00:00 - 05:00)
    public int gameTime; 
    
   
    // kapan intruder terspawn di jam spawn/move miliknya
    public float evidenceItemActiveTime;


    private void Start()
    {
        timer = initialTimer;
        //if (gameTime >= 0) agar spawnPosition() tidak terpanggil setelah jam terakhir
        if (gameTime >= 0)
                {
                    // Debug.Log("EVIDENCE ITEM SPAWN POSITION IS: " + spawnPosition);
                    SpawnPosition();
                }
    }

    private void Update()
    {
            //Deactive (one hour - intruderActiveTime)
            float timeSpawn = 180 - evidenceItemActiveTime;
            if (timer <= timeSpawn)
            {
                //Active in virtual and Terrain
                ObjectChild.SetActive(true);
            }

            timer -=Time.deltaTime;   
    }


    private void SpawnPosition()
    {   
            var x = spawnPosition.transform.localPosition.x;
            var z = spawnPosition.transform.localPosition.z;   

            this.transform.localPosition = new Vector3(x,2,z);
                
    }

}
