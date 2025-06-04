using Cinemachine;
using UnityEngine;

public class CameraOverrider : MonoBehaviour
{
    public int currentPriority = 5;
    public int activePriority = 20;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Reset()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        currentPriority = virtualCamera.Priority;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) virtualCamera.Priority = activePriority;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) virtualCamera.Priority = currentPriority;
    }
}