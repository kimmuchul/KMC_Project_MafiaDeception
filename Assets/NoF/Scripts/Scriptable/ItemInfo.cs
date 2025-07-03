using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoF
{
        [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
        public class ItemInfo : ScriptableObject
        {
            public string itemName;
            public int itemIndex;
            public string ItemInformation;
        }
}
