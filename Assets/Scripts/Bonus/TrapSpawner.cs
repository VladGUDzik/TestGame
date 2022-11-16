using Player;
using UnityEngine;

namespace Bonus
{
    public class TrapSpawner:Bonus
    {
        public override void Activate(GameObject player)
        {
            if (!player.TryGetComponent(out PlayerMovementController component)) return;
            gameObject.SetActive(true);
           
            var transform1 = transform;
            Instantiate(component.trap.gameObject, transform1.position, transform1.rotation);

        }
    }
}