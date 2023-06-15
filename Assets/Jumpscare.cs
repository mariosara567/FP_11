using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] GameObject head;
    public float timerL;
    public float timerR;
    public float timerActive;
    void Update()
    {
        if (timerR <= 0)
            {
            head.gameObject.transform.Rotate(0,-10,0);

               
               if (timerL < 1)
               {

                
                 timerR = 1.1f;
                 timerL = 0;
               }
               timerL -=Time.deltaTime; 
               
            }

        if (timerL <= 0)
            {

                head.gameObject.transform.Rotate(0,10,0);
                
                if (timerR < 1)
                {
                    timerL = 1.1f;
                    timerR = 0;
                } 
                timerR -= Time.deltaTime;  
            }

            if (timerActive <= 0)
            {
                this.gameObject.SetActive(false);
            }
            timerActive -=  Time.deltaTime;
        
    }
}
