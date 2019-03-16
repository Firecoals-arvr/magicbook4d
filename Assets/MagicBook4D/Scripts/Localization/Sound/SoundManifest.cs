using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.AssetBundles.Sound
{
    /// <summary>
    /// All info about sound
    /// </summary>
    [System.Serializable]
    public class SoundManifest : ISerializationCallbackReceiver
    {
        /// <summary>
        /// Convert from JSON to SoundManifest object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static SoundManifest Parse(string json)
        {
            return JsonUtility.FromJson<SoundManifest>(json);
        }

        [SerializeField]
        public SoundInfo[] soundInfos;

        public virtual void OnBeforeSerialize()
        {

        }

        public virtual void OnAfterDeserialize()
        {

        }

    }
}

