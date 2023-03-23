using UnityEngine;

namespace Utils
{
    public class CameraUtils
    {
        public static float GetAngleToCursor(UnityEngine.Camera origin, Transform target)
        {
            Vector3 dir = GetDirToCursor(origin, target);
        
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            return angle;
        }
        
        public static Vector3 GetDirToCursor(UnityEngine.Camera origin, Transform target)
        {
            Vector3 mousePosition = origin.ScreenToWorldPoint(Input.mousePosition);
            var position = target.position;
            Vector3 dir = (mousePosition - position).normalized;
            
            return dir;
        }
    }
}