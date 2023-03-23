using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public float naturalHealth;
    private float _health;
    
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
            {
                KillEntity();
            }
        }
    }

    private void Start()
    {
        _health = naturalHealth;
    }

    public void KillEntity()
    {
        
    }
}
