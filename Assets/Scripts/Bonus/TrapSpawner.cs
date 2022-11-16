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
           
            var transform1 = component.gameObject.transform.position;
            transform1.x += 1.97f;
            Instantiate(component.trap.gameObject, transform1, transform.rotation);

        }
    }
}