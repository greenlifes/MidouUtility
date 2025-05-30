using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Midou.Utility
{
    public static class TweenValue
    {
        public static float EaseOutCubic(float t)
        {
            var progress = Mathf.Clamp01(t);
            return (float)(1f - Math.Pow(1f - progress, 3f));
        }
        public static float EaseOutExpo(float t)
        {
            var progress = Mathf.Clamp01(t);
            return (float)progress >= 1f ? 1f : (1f - Mathf.Pow(2f, -10f * progress));
        }
        public static float ParabolaArc(float t)
        {
            var progress = Mathf.Clamp01(t);
            return 1.0f - 4.0f * (progress - 0.5f) * (progress - 0.5f);
        }
        public static float EaseOutBounce(float t)
        {
            var progress = Mathf.Clamp01(t);
            const float N1 = 7.5625f;
            const float D1 = 2.75f;
            if (progress < 1 / D1) { return N1 * progress * progress; }
            else if (progress < 2 / D1) { return N1 * (progress -= 1.5f / D1) * progress + 0.75f; }
            else if (progress < 2.5 / D1) { return N1 * (progress -= 2.25f / D1) * progress + 0.9375f; }
            else { return N1 * (progress -= 2.625f / D1) * progress + 0.984375f; }
        }
        public static Vector3 Bezier(float t, Vector3 A, Vector3 outTangent, Vector3 B, Vector3 inTangent)
        {
            t = Mathf.Clamp01(t);
            var At = A + outTangent;
            var Bt = B + inTangent;
            return A * Mathf.Pow((1 - t), 3)
                + 3 * At * t * Mathf.Pow((1 - t), 2)
                + 3 * Bt * Mathf.Pow(t, 2) * (1 - t)
                + B * Mathf.Pow(t, 3);
        }
    }
}
