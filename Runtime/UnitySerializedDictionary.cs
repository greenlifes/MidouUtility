using System.Collections.Generic;
using UnityEngine;

namespace Midou.Utility
{
    public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        private List<TKey> keyData = new List<TKey>();

        [SerializeField, HideInInspector]
        private List<TValue> valueData = new List<TValue>();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < this.keyData.Count && i < this.valueData.Count; i++)
            {
                try
                {
                    if (this.TryAdd(this.keyData[i], this.valueData[i]))
                    {
#if DEBUG
                        //Yoto.Log.Info((this.GetType().Name + i));
#endif
                    }
                    else
                    {
                    }
                }
                catch (System.Exception e)
                {
                    var keyData = this.keyData[i];
                    var valueData = this.valueData[i];
                    if (keyData != null && valueData == null)
                    {
                        Debug.LogWarning($"[{this.GetType()}] OnAfterDeserialize error: (Idx='{i}') (Key='{keyData.GetType().Name}') (Value='NULL') (Message='{e.Message}')");
                    }
                    else if (keyData == null && valueData != null)
                    {
                        Debug.LogWarning($"[{this.GetType()}] OnAfterDeserialize error: (Idx='{i}') (Key='NULL') (Value='{valueData.GetType().Name}') (Message='{e.Message}')");
                    }
                    else
                    {
                        Debug.LogWarning($"[{this.GetType()}] OnAfterDeserialize error: (Idx='{i}') (Key/Value='???') (Message='{e.Message}')");
                    }
                }
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.keyData.Clear();
            this.valueData.Clear();

            bool isNull = false;
            foreach (var item in this)
            {
                this.keyData.Add(item.Key);
                this.valueData.Add(item.Value);
                if (item.Key == null)
                {
                    isNull = true;
                }
            }

            if (isNull)
            {
                var errorMsg = string.Empty;
                for (int m = 0; m < this.keyData.Count; m++)
                {
                    var key = this.keyData[m];
                    if (key != null)
                    {
                        errorMsg += $"[{m} = {key}]";
                    }
                    else
                    {
                        errorMsg += $"[{m} = Null]";
                    }
                }
                Debug.LogWarning($"[{this.GetType()}] OnBeforeSerialize => List : {errorMsg}");
            }
        }
    }
}
