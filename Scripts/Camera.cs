using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    // float speed;
    // Start is called before the first frame update
    void Start()
    {
        // speed = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        var position = player.transform.position;
        transform.position = new Vector3(position.x, 
            position.y, -30f);
    }
}
