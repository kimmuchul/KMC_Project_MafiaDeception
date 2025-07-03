using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Rendering.LookDev;

namespace NoF
{
// [CreateAssetMenu(fileName = "NewNPC", menuName = "Inventory/NPC")]
// public class NPCInfo : ScriptableObject
// {
//     public string npcName = "Jan";
//     public string startdialogue;
//     public string npcInformation;
//     public int npcIndex;
//     public string personality;
//     public string restrictions;
//     public string knowledge;
// }

    public class NPC : MonoBehaviour
    {
        //[SerializeField] private Quest questData;

        //[SerializeField] private GameObject interactionIndicator;
        public NPCInfo npcInfo;
        public GameObject dialoguePanel;  // 대화 패널
        public TMP_Text nameTMP;          // NPC 이름 표시 텍스트
        public TMP_Text dialogueTMP;      // 대화 텍스트

        private bool isInteracting = false;  // 상호작용 여부
        public Animator animator;
        private Transform player;
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            //if(interactionIndicator) interactionIndicator.SetActive(false);
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }

            animator = GetComponent<Animator>();
        }

        public void Interact(PlayerController player)
        {
            if (isInteracting) { return; }

            isInteracting = true;
            dialoguePanel?.SetActive(true);

            //// NPC 이름에 따라 다른 대화 내용 처리
            //if (npcName == "Alonso")
            //{
            //    StartCoroutine(ShowDialogues(alonsoDialogues)); // Alonso의 대화 사용
            //}
            //else
            //{
            //    StartCoroutine(ShowDialogues(dialogues)); // 기본 대화 사용
            //}

            // 플레이어 바라보기
            Vector3 lookDirection = player.transform.position - transform.position;
            lookDirection.y = 0;
            this.transform.rotation = Quaternion.LookRotation(lookDirection);

            // 애니메이션 'handle' 상태로 전환
            if (animator != null)
            {
                animator.SetBool("handle", true);
            }
        }
        string answer;

       void SendMessageAswer()
        {
            GPTBasicController.Instance.SetChatAISystem(npcInfo.name, npcInfo.personality, npcInfo.restrictions, npcInfo.knowledge);
             
        } 

        async void GetAnswer(string message)
        {
              answer =   await GPTBasicController.Instance.SendMessageToGPTAndGetAnswer(message);
        }
            
    }
}