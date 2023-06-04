using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoneSwitcherr : MonoBehaviour
{
    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera[] virtualCameras;
    public GameObject playerGameObject;
    public GameObject lightObject;

    private void Start()
    {
        SwitchToCamera(primaryCamera);
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
            playerTransform.position = new Vector3(647f, 0.37f, 116.3f);
            lightObject.SetActive(true);
        }
        if (other.CompareTag("BackPos1"))
        {
            SwitchToCamera(primaryCamera);

            // Mendapatkan referensi komponen Transform pada player
            Transform playerTransform = playerGameObject.transform;

            // Mengubah posisi player ke koordinat yang ditentukan
            playerTransform.position = new Vector3(251f, 6.8f, 106f);
            lightObject.SetActive(false);
        }
        if (other.CompareTag("GeneratorRoom1"))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera);

            // Mendapatkan referensi komponen Transform pada player
            Transform playerTransform = playerGameObject.transform;

            // Mengubah posisi player ke koordinat yang ditentukan
            playerTransform.position = new Vector3(1078.5f, 0.55f, 290f);
            lightObject.SetActive(true);
        }
        if (other.CompareTag("BackGeneratorRoom1"))
        {
            SwitchToCamera(primaryCamera);

            // Mendapatkan referensi komponen Transform pada player
            Transform playerTransform = playerGameObject.transform;

            // Mengubah posisi player ke koordinat yang ditentukan
            playerTransform.position = new Vector3(165.5f, 6.8f, 153.5f);
            lightObject.SetActive(false);
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

