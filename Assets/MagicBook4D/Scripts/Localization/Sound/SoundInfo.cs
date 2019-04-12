using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.AssetBundles.Sound
{
    /// <summary>
    /// The information of Sound
    /// </summary>
    [System.Serializable]
    public class SoundInfo : ISerializationCallbackReceiver
    {
        public static SoundInfo Parse(string json)
        {
            return JsonUtility.FromJson<SoundInfo>(json);
        }

        [SerializeField]
        private string soundID;
        [SerializeField]
        private string fileName;
        [SerializeField]
        private string bundleName;
        [SerializeField]
        private string language;
        [SerializeField]
        private string category = null;
        [SerializeField]
        private string tag = null;
        [SerializeField]
        private string pathBundle;
        /// <summary>
        /// SoundInfo
        /// </summary>
        /// <param name="soundID">sound id is unique</param>
        /// <param name="fileName">file name.extention</param>
        /// <param name="bundleName">name of the bundle</param>
        /// <param name="language">language of this sound</param>
        /// <param name="category">category</param>
        /// <param name="tag">tag</param>
        /// <param name="pathBundle">Path of the sound is built assetbundle</param>
        public SoundInfo(string soundID, string fileName, string bundleName, string language, string category, string tag, string pathBundle)
        {
            if (string.IsNullOrEmpty(soundID))
                throw new System.ArgumentNullException("soundID");
            if (string.IsNullOrEmpty(pathBundle))
                throw new System.ArgumentNullException("pathBundle");


            this.soundID = soundID;
            this.fileName = fileName;
            this.bundleName = bundleName;
            this.language = language;
            this.category = category;
            this.tag = tag;
            this.pathBundle = pathBundle;
        }
        /// <summary>
        /// Get the sound id
        /// </summary>
        public string SoundID { get { return this.soundID; } }
        /// <summary>
        /// Get the file Name of the sound
        /// </summary>
        public string FileName { get { return this.fileName; } }
        /// <summary>
        /// Get the name of the asset bundle
        /// </summary>
        public string BundleName { get { return this.bundleName; } }
        /// <summary>
        /// Get the language of the sound
        /// </summary>
        public string Language { get { return this.language; } }
        /// <summary>
        /// Get the category of the sound
        /// </summary>
        public string Category { get { return this.category; } }
        /// <summary>
        /// Get the file Name of the sound
        /// </summary>
        public string Tag { get { return this.tag; } }
        /// <summary>
        /// Get the path was the sound is built
        /// </summary>
        public string PathBundle { get { return this.pathBundle; } }

        public virtual void OnBeforeSerialize()
        {

        }

        public virtual void OnAfterDeserialize()
        {

        }

        /// <summary>
        /// Convert SoundInfo to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}

