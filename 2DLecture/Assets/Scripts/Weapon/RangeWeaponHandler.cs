using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform bulletSpawnPosition;
    
    [SerializeField] private GameObject bulletPrefab;
    private int bulletIndex;
    public int BulletIndex { get => bulletIndex; }

    [SerializeField] private float bulletSize = 1f;
    public float BulletSize { get => bulletSize; }

    [SerializeField] private float bulletLifeTime;
    public float BulletLifeTime { get => bulletLifeTime; }

    [SerializeField] private float spread;
    public float Spread { get => spread; }

    [SerializeField] private int bulletPerShot;
    public int BulletPerShot { get => bulletPerShot; }

    [SerializeField] private float multipleBulletAngle;
    public float MultipleBulletAngle { get => multipleBulletAngle; }

    [SerializeField] private Color bulletColor;
    public Color BulletColor { get => bulletColor; }

    private BulletManager bulletManager;
    
    private StatHandler statHandler;

    protected override void Start()
    {
        base.Start();
        bulletManager = BulletManager.Instance;
        statHandler = GetComponentInParent<StatHandler>();
        bulletIndex = bulletPrefab.GetComponent<IIndexable>().ObjectIndex;
    }
    public override void Attack()
    {
        base.Attack();

        float bulletAngleSpace = multipleBulletAngle;
        int numberOfbulletPerShot = bulletPerShot + (int)statHandler.GetStat(StatType.BulletCount);

        float minAngle = -(numberOfbulletPerShot / 2f) * bulletAngleSpace;

        for (int i = 0; i < numberOfbulletPerShot; i++)
        {
            float angle = minAngle + bulletAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateBullet(controllor.LookDirection, angle);
        }
    }

    private void CreateBullet(Vector2 _lookDirection, float angle)
    {
        bulletManager.ShootBullet(
            this,
            bulletSpawnPosition.position,
            RotateVector2(_lookDirection, angle)
            );
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}

