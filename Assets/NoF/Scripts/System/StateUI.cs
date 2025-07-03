using System.Collections;
using System.Collections.Generic;
using NoF;
using NoF.CMG;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace NoF
{
public class StateUI : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] ItemButtons;
    public GameObject[] NPCButtons;
    public DroppableUI[] ClueSystem;
    public GameObject imageprefab;
    public Image[] image;
    public bool isDetected;
    
    public void ItemListUpdate()
    {
        foreach(ItemInfo item in gameManager.items)
        {
            ItemButtons[item.itemIndex-101].SetActive(true);
        }
    }
    public void npcListUpdate()
    {
        foreach(NPCInfo npc in gameManager.npc)
        {
            NPCButtons[npc.npcIndex].SetActive(true);
        }
    }
    public void ClearClue()
    {
        for(int i = 0; i<2;i++)
        {
            image[i].color = (ClueSystem[3*i].isClueRight&&ClueSystem[3*i+1].isClueRight&&ClueSystem[3*i+2].isClueRight)? Color.green:Color.red;
        }
    }
}
}