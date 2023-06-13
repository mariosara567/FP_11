using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Kecepatan gerakan objek
    public float moveSpeed = 5f;

    private Vector2 moveDirection;
    
    // Kecepatan tambahan ketika tombol Shift ditekan
    public float shiftSpeed = 10f;

    // Stamina maksimum
    private float maxStamina = 100f;

    // Stamina saat ini
    public float currentStamina;

    // Waktu regenerasi stamina
    private float regenTime = 3f;

    // Timer regenerasi stamina
    private float regenTimer;

    // UI untuk menampilkan stamina
    public Slider staminaBar;

    public float kesadaranSekarang;

    private float maxKesadaran = 100f;

    public Slider kesadaranBar;



    public float turnSpeed = 10f; // kecepatan putar player
    public float maxAngle = 20f; // sudut maksimum senter



    public float totalSpeed;

    public Rigidbody2D rb;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = 50f;   
        kesadaranSekarang = 100f;
    }

    void Update()
    {
         // Mendapatkan input horizontal
        float horizontal = Input.GetAxis("Horizontal");
        
        // Mendapatkan input vertical
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector2(horizontal, vertical).normalized;


        // putar player
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }
    void FixedUpdate()
    {
       

         // Menghitung kecepatan total
        totalSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            totalSpeed = shiftSpeed;
        }

        if (currentStamina < 0 )
        {
            totalSpeed = moveSpeed;
        }

        // Menggerakkan objek
        rb.velocity = moveDirection * totalSpeed;

        // Mengurangi stamina saat berlari
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            currentStamina -= (Time.deltaTime * 5 );
            regenTimer = 0f;
        }
        else
        {
            // Regenerasi stamina jika tidak berlari
            if (regenTimer >= regenTime)
            {
                currentStamina = Mathf.Clamp(currentStamina + Time.deltaTime, 0f, maxStamina);
            }
            else
            {
                regenTimer += Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.R)){
            kesadaranSekarang += Time.deltaTime;
        }
        // Update UI stamina
        staminaBar.value = currentStamina / maxStamina;
        // Update UI stamina
        kesadaranBar.value = kesadaranSekarang / maxKesadaran;
    }
}
