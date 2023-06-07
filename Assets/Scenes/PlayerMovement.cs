using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float speed = 1;
    [SerializeField] float rotateSpeed = 90;

    float horizontalAcc;
    float verticalAcc;

    void Update()
    {
       var horizontal = Input.GetAxis("Horizontal");
       var vertical = Input.GetAxis("Vertical");
       var isMove = horizontal != 0 || vertical!=0;

       transform.Translate(Vector3.forward * speed * vertical * Time.deltaTime);
       transform.Rotate(Vector3.up * rotateSpeed * horizontal * Time.deltaTime);

        if(horizontal > 0)
            horizontalAcc += Time.deltaTime;
        else if(horizontal < 0)
            horizontalAcc -= Time.deltaTime;
        else
            horizontalAcc = Mathf.Lerp(horizontalAcc,0,2*Time.deltaTime);

         if(vertical > 0)
            verticalAcc += Time.deltaTime;
        else if(vertical < 0)
            verticalAcc -= Time.deltaTime;
        else
            verticalAcc = Mathf.Lerp(verticalAcc,0,2*Time.deltaTime);

        horizontalAcc = Mathf.Clamp(horizontalAcc,-1,1);
        verticalAcc = Mathf.Clamp(verticalAcc,-1,1);

        animator.SetFloat("Horizontal", horizontalAcc);
        animator.SetFloat("Vertical", verticalAcc);
        animator.SetBool("isMoving", isMove);
        

    }
}
