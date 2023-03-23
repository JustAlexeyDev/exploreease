using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class ProjectileSpawner : MonoBehaviour
    {
        public float spread;
        public int count;
        public Projectile projectile;
        public float speed;

        public void Invoke(OnShootEventArgs args)
        {
            List<GameObject> instances = new List<GameObject>();
            var origin = args.weapon.shootLocation;
            Vector2 dir = CameraUtils.GetDirToCursor(UnityEngine.Camera.main, origin);
            var gap = spread * Mathf.Deg2Rad / 2;
            dir = dir.normalized;
            for (int i = 0; i < count; i++)
            {
                var bullet = Instantiate(this.projectile);
                instances.Add(bullet.gameObject);
                bullet.transform.position = origin.position;
                Rigidbody2D rigidbody;
                
                if (bullet.TryGetComponent(out Rigidbody2D rb))
                {
                    rigidbody = rb;
                }
                else
                {
                    rigidbody = bullet.AddComponent<Rigidbody2D>();
                }

                dir += new Vector2(Random.Range(-gap, gap), Random.Range(-gap, gap));
                
                rigidbody.AddForce(dir.normalized * speed);
            }
        }
    }
    
}