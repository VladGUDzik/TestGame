using Player;
using UnityEngine;

namespace Bonus
{
    public class BonusSpeed : Bonus
    {
       private const int speedAdd = 1;

       public override void Activate(GameObject player)
       {
           if (player.TryGetComponent<PlayerMovementController>(out var component))
           {
               component.walkSpeed += speedAdd;
           }
       }
    }
}
