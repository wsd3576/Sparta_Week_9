using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private static BulletManager instance;
    public static BulletManager Instance { get => instance; }

    [SerializeField] private GameObject[] bulletPrefabs;

    [SerializeField] private ParticleSystem impactParticleSystem;
    
    ObjectPoolManager objectPoolManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        objectPoolManager = ObjectPoolManager.Instance;
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction)
    {
        // GameObject origin = bulletPrefabs[rangeWeaponHandler.BulletIndex];
        // GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);
        GameObject obj = objectPoolManager.GetObject(rangeWeaponHandler.BulletIndex, startPosition, Quaternion.identity);

        BulletController bulletController = obj.GetComponent<BulletController>();
        bulletController.Init(direction, rangeWeaponHandler, this);
    }

    public void CreateImpactParticlesAtPosition(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        impactParticleSystem.transform.position = position;
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));

        ParticleSystem.MainModule mainModule = impactParticleSystem.main;
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;
        impactParticleSystem.Play();
    }
}
