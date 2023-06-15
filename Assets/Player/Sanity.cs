using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour
{
    [SerializeField] List <Light> sanityEffect;
    [SerializeField] Power power;
    public float sanity = 100f;
    public float penurunanSanity = 3f;
    public float penambahanSanity = 10f;
    public Slider sanitySlider;
    public GameObject gameOverSanityPanel;

    public float totalSanity;
    bool isOn = true;
    public float intensityOperator;
    public float LightIntensity;

    private void Start()
    {
        totalSanity = sanity;
    }

    private void Update()
    {
        if (isOn)
        {
            if (!GetComponent<Senter>().IsSenterOn()) // Memeriksa apakah senter menyala
            {
                totalSanity = Mathf.Clamp(totalSanity - penurunanSanity * Time.deltaTime, 0f, sanity);
                intensityOperator += 3;
            }
        }

        LightIntensity = intensityOperator * 0.00015f;


        for (int i = 0; i < sanityEffect.Count; i++)
        {

            sanityEffect[i].intensity = LightIntensity;
            
        }

        if (totalSanity <= 0)
        {
            gameOverSanityPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PostLamp"))
        {
            if (totalSanity <= 100f && power.blackoutActive == false)
            {
                totalSanity = Mathf.Clamp(totalSanity + penambahanSanity * Time.deltaTime, 0f, sanity);
                isOn = false;
                intensityOperator -= 10;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PostLamp"))
        {
            isOn = true;
        }
    }

    private void FixedUpdate()
    {
        sanitySlider.value = totalSanity / sanity;
    }
}
