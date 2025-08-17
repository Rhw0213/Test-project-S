using System.Collections.Generic;
using UnityEngine;

namespace MySTDLib
{
    [System.Serializable]
    public class MyDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(); 

        public Dictionary<TKey, TValue> Ref => dictionary;

        // Unity가 직렬화 후 자동으로 호출
        public void OnAfterDeserialize()
        {
            MakeDictionary();
        }

        // Unity가 직렬화 전 자동으로 호출
        public void OnBeforeSerialize()
        {
            // Inspector에서 변경된 내용을 Dictionary에 반영할 때 사용
        }

        public void MakeDictionary()
        {
            if (keys == null || values == null || keys.Count == 0 || values.Count == 0)
            {
                Debug.LogError("keys or values count zero!!!");
                return;
            }

            if (keys.Count != values.Count)
            {
                Debug.LogError("keys or values count not same!!!");
                return;
            }

            dictionary.Clear();

            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
            }
        }
    }
}

