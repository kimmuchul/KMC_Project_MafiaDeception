using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoF;
using KMC;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;

namespace NoF
{
    public class QuestManager : MonoBehaviour
    {
        public List<QuestData> questDatas;
        public int questindex = 0;
        public int dialogueindex;
        public TMP_Text questText;
        public GameManager gameManager;
        public string dialogetext;
        public TMP_Text NoteInfoText;
        public GameObject leonbutton;
        public GameObject Bernicebutton;
        public Button button;
        public enum State
        {
            Start,
            NotClear,
            Clear,
            End
        }
        public State state;
        // Start is called before the first frame update
        void Start()
        {
            state = State.Start;
            dialogueindex = 0;
            //dialogetext = gameManager.ChatGPT.responseTMP;//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        }
        public void QuestState(NPCInfo npcInfo, ItemInfo item)
        {
            switch (state)
            {
                case State.Start:
                    if (npcInfo.npcName == questDatas[questindex].questGiverID)
                    {
                        questText.text = questDatas[questindex].questDescription;
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            dialogetext = questDatas[questindex].questInprogressDoalogues[dialogueindex];

                            dialogueindex++;
                            if (dialogueindex >= questDatas[questindex].questInprogressDoalogues.Length)
                            {
                                state = State.NotClear;
                                dialogueindex = 0;
                            }
                        }
                    }
                    break;
                case State.NotClear:
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("실행");
                        dialogetext = npcInfo.npcName == questDatas[questindex].questGiverID ? questDatas[questindex].questInprogressDoalogues[dialogueindex]
                         : npcInfo.startdialogue;//npcInfo.dialogue;
                    }

                    if (gameManager.items.Contains(item))
                    {
                        if (item.itemIndex == questDatas[questindex].targetID)
                        {
                            state = State.Clear;
                        }
                    }
                    break;
                case State.Clear:
                    if (npcInfo.npcName == questDatas[questindex].questReceiverID)
                    {
                        dialogueindex = 0;
                        //dialogetext = questDatas[questindex].questCompletedDialogue;//GPT변환
                        questindex++;
                        if (questindex == 4)
                        {
                            leonbutton.SetActive(true);
                        }
                        if (questindex == 3)
                        {
                            Bernicebutton.SetActive(true);
                        }
                        state = State.Start;
                    }
                    break;
            }
        }
        public void ShowInfonpc(NPC npc)
        {
            NoteInfoText.text = npc.npcInfo.npcInformation;
        }
        public void ShowInfoitem(Item item)
        {
            NoteInfoText.text = item.itemInfo.ItemInformation;
        }
    }
}
