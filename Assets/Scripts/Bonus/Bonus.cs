using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Bonus
{
    public abstract class Bonus : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent activated;

        public PlayerMovementController Player;

        public void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
            Activate(Player.gameObject);
        }
        
        public virtual void Activate(GameObject player)
        { }
    }
}
