using UnityEngine;

namespace MyDefence
{
    public class Rocket : MonoBehaviour
    {
        [Header("--- 미사일 설정 ---")]
        public float speed = 50f;
        public float damageRange = 3.5f;

        // 1-2) 미사일에 공격력(attack) : 50 초기화
        public int attack = 50;

        private Transform target;

        [Header("--- 이펙트 설정 ---")]
        public GameObject explosionPrefab;

        public void Setup(Transform attackTarget)
        {
            target = attackTarget;
        }

        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(target);
        }

        void HitTarget()
        {
            Debug.Log("타격한다");

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            Explode();
            Destroy(gameObject);
        }

        void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    // 🛠️ [수정] 범위 내의 적들을 즉사시키지 않고, 데미지를 50씩 줍니다!
                    Enemy enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(attack); // 범위 내 모든 적 HP 50 감소!
                    }
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, damageRange);
        }
    }
}