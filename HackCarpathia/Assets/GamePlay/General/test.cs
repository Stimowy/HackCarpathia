using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CameraControler.Instance.setCamera("Kamera1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CameraControler.Instance.setCamera("Kamera2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CameraControler.Instance.setCamera("playerCamera");
        }
    }
}
