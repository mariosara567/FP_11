using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitedText : MonoBehaviour
{
    [SerializeField]public TMP_Text submitText;

    string textRecorded;
    public int submitCount;
    int positionInSubmitList;

    //hanya perlu dipanggil di start, tidak akan terupdate oleh instantiate berikutnya
   private void Start()
   {
        // Debug.Log("DATA RECORDED");
        positionInSubmitList = submitCount;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x,
            this.transform.localPosition.y - (20 * submitCount),
            this.transform.localPosition.z);

        // Debug.Log("SUBMIT COUNT: " + submitCount);
   }
}
