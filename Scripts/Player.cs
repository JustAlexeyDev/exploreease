using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{
    public GameObject[] block;
    public float speed;
    private Rigidbody2D _physics;

    // Weapon system
    public Weapon[] inventory;
    private Weapon _selectedItem;
    private HandedEntity _handedEntity;

    void Start()
    {
        _physics = GetComponent<Rigidbody2D>();
        Instantiate(block[0], new Vector3(0, 0, 0), Quaternion.identity);

        _handedEntity = gameObject.GetComponent<HandedEntity>();

        foreach (var weapon in inventory)
        {
            _handedEntity.AttachItem(weapon);
        }

        _handedEntity.RaspolozitPredmeti();
    }

    void Update()
    {
        _physics.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        // Listen mouse click 
        if (_handedEntity.itemsInHands.Count > 0)
        {
            var firstGun = _handedEntity.itemsInHands[0];

            bool sendInput = firstGun.autoShooting ? Input.GetKey(KeyCode.Mouse0) : Input.GetKeyDown(KeyCode.Mouse0);

            if (sendInput && !firstGun.HasCooldown())
            {
                firstGun.Shoot();
            }
        }

        InputLogic();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    private void InputLogic()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _handedEntity.NextHand();
            _selectedItem = _handedEntity.GetItemInMainHand();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _handedEntity.PreviousHand();
            _selectedItem = _handedEntity.GetItemInMainHand();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_handedEntity.itemsInHands.Count > 0)
            {
                var item = _handedEntity.itemsInHands[0];
                if (item.TryGetComponent(out ThrowEntity throwEntity))
                {
                    throwEntity.Throw(CameraUtils.GetDirToCursor(UnityEngine.Camera.main, item.transform) * 50f);
                    throwEntity.transform.parent = transform.parent;
                    
                    _handedEntity.itemsInHands.Remove(item);
                    Destroy(item);
                }
            }
        }
    }

    public void Dash()
    {
        _physics.velocity = transform.forward * 10f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpawnBlock"))
        {
            var position = other.transform.position;

            Instantiate(block[Random.Range(0, 3)], new Vector3(position.x,
                position.y + 25f, 0), Quaternion.identity);
            Instantiate(block[Random.Range(0, 3)], new Vector3(position.x,
                position.y - 25f, 0), Quaternion.identity);
            Instantiate(block[Random.Range(0, 3)], new Vector3(position.x + 25f,
                position.y, 0), Quaternion.identity);
            Instantiate(block[Random.Range(0, 3)], new Vector3(position.x - 25f,
                position.y, 0), Quaternion.identity);

            other.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}