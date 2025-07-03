using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoF;
using Unity.VisualScripting;
using UnityEditor.Compilation;
using UnityEngine.UI;
using TMPro;

namespace NoF
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 7.0f;
        public float backwardSpeed = 2.0f;
        public float rotateSpeed = 1f;
        public float highlightDistance = 3f; // 거리 제한
        public GameObject[] items;
        public GameObject[] npcs;
        private Animator m_animator;
        private Rigidbody m_rigidbody;
        private int m_state;
        private float m_v = 0;
        private float m_h = 0;
        public GameManager gameManager;
        public KeyCode interactionKey = KeyCode.E;
        public GameObject Effect;
        public ItemInfo emptyitem;
        public NPCInfo emptynpc;
        public Transform cameraTransform; // 카메라의 Transform 참조
        public float interactionDistance = 3.0f;
        public GameObject arrow;
        //public QuestManager questManager;
        public AudioManager audioManager;
        public string answer;
        public Transform NextNPCtransform;
        public bool isik = false;
        public NPC currentNPC;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_rigidbody = GetComponent<Rigidbody>();
            items = GameObject.FindGameObjectsWithTag("Item");
            npcs = GameObject.FindGameObjectsWithTag("NPC");
            // if (questManager == null)
            // {
            //     questManager = FindObjectOfType<QuestManager>();
            // }
            foreach (GameObject item in items)
            {
                if (item.transform.Find("HighlightEffect") == null && Effect != null)
                {
                    GameObject highlightEffect = Instantiate(Effect, item.transform);
                    highlightEffect.name = "HighlightEffect";
                    highlightEffect.transform.localPosition = Vector3.zero;
                    highlightEffect.SetActive(false);
                }
            }
        }
        private void Update()
        {
            m_v = Input.GetAxis("Vertical");
            m_h = Input.GetAxis("Horizontal");
            CheckAnimation();
            // 상호작용 처리
            if (Input.GetKeyDown(KeyCode.F))
            {
                bool isActive = gameManager.statecanvas.activeSelf;
                gameManager.statecanvas.SetActive(!isActive);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gameManager.util.ShowQuest();
                for (int i = 0; i < gameManager.items.Count; i++)
                {
                    gameManager.questManager.QuestState(emptynpc, gameManager.items[i]);
                }
                foreach (GameObject npc in npcs)
                {
                    if (npc.GetComponent<NPC>().npcInfo.npcName == gameManager.questManager.questDatas[gameManager.questManager.questindex].questReceiverID)
                    {
                        NextNPCtransform = npc.transform;
                    }
                }
                isik = !isik;
            }
            //items.RemoveAll(item => item == null); // 삭제된 아이템 제거
            foreach (GameObject item in items)
            {
                if (item == null) continue;

                float distance = Vector3.Distance(transform.position, item.transform.position);
                bool shouldHighlight = distance <= highlightDistance;

                Transform effectTransform = item.transform.Find("HighlightEffect");
                if (effectTransform != null)
                {
                    effectTransform.gameObject.SetActive(shouldHighlight);
                }
            }
            foreach (GameObject npc in npcs)
            {
                if (npc == null) continue;

                float distance = Vector3.Distance(transform.position, npc.transform.position);
                bool isInteraction = distance <= interactionDistance;
                if (isInteraction)
                {
                    Vector3 direction = transform.position - npc.transform.position;
                    direction.y = 0;
                    npc.transform.rotation = Quaternion.LookRotation(direction);
                }
            }
        }
        private void FixedUpdate()
        {

            // 카메라의 방향을 기준으로 이동 벡터 계산
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            //Vector3 desiredMoveDirection = (forward * m_v + right * m_h).normalized;
            Vector3 desiredMoveDirection = (forward * m_v + right * m_h).normalized;
            if (desiredMoveDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }

            // 이동 적용
            //m_rigidbody.velocity = forward * m_v * moveSpeed;
            m_rigidbody.AddForce(forward * m_v * moveSpeed);
        }
        private void CheckAnimation()
        {
            m_state = 0;
            if (m_v > 0)
            {
                m_state = 2;
            }
            else if (m_v < 0)
            {
                m_state = -1;
            }
            else if (m_h != 0)
            {
                m_state = 1;
            }
            m_animator.SetInteger("MoveState", m_state);
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                NPC npc = other.GetComponent<NPC>();
                currentNPC = npc;
                Answer(npc, "안녕하세요");
                gameManager.dialogue.text = answer;
                if (npc.animator != null)
                {
                    npc.animator.SetBool("handle", true);
                }
            }
        }
        private void OnAnimatorIK(int layerIndex)
        {
            if (!isik)
            {
                return;
            }
            m_animator.SetLookAtWeight(0.5f);
            m_animator.SetLookAtPosition(NextNPCtransform.position);
        }
        void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                NPC Chatnpc = other.GetComponent<NPC>();
                Item item = other.GetComponent<Item>();
                if (other.CompareTag("NPC"))
                {
                    gameManager.ChatGPT.SetChatAISystem(Chatnpc.npcInfo.name, Chatnpc.npcInfo.personality, Chatnpc.npcInfo.restrictions, Chatnpc.npcInfo.knowledge);
                    if (!gameManager.npc.Contains(Chatnpc.npcInfo))
                    {
                        gameManager.npc.Add(Chatnpc.npcInfo);
                        Debug.Log("NPC 정보 받기");
                    }
                    gameManager.stateUI.npcListUpdate();
                    //gameManager.ChatGPT.npc = Chatnpc;//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    //gameManager.ChatGPT.responseTMP.text = $"{Chatnpc.npcInfo.npcName} : ";//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    Answer(Chatnpc, gameManager.questManager.dialogetext);
                    gameManager.dialogue.text = answer;
                    gameManager.GPTtalkingCanvas.SetActive(true);
                    gameManager.questManager.QuestState(Chatnpc?.npcInfo, emptyitem);
                }

                if (other.CompareTag("Item"))
                {
                    gameManager.items.Add(item.itemInfo);
                    Debug.Log("아이템 먹기");
                    AudioManager.Instance.ItemSoundPlay(); // 아이템 사운드
                    Destroy(item.gameObject);
                    gameManager.stateUI.ItemListUpdate();
                    gameManager.questManager.QuestState(emptynpc, item?.itemInfo);
                }
                //gameManager.questManager.QuestState(Chatnpc?.npcInfo,item?.itemInfo);
            }
        }
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                gameManager.GPTtalkingCanvas.SetActive(false);
                gameManager.dialogue.text = "";
            }
        }
        async void Answer(NPC npc, string message)
        {
            gameManager.ChatGPT.SetChatAISystem(npc.npcInfo.name, npc.npcInfo.personality, npc.npcInfo.restrictions, npc.npcInfo.knowledge);
            answer = await gameManager.ChatGPT.SendMessageToGPTAndGetAnswer(message);
        }
        public void AnswerButton(TMP_InputField inputField)
        {
            Answer(currentNPC, inputField.text);
            gameManager.dialogue.text = answer;
        }
    }
}
