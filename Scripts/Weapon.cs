using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Weapons;

public class Weapon : MonoBehaviour
{
    // TODO
    [Header("Deprecated")]
    public ProjectileSpawner projectileSpawner;
    
    // Weapon stats
    [Header("Stats")]
    public float reloadTime, shootCooldown;
    public int magazineSize, storeAmmo;
    public bool autoShooting, isReloading;
    public Projectile defaultBullet;

    // Weapon state
    private int _currentAmmo;
    private float _currentCooldown;

    public float Cooldown
    {
        get => _currentCooldown;
        set => _currentCooldown = value;
    }
    private bool _allowFire = true;
    public bool AllowFire
    {
        get => _allowFire;
        set => _allowFire = value;
    }
    
    // References
    public Transform shootLocation;
    public OnShootEvent onShoot;
    public Transform pivotPoint;
    public UnityEvent<float> onReload;

    private void Start()
    {
        _currentAmmo = magazineSize;
    }

    public bool HasCooldown()
    {
        return _currentCooldown > 0;
    }

    private void Update()
    {
        _currentCooldown -= Time.deltaTime;
        if (isReloading)
        {
            onReload?.Invoke(_currentCooldown / reloadTime);
        }
    }

    public void Shoot()
    {
        if (_allowFire)
        {
            if (_currentAmmo > 0)
            {
                _currentCooldown = shootCooldown;
                _currentAmmo--;
                var args = new OnShootEventArgs();
                args.weapon = this;
                args.bulletType = defaultBullet;
                onShoot?.Invoke(args);
                projectileSpawner.Invoke(args);
            }
            else
            {
                Reload();
            }
        }
    }

    public void Reload()
    {
        isReloading = true;
        _currentCooldown = reloadTime;
        Invoke(nameof(ReloadEnd), reloadTime);
    }

    private void ReloadEnd()
    {
        isReloading = false;
        int need = magazineSize - _currentAmmo;
        int canTake = Math.Min(storeAmmo, need);
        _currentAmmo += canTake;
        storeAmmo -= canTake;
    }
}

[Serializable]
public class OnShootEvent : UnityEvent<OnShootEventArgs>
{
}

[Serializable]
public class OnShootEventArgs : EventArgs
{
    public Weapon weapon;
    public Projectile bulletType;
}
