using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class MyCommon
    {
        public static Quaternion RadiansToQuaternion(Vector3 radians)
        {
            return Quaternion.Euler(radians * Mathf.Rad2Deg);
        }

        public static Vector3 QuaternionToRadians(Quaternion quaternion)
        {
            Vector3 eulerDegrees = quaternion.eulerAngles;
            return eulerDegrees * Mathf.Deg2Rad;
        }
    }
}
