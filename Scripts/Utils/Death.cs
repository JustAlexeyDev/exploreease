using UnityEngine;

namespace Utils
{
    public class Death : MonoBehaviour
    {
        public float liveTime;
        private void Awake()
        {
            Invoke(nameof(DeathMethod), liveTime);
        }

        private void DeathMethod()
        {
            Destroy(gameObject);
        }
    }
}
