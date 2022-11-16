using UnityEngine;

namespace Player
{
    public class AutoPlayerBullet : MonoBehaviour
    {
        [SerializeField] protected float speed, timer;
        [SerializeField] private LayerMask whatIsEnemy;
        private Rigidbody2D _rb;
    
        private void Start()
        {
            name = "Bullet";
      
            _rb = GetComponent<Rigidbody2D>();
            if (!AutoWeaponController.target) return;
            var position = AutoWeaponController.target.transform.position;
            var dir = Vector2.up * (position.y - transform.position.y) +
                      Vector2.right * (position.x - transform.position.x) /
                      (Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                          new Vector2(position.x, position.y)) / 3);

            _rb.AddForce(dir * (speed * Time.deltaTime), ForceMode2D.Impulse);
        }
    
        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                Destroy(gameObject);
        }

    }
}
