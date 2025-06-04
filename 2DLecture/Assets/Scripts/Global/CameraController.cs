using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBasicMultiChannelPerlin perlin;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private float shakeTimeRemaining;

    private void Reset()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            if (shakeTimeRemaining <= 0) StopShaking();
        }
    }


    public void ShakeCamera(float duration, float amplitude, float frequency)
    {
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