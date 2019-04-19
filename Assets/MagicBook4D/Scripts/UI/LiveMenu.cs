﻿using Firecoals.AssetBundles.Sound;

namespace Firecoals.UI
{
    using UnityEngine;
    /// <summary>
    /// Handle texture and audio playing in the Menu Scene
    /// </summary>
    public class LiveMenu : MonoBehaviour
    {
        private bool _animalIsPlaying = false, _spaceIsPlaying, _colorIsPlaying, _comingSoon;

        public UITexture backDrop;
        public AudioClip colorAudioClip, animalAudioClip, spaceAudioClip, backgroundClip;

        private void Start()
        {
            FirecoalsSoundManager.PlayMusic(backgroundClip);
        }
        private void Update()
        {
            if (transform.localPosition.x >= -500 && transform.localPosition.x <= -300 && _animalIsPlaying == false)
            {
                //TODO Animal
                //Debug.Log("<color=green>Animal is playing</color>");
                FirecoalsSoundManager.StopAllSounds();
                FirecoalsSoundManager.PlaySound(animalAudioClip, true);
                
                backDrop.mainTexture = Resources.Load<Texture2D>("Bg_texture/menu_ bg animal");
                _animalIsPlaying = true;
                _colorIsPlaying = false;
                _spaceIsPlaying = false;
            }
            if (transform.localPosition.x < -800 && transform.localPosition.x >=-1200 && _colorIsPlaying == false)
            {
                //TODO color
                //Debug.Log("<color=yellow>Color is playing</color>");
                FirecoalsSoundManager.StopAllSounds();
                FirecoalsSoundManager.PlaySound(colorAudioClip, true);
                backDrop.mainTexture = Resources.Load<Texture2D>("Bg_texture/menu_bg color");
                _animalIsPlaying = false;
                _colorIsPlaying = true;
                _spaceIsPlaying = false;
            }
            if (transform.localPosition.x >= 300 && transform.localPosition.x <= 500 && _spaceIsPlaying == false)
            {
                //TODO Space
                //Debug.Log("<color=pink>Space is playing</color>");
                FirecoalsSoundManager.StopAllSounds();
                FirecoalsSoundManager.PlaySound(spaceAudioClip,true);
                backDrop.mainTexture = Resources.Load<Texture2D>("Bg_texture/menu_bg space");
                _animalIsPlaying = false;
                _colorIsPlaying = false;
                _spaceIsPlaying = true;
            }
            if (transform.localPosition.x > 800f)
            {
                //TODO ComingSoon
                //Debug.Log("<color=pink>Space is playing</color>");
                FirecoalsSoundManager.StopAllSounds();
                backDrop.mainTexture = Resources.Load<Texture2D>("Bg_texture/bg_all");
                _animalIsPlaying = false;
                _colorIsPlaying = false;
                _spaceIsPlaying = false;
            }
        }
    }

}