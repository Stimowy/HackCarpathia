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
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            CameraControler.Instance.setCamera("Kamera1");
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            CameraControler.Instance.setCamera("Kamera2");
        }
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    CameraControler.Instance.setCamera("kameraTrzy");
        //}
    }
}
