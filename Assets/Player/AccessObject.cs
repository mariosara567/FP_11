using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessObject : MonoBehaviour
{
    [SerializeField] PlayManager playManager;
    [SerializeField] MonitorButton monitorButton;
    [SerializeField] EndGameResult endGameResult;
    



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WarningObject"))
        {
             if(Input.GetKeyDown(KeyCode.F))
            {
                other.gameObject.SetActive(false);
                playManager.charismaPoint++;
                endGameResult.valueWOC++;

                for (int i = 0; i < playManager.warningObjectTerrainInstantiateList.Count; i++)
                {
                    if (playManager.warningObjectTerrainInstantiateList[i].gameObject.activeInHierarchy == false)
                    {
                        playManager.warningObjectVirtualInstantiateList[i].gameObject.SetActive(false);
                    }
                }

            }    
        }

        if (other.CompareTag("EvidenceItem"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                other.gameObject.SetActive(false);
                playManager.charismaPoint++;
                endGameResult.valueEIF++;
            }
        }

        if (other.CompareTag("CCTV 1"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                monitorButton.cameraError[0].SetActive(false);
                monitorButton.terrainCameraCollider[0].SetActive(false);
                monitorButton.terrainCameraLight[0].color = new Color(0, 255, 0);
            }
        }
        if (other.CompareTag("CCTV 2"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                monitorButton.cameraError[1].SetActive(false);
                monitorButton.terrainCameraCollider[1].SetActive(false);
                monitorButton.terrainCameraLight[1].color = new Color(0, 255, 0);
            }
        }
        if (other.CompareTag("CCTV 3"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                monitorButton.cameraError[2].SetActive(false);
                monitorButton.terrainCameraCollider[2].SetActive(false);
                monitorButton.terrainCameraLight[2].color = new Color(0, 255, 0);
            }
        }
        if (other.CompareTag("CCTV 4"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                monitorButton.cameraError[3].SetActive(false);
                monitorButton.terrainCameraCollider[3].SetActive(false);
                monitorButton.terrainCameraLight[3].color = new Color(0, 255, 0);
            }
        }
        if (other.CompareTag("CCTV 5"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                monitorButton.cameraError[4].SetActive(false);
                monitorButton.terrainCameraCollider[4].SetActive(false);
                monitorButton.terrainCameraLight[4].color = new Color(0, 255, 0);
            }
        }
        if (other.CompareTag("CCTV 6"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                monitorButton.cameraError[5].SetActive(false);
                monitorButton.terrainCameraCollider[5].SetActive(false);
                monitorButton.terrainCameraLight[5].color = new Color(0, 255, 0);
            }
        }
        if (other.CompareTag("CCTV 7"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                monitorButton.cameraError[6].SetActive(false);
                monitorButton.terrainCameraCollider[6].SetActive(false);
                monitorButton.terrainCameraLight[6].color = new Color(0, 255, 0);
            }
        }
    }
}

