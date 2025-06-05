using System;
using UnityEngine;

public class BulletController : MonoBehaviour, IPoolable, IIndexable
{
    [SerializeField] private LayerMask levelCollisionLayer;
    
    private RangeWeaponHandler rangeWeaponHandler;

    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestroy = true;

    BulletManager bulletManager;
    
    [Range(0, 99)]
    [SerializeField] private int bulletIndex;
    public int ObjectIndex => bulletIndex;
    
    private Action<GameObject> returnToPool;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady) return;

        currentDuration += Time.deltaTime;
        
        if(currentDuration > rangeWeaponHandler.BulletLifeTime)
        {
            DestroyBullet(transform.position, false);
        }

        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyBullet(collision.ClosestPoint(transform.position) - direction * 0.2f, fxOnDestroy);
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if(resourceController != null)
            {
                resourceController.ChangeHealth(-rangeWeaponHandler.power);
                if (rangeWeaponHandler.IsOnKnockback)
                {
                    BaseControllor controllor = collision.GetComponent<BaseControllor>();
                    if(collision != null)
                    {
                        controllor.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }
            DestroyBullet(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    public void Init(Vector2 direction, RangeWeaponHandler rangeWeaponHandler, BulletManager bulletManager)
    {
        this.bulletManager = bulletManager;
        this.rangeWeaponHandler = rangeWeaponHandler;

        this.direction = direction;
        currentDuration = 0;
        transform.localScale = Vector3.one * rangeWeaponHandler.BulletSize;
        spriteRenderer.color = rangeWeaponHandler.BulletColor;

        transform.right = this.direction;

        if (direction.x < 0) pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    private void DestroyBullet(Vector3 position, bool createFx)
    {
        if (createFx) bulletManager.CreateImpactParticlesAtPosition(position, rangeWeaponHandler);

        OnDespawn();
    }

    public void Initialize(Action<GameObject> reaturnAction)
    {
        returnToPool = reaturnAction;
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {
        returnToPool?.Invoke(gameObject);
    }
}
