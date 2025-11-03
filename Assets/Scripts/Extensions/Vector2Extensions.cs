using UnityEngine;

namespace Octobass.Waves.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 ProjectX(this Vector2 source)
        {
            return new Vector2(source.x, 0);
        }

        public static Vector2 ProjectY(this Vector2 source)
        {
            return new Vector2(0, source.y);
        }

        public static Vector2 Quantize(this Vector2 source, float step)
        {
            return new Vector2(Mathf.Round(source.x / step) * step, Mathf.Round(source.y / step) * step);
        }
    }
}
