using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class DirectionTable
    {
        private const uint DirectionCount = 3600;
        private static Vector3[] directionTable = new Vector3[DirectionCount];

        static DirectionTable()
        {
            for (uint i = 0; i < DirectionCount; i++)
            {
                float angle = (i / (float)DirectionCount) * 360f;
                directionTable[i] = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                directionTable[i].Normalize();
            }
        }

        public static Vector3 GetDirection(float angle)
        {
            angle = angle % 360f;
            if (angle < 0) angle += 360f;
            uint index = (uint)(angle * DirectionCount / 360f);
            return directionTable[index];
        }
    }
}
