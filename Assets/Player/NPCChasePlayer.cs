using UnityEngine;

public class NPCChasePlayer : MonoBehaviour
{
    [SerializeField] public Intruder intruder;
    [SerializeField] public CameraZoneSwitcherr cameraZoneSwitcherr;
    public Transform objectParent;
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
                cameraZoneSwitcherr.chaseEvent = true;

                this.transform.parent = objectParent;
                intruder.gameObject.SetActive(false);
                cameraZoneSwitcherr.nPCChaseList.Add(this);
                // PlayManager.maxcharisma++
                
                // Debug.Log("PLAYER SEEN BY INTRUDER");
            }

            // Menghitung arah menuju pemain
            Vector3 direction = (player.transform.position - transform.position).normalized;

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


    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("IntruderWayOut"))
        {
            cameraZoneSwitcherr.nPCChaseList.RemoveAt(0);
           this.gameObject.SetActive(false);
        }

        //saat intruder menyentuh pintu (akan mengirim pesan NPCInPosition ke CameraZoneSwitcher), intruder akan berpindah ke dalam pos dan akan mengejar player
        if(other.CompareTag("NPCBait"))
        {
           for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
            {   
                cameraZoneSwitcherr.nPCChaseList[i].player = cameraZoneSwitcherr.playerGameObject;
            }
           cameraZoneSwitcherr.NPCInPosition = true;
        }

        if(other.CompareTag("NPCPostMediumToRight"))
        {
           for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
            {   
                cameraZoneSwitcherr.nPCChaseList[i].player = cameraZoneSwitcherr.NPCRightPostBait;
            }
            
            cameraZoneSwitcherr.intruderChaseLeftPost = false;
            cameraZoneSwitcherr.intruderChaseRightPost = true;
        }

        if(other.CompareTag("NPCPostMediumToLeft"))
        {
           for (int i = 0; i < cameraZoneSwitcherr.nPCChaseList.Count; i++)
            {   
                cameraZoneSwitcherr.nPCChaseList[i].player = cameraZoneSwitcherr.NPCLeftPostBait;
            }

            cameraZoneSwitcherr.intruderChaseLeftPost = true;
            cameraZoneSwitcherr.intruderChaseRightPost = false;
            
        }
        
        

    }

    
}
