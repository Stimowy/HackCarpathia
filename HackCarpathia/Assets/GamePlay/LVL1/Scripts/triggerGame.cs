using Unity.VectorGraphics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerGame : MonoBehaviour
{
    private bool isPlayerInTrigger = false;

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("miniGameLVL1");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        popupMessage.triggerMessage("naciśnij E, aby napisać egzamin");
        isPlayerInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        popupMessage.closeMessage();
        isPlayerInTrigger = false;
    }
}
