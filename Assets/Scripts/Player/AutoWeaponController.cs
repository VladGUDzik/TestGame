using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AutoWeaponController : Bonus.Bonus
    {
        [SerializeField] protected List<GameObject> bullets;
        [SerializeField] protected List<Transform> bulletDirections;
        [SerializeField] protected float checkRadius,reloadTime;
        [SerializeField] private int bulletAmount = 1;
        
        private bool _canShoot = true;
        public static BaseEnemy target { get; set; }
        public GameObject weapon;

        public override void Activate(GameObject player)
        {
            if (GetComponent<PlayerMovementController>())
            {
               Player.autoWeaponController.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            var collider2Ds = Physics2D.OverlapCircleAll(transform.position, checkRadius);
            
            foreach (var col2D in collider2Ds)
            {
                if (col2D.TryGetComponent<BaseEnemy>(out var enemy))
                {
                    SetTarget(enemy);
                    ShotDirection(enemy); 
                    StartCoroutine(nameof(AutoPlayerShoot), reloadTime);
                }
                else
                {
                    StopCoroutine(nameof(AutoPlayerShoot));
                }
            }
        }

        private static void SetTarget(BaseEnemy targetEnemy)
        {
            target = targetEnemy;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }

        private void ShotDirection(BaseEnemy baseEnemy)
        {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), 
                    new Vector2(baseEnemy.transform.position.x, baseEnemy.transform.position.y)) < 
                Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                         new Vector2(target.transform.position.x, target.transform.position.y)))
            {
                target = baseEnemy;
            }

            var difference = target.transform.position - transform.position;

            var rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        }

        private void AutoPlayerShoot()
        {
            if (!_canShoot) return;

            SpawnBullets();
            StartCoroutine(CanShoot());
        }

        private void SpawnBullets()
        {
            for (var i = 0; i < bullets.Count; i++)
            {
                SetBulletDirection(bulletAmount);
            }
        }

        private void SetBulletDirection(int count)
        {
            bulletAmount = count;
            for (var i = 0; i < bulletAmount; i++)
            {
                Instantiate(bullets[0], bulletDirections[i].position, bulletDirections[i].rotation);
                bullets[0].SetActive(true);
                StartCoroutine(CanShoot());
            }
        }
        
        private IEnumerator CanShoot()
        {
            _canShoot = false;
            yield return new WaitForSeconds(reloadTime);
            _canShoot = true;
        }
    }
}