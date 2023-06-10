using UnityEngine;

public class NPCChasePlayer : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 5f;
    public float viewRadius = 10f;
    public float enlargedViewRadius = 15f; // Radius diperbesar saat pemain berada dalam area

    private Rigidbody rb;
    private bool isPlayerInArea = false; // Menandakan apakah pemain berada dalam area

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Periksa apakah objek pemain ada sebelum melanjutkan
        if (player == null)
            return;

        // Periksa apakah pemain berada dalam area pandang
        if (Vector3.Distance(transform.position, player.transform.position) <= viewRadius)
        {
            if (!isPlayerInArea)
            {
                // Pemain memasuki area, perbesar radius
                viewRadius = enlargedViewRadius;
                isPlayerInArea = true;
            }

            // Menghitung arah menuju pemain
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0f; // Tetapkan komponen Y ke 0

            // Menggerakkan NPC ke arah pemain
            rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        }
        else if (isPlayerInArea)
        {
            // Pemain keluar dari area, kembalikan radius ke ukuran awal
            viewRadius = 10f;
            isPlayerInArea = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Menggambar area pandang dalam Scene View di Unity Editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
