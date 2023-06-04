using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Pemain (player) yang akan diikuti oleh kamera
    public float smoothSpeed = 0.125f; // Kecepatan pergerakan kamera. Sesuaikan sesuai kebutuhan.

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x, transform.position.y , (target.position.z-8));
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
