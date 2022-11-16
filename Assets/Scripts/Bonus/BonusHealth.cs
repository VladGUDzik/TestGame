using UnityEngine;

namespace Bonus
{
    public class BonusHealth : Bonus
    {
        private const int AddHealth = 1;

        public override void Activate(GameObject player)
        {
            if(GameObject.Find("Health").TryGetComponent(out Health health))
            {
                health.AddHealth(AddHealth);
            }
        }
    }
}
