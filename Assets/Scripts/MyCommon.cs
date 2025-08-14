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
        public static GameObject FindParentTag(GameObject obj, string tag, uint depth = 10)
        {
            GameObject finded = null;

            for (uint i = 0; i < depth; i++)
            {
                if (obj.CompareTag(tag))
                {
                    finded = obj;
                    break;
                }

                if (!obj || !obj.transform.parent) break;

                obj = obj.transform.parent.gameObject;
            }

            return finded;
        }

        public static GameObject FindChildTag(GameObject obj, string tag)
        {
            GameObject result = null;
            RecurciveFindChildTag(obj, tag, ref result);

            return result;
        }
        static void RecurciveFindChildTag(GameObject obj, string tag, ref GameObject result)
        {
            var childs = obj.transform.GetComponentsInChildren<Transform>(true);

            if (childs == null || childs.Length == 0) return;

            foreach(var trans in childs)
            {
                if (trans.gameObject.CompareTag(tag))
                {
                    result = trans.gameObject;
                    return;
                }
            }

            RecurciveFindChildTag(childs[0].gameObject, tag, ref result);   
        }

        public static GameObject FindParentPlayerAndEnemy(GameObject obj)
        {
            string[] findObjetTags = new string[]{
                "Player", "Enemy"
            };

            foreach (string str in findObjetTags)
            {
                GameObject result = FindParentTag(obj, str);
                if (result) return result;
            }

            return null;
        }

        public static void ChangeFlibX(GameObject obj, bool isFlibX = true)
        {
            if (obj)
            {
                obj.GetComponent<SpriteRenderer>().flipX = isFlibX;
            }
        }
    }
}
