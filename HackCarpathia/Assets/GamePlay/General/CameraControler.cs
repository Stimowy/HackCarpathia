using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    // dodać skrypt CameraControl do głównej kamery
    // utworzyć tag gameCamera, dodać go do kamer(nie dodawać do kamer odpowiedzialnych za UI)
    // każda kamera ma mieć włączone "Priority" ustawione na 0
    // priorytet kamer innych niż otoczenie nie powinny być większy niż 20
    public static CameraControler Instance;
    GameObject[] Cameras;
    public string activeCamera;

    void Awake()
    {
        Instance = this;
        Cameras = GameObject.FindGameObjectsWithTag("gameCamera");
    }

    // funkcje wywołuje się globalnie używając CameraControler.Instance.setCamera()
    public void setCamera(string CameraName)
    {
        if (CameraName == activeCamera)
        {
            return;
        }
        activeCamera = CameraName;

        foreach (var camera in Cameras)
        {
            CinemachineCamera cameraComponent = camera.GetComponent<CinemachineCamera>();
            cameraComponent.Priority = 0;
            if (camera.name == CameraName)
            {
                cameraComponent.Priority = 20;
            }
        }
    }
}
