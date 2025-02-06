using UnityEngine;

namespace MeatAndSoap
{
    public static class EasingLib
    {
        public static float EaseInOutSine(float x)
        {
            return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
        }

        public static float EaseOutSine(float x)
        {
            return Mathf.Sin((x * Mathf.PI) / 2);
        }

        public static float EaseOutQuint(float x)
        {
            return 1 - Mathf.Pow(1 - x, 5);
        }

        public static float EaseOutQuad(float x)
        {
            return 1 - (1 - x) * (1 - x);
        }
        public static float DerEaseOutQuad(float x, float k)
        {
            return 2 * k * (1 - x * k);
        }

        public static float EaseInQuart(float x)
        {
            return x * x * x * x;
        }
        public static float DerEaseInQuart(float x, float k = 1f)
        {
            return 4 * Mathf.Pow( k, 4f) * x * x * x;
        }
    }
}