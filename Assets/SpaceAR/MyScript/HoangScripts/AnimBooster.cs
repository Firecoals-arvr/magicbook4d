using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Space
{
    /// <summary>
    /// chạy các animations của tàu con thoi
    /// </summary>
    public class AnimBooster : MonoBehaviour
    {
        [SerializeField] Animation _boosterAnimation = default;
        // Start is called before the first frame update

        private LoadSoundbundles _loadSoundbundle;

        /// <summary>
        /// tag của âm thanh trong assetbundle
        /// </summary>
        public string _boosterMusic;

        void Start()
        {
            //anim = GetComponent<Animator>();
            _loadSoundbundle = GameObject.FindObjectOfType<LoadSoundbundles>();
        }
        public void Launch()
        {
            if (_boosterAnimation.isPlaying)
            {
                _boosterAnimation.Play("Idle");
            }
            else
            {
                _boosterAnimation.Play("Idle");
                _boosterAnimation.PlayQueued("Lauch");
            }
            _loadSoundbundle.PlayMusicOfObjects(_boosterMusic);
        }
        public void Open4D()
        {
            if (!_boosterAnimation.isPlaying)
            {
                _boosterAnimation.Play("4D");
            }
        }
    }
}
