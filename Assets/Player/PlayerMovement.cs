using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float rotateSpeed = 90;
    public float jalan = 2.5f;
    public float lari = 4f;
    public float kekuatan = 100f;
    public float penurunanStamina = 10f;
    public float penambahanStamina = 10f;

    // Waktu regenerasi stamina
    private float regenTime = 1f;

    // Timer regenerasi stamina
    private float regenTimer;

    public Slider staminaSlider;
    public float totalStamina;
    private Rigidbody rb;
    private Quaternion targetRotation;

    private float horizontalAcc;
    private float verticalAcc;

    

    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        totalStamina = kekuatan;
        UpdateStaminaUI();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Menggerakkan kubus
        Vector3 perpindahan = new Vector3(horizontalInput, 0, verticalInput);
        rb.velocity = perpindahan * jalan;

        // Mengatur kecepatan berdasarkan input
        float kecepatan = Input.GetKey(KeyCode.LeftShift) && totalStamina > 0 ? lari : jalan; // Cek jika tombol Shift ditekan dan stamina masih tersedia, gunakan kecepatan lari
        rb.velocity = new Vector3(rb.velocity.x * kecepatan, rb.velocity.y, rb.velocity.z * kecepatan);

        // Mengurangi atau menambah stamina berdasarkan input
        if (Mathf.Abs(horizontalInput) >= 0 || Mathf.Abs(verticalInput) >= 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && totalStamina > 0)
            {
                totalStamina -= penurunanStamina * Time.fixedDeltaTime;
                regenTimer = 0f;
            }
            else
            {
                // Regenerasi stamina jika tidak berlari
                if (regenTimer >= regenTime)
                {
                    totalStamina = Mathf.Clamp(totalStamina + penambahanStamina * Time.deltaTime, 0f, kekuatan);
                }
                else
                {
                    regenTimer += Time.deltaTime;
                }
            }

            totalStamina = Mathf.Clamp(totalStamina, 0, kekuatan);
            UpdateStaminaUI();
        }

        // Mengembalikan kecepatan ke jalan jika stamina habis dan tombol Shift tetap ditekan
        if (totalStamina <= 0 && Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector3(rb.velocity.x / lari * jalan, rb.velocity.y, rb.velocity.z / lari * jalan);
        }

        // Rotasi kubus sesuai arah tujuannya
        if (perpindahan != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(perpindahan);
        }

        //fungsi animasi
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        if (horizontalInput > 0)
            horizontalAcc += Time.deltaTime;
        else if (horizontalInput < 0)
            horizontalAcc -= Time.deltaTime;
        else
            horizontalAcc = Mathf.Lerp(horizontalAcc, 0, 2 * Time.deltaTime);

        if (verticalInput > 0)
            verticalAcc += Time.deltaTime;
        else if (verticalInput < 0)
            verticalAcc -= Time.deltaTime;
        else
            verticalAcc = Mathf.Lerp(verticalAcc, 0, 2 * Time.deltaTime);

        horizontalAcc = Mathf.Clamp(horizontalAcc, -1, 1);
        verticalAcc = Mathf.Clamp(verticalAcc, -1, 1);

        animator.SetFloat("Horizontal", horizontalAcc);
        animator.SetFloat("Vertical", verticalAcc);
        animator.SetBool("isMoving", (horizontalInput != 0 || verticalInput != 0));
    }


    private void UpdateStaminaUI()
    {
        staminaSlider.value = totalStamina / kekuatan;
    }

    
}
