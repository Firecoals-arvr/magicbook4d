using Firecoals.AssetBundles.Sound;

namespace Firecoals.UI
{
    using UnityEngine;
    /// <summary>
    /// Handle texture and audio playing in the Menu Scene
    /// </summary>
    public class LiveMenu : MonoBehaviour
    {
        private bool _animalIsPlaying = false, _spaceIsPlaying, _colorIsPlaying;

        public UITexture backDrop;
        public AudioClip colorAudioClip, animalAudioClip, spaceAudioClip, backgroundClip;

        private void Start()
        {
            FirecoalsSoundManager.PlayMusic(backgroundClip);
        }
        private void Update()
        {
            if (transform.localPosition.x < 100 && transform.localPosition.x > -100 && _animalIsPlaying == false)
            {
                //TODO Animal
                //Debug.Log("<color=green>Animal is playing</color>");
                FirecoalsSoundManager.StopAllSounds();
                FirecoalsSoundManager.PlaySound(animalAudioClip, true);
                
                backDrop.mainTexture = Resources.Load<Texture2D>("Bg_texture/menu_bg_animal");
                _animalIsPlaying = true;
                _colorIsPlaying = false;
                _spaceIsPlaying = false;
            }
            if (transform.localPosition.x < -600 && transform.localPosition.x <= -100 && _colorIsPlaying == false)
            {
                //TODO color
                //Debug.Log("<color=yellow>Color is playing</color>");
                FirecoalsSoundManager.StopAllSounds();
                FirecoalsSoundManager.PlaySound(colorAudioClip, true);
                backDrop.mainTexture = Resources.Load<Texture2D>("Bg_texture/menu_bg_color");
                _animalIsPlaying = false;
                _colorIsPlaying = true;
                _spaceIsPlaying = false;
            }
            if (transform.localPosition.x > 600 && transform.localPosition.x >= 100 && _spaceIsPlaying == false)
            {
                //TODO Space
                //Debug.Log("<color=pink>Space is playing</color>");
                FirecoalsSoundManager.StopAllSounds();
                FirecoalsSoundManager.PlaySound(spaceAudioClip,true);
                backDrop.mainTexture = Resources.Load<Texture2D>("Bg_texture/menu_bg_space");
                _animalIsPlaying = false;
                _colorIsPlaying = false;
                _spaceIsPlaying = true;
            }
        }
    }

}