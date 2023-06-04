using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Senter : MonoBehaviour
{
    public float energiSenter = 100f;
    public float penurunanEnergiSenter = 10f;
    public float penambahanEnergiSenter = 5f;
    public Slider energiSenterSlider;

    public GameObject targetObject;

    bool isOn = false;

    public float totalEnergiSenter;

    private void Start()
    {
        totalEnergiSenter = energiSenter;
        targetObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
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
            if (totalEnergiSenter <= 100f && Input.GetMouseButton(0))
            {
                totalEnergiSenter = Mathf.Clamp(totalEnergiSenter + penambahanEnergiSenter * Time.deltaTime, 0f, energiSenter);
                isOn = false;
                targetObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        energiSenterSlider.value = totalEnergiSenter / energiSenter;
    }
}
