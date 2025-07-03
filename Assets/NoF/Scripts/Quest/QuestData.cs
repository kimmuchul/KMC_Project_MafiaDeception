using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoF
{
    [CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest4Practice")]
    public class QuestData : ScriptableObject
    {
        [Header("기본 정보")]
        public string questID; // 퀘스트 이름
        public string questName; // 퀘스트 이름
        [TextArea(3, 10)]
        public string questDescription; // 퀘스트상세정보를 저장할곳

        [Header("NPC 정보")]
        public string questGiverID; // 퀘스트 주는 것
        public string questReceiverID; // 퀘스트 끝내는 것

        [Header("대화 내용")]
        //[TextArea(2, 5)] public string questStartDialogue; // NPC상호작용(퀘스트시작)

        [TextArea(2, 5)] public string[] questInprogressDoalogues; // 질문횟수에 대한 지정대사

        //[TextArea(2, 5)] public string questCompletedDialogue; // API종료(퀘스트완료)

        [Header("목표")]
        public string targetName; // 마스터키
        public int targetID; // 아이템에 ID값 주기위해
        public int requiredTargetAmount; // 타켓을 만났는가 (질문 횟수만큼 질문 했는지 체크)
        public int questionCount; // 질문횟수 3회로 해야함
        public string keyword; // 다니엘
        public bool requiredKeyword = true; // 키워드(다니엘) 말했는지 체크
    }
}
