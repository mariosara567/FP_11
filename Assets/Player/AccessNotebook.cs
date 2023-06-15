using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessNotebook : MonoBehaviour
{
    [SerializeField] Power power;
    public GameObject noteBook;
    public GameObject monitor;
    public GameObject virtualCamera;
    public PlayerMovement playerMovement;
    public bool noteBookActive = false;
    public bool monitorActive = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monitor"))
        {
            if(Input.GetKeyDown(KeyCode.F) && monitorActive == false)
            {
                monitor.SetActive(true);
                virtualCamera.SetActive(true);
                monitorActive = true;
                playerMovement.jalan = 0;
                playerMovement.lari = 0;
                
            }
            else if(Input.GetKeyDown(KeyCode.F) && monitorActive == true)
            {
                monitor.SetActive(false);
                virtualCamera.SetActive(false);
                monitorActive = false;
                playerMovement.jalan = 2.5f;
                playerMovement.lari = 4;
                power.generatorSFX.Stop();
                
            }      
            
        }

        if (other.CompareTag("NoteBook"))
        {
            if(Input.GetKeyDown(KeyCode.F) && noteBookActive == false)
            {
                noteBook.SetActive(true);
                noteBookActive = true;
                playerMovement.jalan = 0;
                playerMovement.lari = 0;
                
            }
            else if(Input.GetKeyDown(KeyCode.F) && noteBookActive == true)
            {
                noteBook.SetActive(false);
                noteBookActive = false;
                playerMovement.jalan = 2.5f;
                playerMovement.lari = 4;
                
            }
        }
    }
}
