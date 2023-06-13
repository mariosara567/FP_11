using UnityEngine;

public class NPCChasePlayer : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 5f;
    public float viewRadius = 10f;
    public float enlargedViewRadius = 15f;

    private Animator animator;
    private Rigidbody rb;
    private bool isPlayerInArea = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (player == null)
            return;

        if (Vector3.Distance(transform.position, player.transform.position) <= viewRadius)
        {
            if (!isPlayerInArea)
            {
                viewRadius = enlargedViewRadius;
                isPlayerInArea = true;
            }

            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0f;

            rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            float horizontalAcc = Mathf.Clamp(direction.x, -1, 1);
            float verticalAcc = Mathf.Clamp(direction.z, -1, 1);

            animator.SetFloat("Horizontal", horizontalAcc);
            animator.SetFloat("Vertical", verticalAcc);
            animator.SetBool("isMoving", true);
        }
        else if (isPlayerInArea)
        {
            viewRadius = 10f;
            isPlayerInArea = false;
            animator.SetBool("isMoving", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
