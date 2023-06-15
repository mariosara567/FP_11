using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] NoteBookText noteBookText;
    [SerializeField] PlayManager playManager;
    [SerializeField] EndGameResult endGameResult;
    public int submitCount;
    public int positionInSubmitList;
    public bool charismaPointStatus;




    public void DeleteText()
    {
        Debug.Log("POSITION IN SUBMIT LIST IN DELETE BUTTON IS: " + positionInSubmitList);
        Debug.Log("SUBMIT TEXT LIST: " + noteBookText.submitTextList.Count );
        if (positionInSubmitList == submitCount )
        {
            Debug.Log("INSTANTIATE TEXT COUNT IS: " + noteBookText.submitTextList.Count);
            Destroy(noteBookText.submitTextList[positionInSubmitList - 1].gameObject);
            this.gameObject.SetActive(false);
            noteBookText.submitTextList.RemoveAt(positionInSubmitList - 1);
            noteBookText.submitTextListData.RemoveAt(positionInSubmitList - 1);
            noteBookText.submitCount--;
            submitCount--;

            Debug.Log("CHARISMA POINT STATUS IN DELETE BUTTON IS: " + charismaPointStatus);
            //charismaPoint return
            if (charismaPointStatus == true)
            {
                playManager.charismaPoint--;
                endGameResult.valueIRA--;
            }
            else if (charismaPointStatus == false)
            {
                playManager.charismaPoint++;
                endGameResult.valueIRD--;
            }
            noteBookText.deleteButtonPositionInList--;
            Debug.Log("CHARISMA POINT AFTER DELETE BUTTON IS: " + playManager.charismaPoint);

            //menyamakan submitCount pada semua deleteButton
            for (int i = 0; i < submitCount; i++)
            {
               noteBookText.deleteButtonData[i].submitCount = submitCount;
            }
            Debug.Log("SUBMIT COUNT IN DELETE BUTTON AFTER TEXT ERASED IS: " + submitCount);
            Debug.Log("INSTANTIATE TEXT COUNT IS: " + noteBookText.submitTextList.Count);

            //charisma point
            
        }
        else if (positionInSubmitList < submitCount)
        {
            
            for (int i = positionInSubmitList ; i <= submitCount - 1; i++)
            {
                Debug.Log("DATA CHECKING " + i );
                Debug.Log("CHARISMA POINT STATUS IS: " + charismaPointStatus );
                noteBookText.submitTextList[i].transform.localPosition = new Vector3(noteBookText.submitTextList[i].transform.localPosition.x,
                noteBookText.submitTextList[i].transform.localPosition.y + 50,
                noteBookText.submitTextList[i].transform.localPosition.z);
                
                
                Debug.Log("CHARISMA POINT STATUS IS: " + charismaPointStatus );
                
            }
            Debug.Log("INSTANTIATE TEXT COUNT IS: " + noteBookText.submitTextList.Count);
            Destroy(noteBookText.submitTextList[positionInSubmitList - 1].gameObject);
            Debug.Log("POSITION IN SUBMIT LIST IN DELETE BUTTON IS: " + positionInSubmitList);
            noteBookText.submitTextList.RemoveAt(positionInSubmitList - 1 );
            noteBookText.submitTextListData.RemoveAt(positionInSubmitList - 1);

            //charismaPoint return
            if (charismaPointStatus == true)
            {
                playManager.charismaPoint--;
                endGameResult.valueIRA--;
            }
            else if (charismaPointStatus == false)
            {
                playManager.charismaPoint++;
                endGameResult.valueIRD--;
            }
            noteBookText.deleteButtonPositionInList--;
            Debug.Log("CHARISMA POINT AFTER DELETE BUTTON IS: " + playManager.charismaPoint);

            //update value charismaPointStatus (dipanggil setelah charismaPoint return)
            for (int i = positionInSubmitList ; i <= submitCount - 1; i++)
            {
            noteBookText.deleteButtonData[i - 1].charismaPointStatus = noteBookText.deleteButtonData[i].charismaPointStatus;
            }
            noteBookText.deleteButton[submitCount-1].SetActive(false);
            
            //submit count berkurang setelah update value charismaPointStatus
            noteBookText.submitCount--;
            submitCount--;

            //menyamakan submitCount pada semua deleteButton
            for (int i = 0; i < submitCount; i++)
            {
               noteBookText.deleteButtonData[i].submitCount = submitCount;
            }
            Debug.Log("SUBMIT COUNT IN DELETE BUTTON AFTER TEXT ERASED IS: " + submitCount);
            Debug.Log("INSTANTIATE TEXT COUNT IS: " + noteBookText.submitTextList.Count);

            

            
            
            
        }
    }
}
