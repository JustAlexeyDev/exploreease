using UnityEngine;

public class PickupableEntity : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collision.gameObject.name");
        if (collider.gameObject.CompareTag("Item"))
        {
            
        }
        if (collider.gameObject.TryGetComponent(out HandedEntity hands))
        {
            // TODO
            if (this.TryGetComponent(out Weapon weapon))
            {
                hands.AttachItem(weapon);
            }
        }
    }
}
