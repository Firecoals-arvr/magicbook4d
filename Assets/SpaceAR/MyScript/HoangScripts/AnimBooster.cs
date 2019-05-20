using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firecoals.AssetBundles.Sound;

namespace Firecoals.Space
{
    /// <summary>
    /// chạy các animations của tàu con thoi
    /// </summary>
    public class AnimBooster : MonoBehaviour
    {
        private LoadSoundbundles _loadSoundbundle;

        /// <summary>
        /// tag cho âm thanh của tàu vũ trụ lúc chạy animation
        /// </summary>
        public string _musicbooster;

        Animator anim;
        public bool checkAnim;

        void Start()
        {
            anim = GetComponent<Animator>();
            checkAnim = false;
        }

        private void OnEnable()
        {
            _loadSoundbundle = FindObjectOfType<LoadSoundbundles>();
        }

        public void Launch()
        {
            anim.Play("Lauch");
            _loadSoundbundle.PlayMusicOfObjects(_musicbooster);
        }

        public void Open4D()
        {
            FirecoalsSoundManager.StopAll();
            anim.Play("4D");
        }
    }
}