// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections.Generic;
// using System.IO;
// using TMPro;

// public class DialogueManager : MonoBehaviour
// {
//     public TMP_Text dialogueTextUI;
//     public Button[] choiceButtons;
//     public TMP_Text[] choiceTexts;
//     //public GPTBasicController gPTBasicController;

//     private List<Dialogue> dialogues = new List<Dialogue>();
//     private int currentDialogueIndex = 0;

//     void Start()
//     {
//         LoadDialogueFromFile("Dialogue");
//         ShowDialogue(0);
//     }

//     void LoadDialogueFromFile(string fileName)
//     {
//         TextAsset textAsset = Resources.Load<TextAsset>(fileName);
//         if (textAsset == null)
//         {
//             Debug.LogError("파일을 찾을 수 없습니다!");
//             return;
//         }

//         string[] lines = textAsset.text.Split('\n');
//         Dialogue currentDialogue = null;

//         foreach (string line in lines)
//         {
//             string trimmedLine = line.Trim();
//             if (string.IsNullOrEmpty(trimmedLine)) 
//             {
//                 if (currentDialogue != null)
//                 {
//                     dialogues.Add(currentDialogue);
//                     currentDialogue = null;
//                 }
//                 continue;
//             }

//             if (currentDialogue == null)
//             {
//                 currentDialogue = new Dialogue(trimmedLine);
//             }
//             else
//             {
//                 string[] choiceData = trimmedLine.Split('|');
//                 if (choiceData.Length == 2 && int.TryParse(choiceData[1], out int affectionChange))
//                 {
//                     currentDialogue.choices.Add(new Choice(choiceData[0], affectionChange));
//                 }
//             }
//         }

//         if (currentDialogue != null) dialogues.Add(currentDialogue);
//     }

//     void ShowDialogue(int index)
//     {
//         if (index >= dialogues.Count) return;

//         dialogueTextUI.text = dialogues[index].text;

//         for (int i = 0; i < choiceButtons.Length; i++)
//         {
//             if (i < dialogues[index].choices.Count)
//             {
//                 choiceTexts[i].text = dialogues[index].choices[i].text;
//                 int choiceIndex = i;
//                 choiceButtons[i].onClick.RemoveAllListeners();
//                 choiceButtons[i].onClick.AddListener(() => SelectChoice(choiceIndex));
//                 choiceButtons[i].gameObject.SetActive(true);
//             }
//             else
//             {
//                 choiceButtons[i].gameObject.SetActive(false);
//             }
//         }
//     }

//     void SelectChoice(int choiceIndex)
//     {
//         if (currentDialogueIndex >= dialogues.Count) return;

//         int affectionChange = dialogues[currentDialogueIndex].choices[choiceIndex].affectionChange;
//         //gPTBasicController.like_value += affectionChange;
//         //Debug.Log("현재 호감도: " + gPTBasicController.like_value);

//         currentDialogueIndex++;
//         if (currentDialogueIndex < dialogues.Count)
//         {
//             ShowDialogue(currentDialogueIndex);
//         }
//         else
//         {
//             Debug.Log("대화 종료");
//         }
//     }

//     [System.Serializable]
//     public class Dialogue
//     {
//         public string text;
//         public List<Choice> choices = new List<Choice>();

//         public Dialogue(string text)
//         {
//             this.text = text;
//         }
//     }

//     [System.Serializable]
//     public class Choice
//     {
//         public string text;
//         public int affectionChange;

//         public Choice(string text, int affectionChange)
//         {
//             this.text = text;
//             this.affectionChange = affectionChange;
//         }
//     }
// }
