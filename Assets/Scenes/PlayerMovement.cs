using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0,100)] float speed;
    void Update()
    {
       var movex = Input.GetAxisRaw("Horizontal");
       var movez = Input.GetAxisRaw("Vertical");
       
       transform.Translate(speed * movex * Time.deltaTime, 0 , speed * movez * Time.deltaTime); 

    }
}
