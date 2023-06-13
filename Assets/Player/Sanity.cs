using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour
{
    public float sanity = 100f;
    public float penurunanSanity = 10f;
    public float penambahanSanity = 5f;
    public Slider sanitySlider;
    public GameObject gameOverSanityPanel;

    public float totalSanity;
    bool isOn = true;

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
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PostLamp"))
        {
            if (totalSanity <= 100f)
            {
                totalSanity = Mathf.Clamp(totalSanity + penambahanSanity * Time.deltaTime, 0f, sanity);
                isOn = false;
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
