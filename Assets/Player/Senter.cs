using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Senter : MonoBehaviour
{
    public float energiSenter = 100f;
    public float penurunanEnergiSenter = 2f;
    public float penambahanEnergiSenter = 10f;
    public Slider energiSenterSlider;

    public GameObject targetObject;
    public GameObject chargerLamp;

    bool isOn = false;

    public float totalEnergiSenter;

    [SerializeField] AudioSource chargingSFX;
    bool inChargingPositionActive = false;

    private void Start()
    {
        totalEnergiSenter = energiSenter;
        targetObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isOn = !isOn;
            targetObject.SetActive(isOn);
        }

        if (isOn)
        {
            totalEnergiSenter -= penurunanEnergiSenter * Time.deltaTime;
            if (totalEnergiSenter <= 0)
            {
                totalEnergiSenter = 0;
                isOn = false;
                targetObject.SetActive(false);
            }
        }
        else if (isOn == false)
        {
            return;
        }
    }

    public bool IsSenterOn() // Metode untuk mendapatkan status senter
    {
        return isOn;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Charger"))
        {

                chargerLamp.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                totalEnergiSenter = Mathf.Clamp(totalEnergiSenter + penambahanEnergiSenter * Time.deltaTime, 0f, energiSenter);
                isOn = false;
                targetObject.SetActive(false);
                if (inChargingPositionActive == false)
                {
                    chargingSFX.Play();
                    inChargingPositionActive = true;
                }
                
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Charger"))
        {
                chargerLamp.GetComponent<Renderer>().material.SetColor("_Color", Color.red);  
                inChargingPositionActive = false;
        }
    }

    private void FixedUpdate()
    {
        energiSenterSlider.value = totalEnergiSenter / energiSenter;
    }
}
