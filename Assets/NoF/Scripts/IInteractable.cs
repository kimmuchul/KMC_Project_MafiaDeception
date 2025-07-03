// // IInteractable.cs - 상호작용 가능한 모든 오브젝트를 위한 인터페이스
// using UnityEngine;
// using NoF;
// using Unity.VisualScripting;

// namespace NoF
// {
//     public interface IInteractable
//     {
//         //void Interact(PlayerController player);
//         // 플레이어 컨트롤러를 우리가 사용할 컨트롤러로 변경
//         void InteractItem(ItemInfo item)
//         {
//             item.itemIndex = 1;
//         }
//         void InteractNPC(mainNPC mainnpc)
//         {
//             switch(mainnpc.NPCIndex)
//             {
//                 case 0:
//                 Debug.Log("Leon");
//                 break;
//                 case 1:
//                 Debug.Log("Alonso");
//                 break;
//                 case 2:
//                 Debug.Log("Darco");
//                 break;
//                 case 3:
//                 Debug.Log("Jan");
//                 break;
//                 case 4:
//                 Debug.Log("Bernice");
//                 break;
//             }
//         }
//         void InteractsubNPC(subNPC subnpc)
//         {
            
//         }
//         void Interact()
//         {

//         }
//     }
// }