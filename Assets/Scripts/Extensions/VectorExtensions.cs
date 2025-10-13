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
            var _theta = Random.Range(0, 2 * Mathf.PI);
            return _r * Gen2ByAngle(_theta);
        }

        public static Vector3 VectorPow(Vector3 from, Vector3 to, float exp = 1, float scale = 1, float cons = 1)
        {
            var _mag = (to - from).magnitude;
            if (_mag == 0) return Vector3.zero;
            var _dir = (to - from).normalized;
            var force = cons * Mathf.Pow(_mag * scale, exp);
            return force * _dir;
        }

        public static Vector3 Force(Vector3 dir,float r)
        {
            return 100 * Mathf.Log(r, 2.0f) * dir;
        }
    }
}