using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using NoF;

namespace NoF.KMC
{
    public class AudioManager : Singleton<AudioManager>
    {
        public static AudioManager instance;
        public AudioSource bgm;
        public AudioSource sfx;

        public AudioClip[] audioSource;
    }
}