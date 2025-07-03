using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoF
{
    [CreateAssetMenu(fileName = "NewNPC", menuName = "Inventory/NPC")]
    public class NPCInfo : ScriptableObject
    {
        public string npcName = "Jan";
        public string startdialogue;
        public string npcInformation;
        public int npcIndex;
        public string personality;
        public string restrictions;
        public string knowledge;
    }
}
