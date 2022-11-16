using Player;
using UnityEngine;

namespace Bonus
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] protected float damageSpikes;
        [SerializeField] private Vector2 angle;
        [SerializeField] public bool poison;
        [SerializeField] protected float damagePoison;

        public float startPeriod;

        private float period;
        private bool checkPoison;

        private void Start()
        {
            checkPoison = false;
        }

        private void FixedUpdate()
        {
            if (!checkPoison) return;
            period -= Time.deltaTime;
            if (period > 0)
            {
                Debug.Log("Damage poison " + damagePoison);
            }
            else
            {
                checkPoison = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D player)
        {
            if (!player.CompareTag("Player")) return;

            if (poison)
            {
                period = startPeriod;
                checkPoison = true;
                Debug.Log("Damage spikes " + damageSpikes);
                Debug.Log("Knockback angle " + angle);
            }
            else
            {
                Debug.Log("Damage spikes " + damageSpikes);
                Debug.Log("Knockback angle " + angle);
            }
        }
        
    }
}