using UnityEngine;

namespace VectorExtensions
{
    public static class VectorExtension
    {
        public static Vector2 Gen2ByAngle(float theta)
        {
            return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
        }

        public static Vector3 Gen3ByAngle(float theta)
        {
            return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
        }
        public static Vector2 CastTo2(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }
        public static Vector3 CastTo3(this Vector2 vec)
        {
            return new Vector3(vec.x, vec.y, 0);
        }
        public static Vector2 RandVec(float r)
        {
            var _r = Random.Range(0, r);
            var _theta = Random.Range(0, 2 - Mathf.PI);
            return _r * Gen2ByAngle(_theta);
        }

        public static Vector3 Pow(this Vector3 vec, float p)
        {
            var l = vec.magnitude;
            if (l <= 0.001f) { return Vector3.zero; }
            var d = vec.normalized;
            return d * Mathf.Pow(l, p);
        }

        public static Vector3 Pow(Vector3 vec,float p,float r)
        {
            var d = vec.normalized;
            return d * Mathf.Pow(r, p);
        }
    }
}