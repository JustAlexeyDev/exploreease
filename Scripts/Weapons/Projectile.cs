using UnityEngine;
using UnityEngine.Events;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        public float liveTime;
        public UnityEvent onHit;
        public ParticleSystem ParticleSystem;

        private void Awake()
        {
            Invoke(nameof(EndLive), liveTime);
        }

        private void EndLive()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Debug.Log(
                collider.gameObject.tag);
            if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("SpawnBlock") || collider.gameObject.CompareTag("Projectile")) return;
            onHit.Invoke();
            if (gameObject.TryGetComponent(out Rigidbody2D rb))
            {
                var particle = Instantiate(ParticleSystem);
                particle.transform.position = transform.position;
                particle.Play();
                Destroy(gameObject);
            }
        }
    }
}
