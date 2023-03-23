using UnityEngine;
using Utils;

namespace Weapons
{
    public class MouseAimController : MonoBehaviour
    {
        // Automate set with Player (in method Start)
        public Transform aimReference;
    
        void Update()
        {
            float angle = CameraUtils.GetAngleToCursor(UnityEngine.Camera.main, aimReference.transform);
            
            aimReference.transform.eulerAngles = new Vector3(0, 0, angle + 35f);
        }
    }
}
