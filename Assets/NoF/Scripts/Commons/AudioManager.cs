using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NoF
{
    [System.Serializable] 
    public class Audio
    {
        public string audioName;
        public AudioClip clip;
    }

    public class AudioManager : Singleton<AudioManager>
    {
        // NPC인사말에 쓸 예정이므로 NPC Script에 붙여서 쓸 예정 'NPCCC'예시 스크립트 참조

        public Audio[] nPCSounds;
        public Audio itemSounds;
        private List<AudioSource> audioSources = new List<AudioSource>(); //오디오 소스 풀 리스트
        [SerializeField] private int maxAudioList = 20; //최대 오디오 소스 개수

        public override void Awake()
        {
            base.Awake();

            // 오디오 소스 풀 생성
            for (int i = 0; i < maxAudioList; i++)
            {
                AudioSource newSource = gameObject.AddComponent<AudioSource>();
                newSource.playOnAwake = false;
                audioSources.Add(newSource);
            }
        }

        //public void NPCSoundPlay(string name)
        //{
        //    Audio playSound = Array.Find(nPCSounds, x => x.audioName == name);

        //    if (playSound == null)
        //    {
        //        Debug.LogWarning($"Sound '{name}' not found in Sounds array!");
        //        return;
        //    }

        //    AudioSource availableSource = GetAvailableAudioSource();

        //    if (availableSource != null)
        //    {
        //        availableSource.PlayOneShot(playSound.clip); //PlayOneShot으로 중첩 가능하게 재생
        //    }
        //}
        public void ItemSoundPlay()
        {
            AudioSource availableSource = GetAvailableAudioSource();

            if (availableSource != null)
            {
                availableSource.PlayOneShot(itemSounds.clip); //PlayOneShot으로 중첩 가능하게 재생
            }
        }

        private AudioSource GetAvailableAudioSource()
        {
            foreach (var source in audioSources)
            {
                if (!source.isPlaying) // 현재 재생 중이 아닌 오디오 소스 찾기
                {
                    return source;
                }
            }

            return null; // 모든 소스가 사용 중이면 null 반환
        }
    }
    
    
}