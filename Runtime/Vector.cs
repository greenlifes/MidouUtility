using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Midou.Utility
{
    public static class Vector
    {
        public static Vector3 XZ(this Vector3 vector)
        {
            vector.y = 0;
            return vector;
        }
        public static Vector3 XZ(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
        public static float YAxisAngle(Vector3 localFrom, Vector3 localTo)
        {
            var angle = Quaternion.FromToRotation(localFrom.XZ(), localTo.XZ()).eulerAngles.y;
            return angle > 180 ? angle - 360 : angle;
        }
        public static float YAxisAngle(Vector3 WorldPosFrom, Vector3 WorldPosTo, Transform axisTransform)
        {
            return YAxisAngle(axisTransform.InverseTransformPoint(WorldPosFrom), axisTransform.InverseTransformPoint(WorldPosTo));
        }
    }
}
