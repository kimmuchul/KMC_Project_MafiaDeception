using System.Collections;
using System.Collections.Generic;
using KMC;
using UnityEngine;
using TMPro;
using UnityEditor.AssetImporters;

namespace NoF
{
    public class GameManager : Singleton<GameManager>
    {
        public GameManager instance;
        public List<ItemInfo> items = new List<ItemInfo>();
        public List<NPCInfo> npc = new List<NPCInfo>();
        public enum GameState
        {
            READY, START, PLAYING, TALKING, END // 수정필요
        }
        public GameObject statecanvas;
        public GameObject talkingCanvas;
        public GameObject GPTtalkingCanvas;
        public GameObject startCamera; // StartCamera 오브젝트를 할당할 변수
        public GPTBasicController ChatGPT;
        public TMP_Text dialogue;
        public QuestManager questManager;
        public StateUI stateUI;
        public Util util;
        [SerializeField]
        private GameState state;
        public GameState State { get { return state; } set { state = value; } }


        public override void Awake()
        {
            base.Awake();
            State = GameState.READY; // 초기화시 READY로 설정
        }
    }
}