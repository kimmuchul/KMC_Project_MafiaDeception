using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NoF
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField]
        private Button startButton;
        [SerializeField]
        private Button restartButton;
        [SerializeField]
        private GameObject gameOver;
        [SerializeField]
        private GameObject startMenu;

        // 추가된 퀘스트 UI 변수
        //[SerializeField]
        //private GameObject questUI;

        public static UIManager Instance;

        public override void Awake()
        {
            base.Awake();

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            gameOver.SetActive(false);

            // 퀘스트 UI 비활성화 초기화
            //questUI.SetActive(false);
        }

        private void Start()
        {
            startButton.gameObject.SetActive(GameManager.Instance.State == GameManager.GameState.READY);
            startButton.onClick.AddListener(OnStartButtonClicked);
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void Update()
        {
            startButton.gameObject.SetActive(GameManager.Instance.State == GameManager.GameState.READY);
            startMenu.gameObject.SetActive(GameManager.Instance.State == GameManager.GameState.READY);

            if (GameManager.Instance.State == GameManager.GameState.END)
            {
                StartCoroutine(GameOver());
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    GameManager.Instance.State = GameManager.GameState.END;
            //}
        }

        public void OnStartButtonClicked()
        {
            if (GameManager.Instance.State == GameManager.GameState.READY)
            {
                GameManager.Instance.State = GameManager.GameState.START;
                startMenu.SetActive(false);
            }
        }

        public void OnRestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.Instance.State = GameManager.GameState.READY;
        }

        public IEnumerator GameOver()
        {
            yield return new WaitForSeconds(1f);
            if (GameManager.Instance.State == GameManager.GameState.END)
            {
                restartButton.gameObject.SetActive(true);
                gameOver.gameObject.SetActive(true);
            }
        }

        // ✅ 퀘스트 UI 활성화/비활성화 메서드 추가
        //public void ShowQuestUI(bool isActive)
        //{
        //    questUI.SetActive(isActive);
        //}
    }
}
