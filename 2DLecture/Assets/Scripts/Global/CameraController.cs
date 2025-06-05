using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBasicMultiChannelPerlin perlin;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private bool isInit;
    private float shakeTimeRemaining;

    private void Awake()
    {
        if (!isInit) Init();
    }

    private void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            if (shakeTimeRemaining <= 0) StopShaking();
        }
    }

    private void Init()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        isInit = true;
    }


    public void ShakeCamera(float duration, float amplitude, float frequency)
    {
        if (!isInit) Init();

        if (shakeTimeRemaining > duration) return;

        shakeTimeRemaining = duration;

        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = frequency;
    }

    public void StopShaking()
    {
        shakeTimeRemaining = 0f;
        perlin.m_AmplitudeGain = 0f;
        perlin.m_FrequencyGain = 0f;
    }
}