using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollider : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject player;

private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            gameOverPanel.SetActive(true);
            player.SetActive(false);
     
        }
    }
}
