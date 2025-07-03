using System.Collections;
using System.Collections.Generic;
using NoF;
using UnityEngine;

namespace KMC
{
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest1")]
public class QuestData : ScriptableObject
{
    [Header("퀘스트 정보")]
    public string questID;
    public int questIndex;
    public string questGiverID;            // 퀘스트 제공 NPC ID
    public string questReceiverID;         // 퀘스트 완료 NPC ID (같을 수도 있음)
    public string questStartDialogue;      // 퀘스트 시작 대화
    public string questInProgressDialogue; // 퀘스트 진행 중 대화
    public string questCompletedDialogue;  // 퀘스트 완료 대화
    public string[] questImpressgress;
    public string targetID;
    public int requiredAmount = 1;
}

}