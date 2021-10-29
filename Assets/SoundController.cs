using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private Slider volumeSetter;
    [SerializeField] private AudioClip good;
    [SerializeField] private AudioClip bad;
    private AudioSource music;
    void Start()
    {
        music = GetComponent<AudioSource>();
        music.volume = volumeSetter.value;
    }

    public void playGoodSFX() 
    {
         if (music.volume != volumeSetter.minValue)
        {
            music.volume += 0.5f;
            music.PlayOneShot(good);
            music.volume -= 0.5f;
        }

        else music.PlayOneShot(good);
    }

    public void playBadSFX()
    {
        if (music.volume != volumeSetter.minValue)
        {
            music.volume += 0.5f;
            music.PlayOneShot(bad);
            music.volume -= 0.5f;
        }
       
        else music.PlayOneShot(bad);
        
    }

    public void ChangeVolume() {
        music.volume = volumeSetter.value;
    }
}
