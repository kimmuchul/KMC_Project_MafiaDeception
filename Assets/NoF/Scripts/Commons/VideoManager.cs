using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace NoF
{
    public class VideoManager : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        public VideoClip startClip;
        public VideoClip endClip;

        private GameManager.GameState lastState; // 게임의 현재상태 감지
        void Start()
        {
            videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
            videoPlayer.targetCamera = Camera.main;
            videoPlayer.loopPointReached += OnVideoEnd;

            lastState = GameManager.Instance.State; // 시작전 상태를 LastState에 저장
        }

        private void Update()
        {
            GameManager.GameState currentState = GameManager.Instance.State;

            if (currentState != lastState) // lastState가 현상태와 다르면 실행
            {
                lastState = currentState; // lastState에 현재상태를 저장해주고
                HandleGameStateChange(currentState); // 아래 코드를 실행한다
            }
        }
        private void HandleGameStateChange(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.START:
                    PlayVideo(startClip); // 비디오를 실행시킬건데 startClip에 있는걸로
                    break;
                case GameManager.GameState.END:
                    PlayVideo(endClip); // 비디오를 실행시킬건데 endClip에 있는걸로
                    break;
            }
        }

        private void PlayVideo(VideoClip clip)
        {
            if (clip == null) // 근데 이제 이게 비어있으면 아래코드를 실행
            {
                Debug.LogError("비디오 클립이 설정되지 않았습니다!");
                return;
            }
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.clip = clip; // 안비어있으면 start/end 에 맞는 클립을
            videoPlayer.Play(); // 실행해라
        }

        private void OnVideoEnd(VideoPlayer vp) // 비디오 루프가 끝나면
        {
            Debug.Log("비디오 종료"); // 종료됐다고 표시해주기
            videoPlayer.gameObject.SetActive(false);
            if (GameManager.Instance.State == GameManager.GameState.START)
            {
                GameManager.Instance.State = GameManager.GameState.PLAYING;
            }
            else if (GameManager.Instance.State == GameManager.GameState.END)
            {
                // 게임 종료 후 상태 전환 (예: READY)
                GameManager.Instance.State = GameManager.GameState.READY;
            }
        }

    } // class
} // name