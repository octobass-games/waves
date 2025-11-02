using UnityEngine;

namespace Octobass.Waves.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Destroy all child game objects
        /// </summary>
        /// <paramref name="source"/> The transform whose children are to be destroyed
        public static void DestroyChildren(this Transform source)
        {
            foreach (Transform child in source)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Destroy all child game objects immediately. Unless required
        /// <see cref="DestroyChildren(Transform)"/> should be preferred
        /// </summary>
        /// <paramref name="source"/> The transform whose children are to be destroyed immediately
        public static void DestroyChildrenImmediate(this Transform source)
        {
            foreach (Transform child in source)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }
        }
    }
}
