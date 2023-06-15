using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollider : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject player;
    [SerializeField] List <GameObject> jumpscare;
    [SerializeField] AudioSource jumpScare;

private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            for (int i = 0; i < jumpscare.Count; i++)
        {
            jumpscare[i].SetActive(true);
        }
            player.SetActive(false);
            // jumpScare.Play();
            Invoke("GameOver", 2);

            

            
     
        }
    }

    public void GameOver()
    {
        
         gameOverPanel.SetActive(true); 
    }
}
