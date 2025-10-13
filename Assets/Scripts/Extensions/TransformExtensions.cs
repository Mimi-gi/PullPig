using UnityEngine;

namespace TransformExtensions
{
    public static class TransformExtension
    {
        public static Vector3 Position(this GameObject obj)
        {
            return obj.transform.position;
        }

        public static float Distance(GameObject A,GameObject B)
        {
            return (A.transform.position - B.transform.position).magnitude;
        }
    }
}