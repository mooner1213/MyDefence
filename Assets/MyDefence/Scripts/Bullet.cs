using UnityEngine;

namespace MyDefence
{
    public class Bullet : MonoBehaviour
    {
        #region Variables
        private Transform target;
        public float speed = 70f;

        public GameObject hitEffectPrefab;

        // 1-2) 탄환에 공격력(attack) : 50 초기화
        public int attack = 50;
        #endregion

        #region Unity Event Method
        public void Seek(Transform _target)
        {
            target = _target;
        }

        private void Update()
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
        }

        private void HitTarget()
        {
            Debug.Log("Hit Target!!!");

            if (hitEffectPrefab != null)
            {
                GameObject deathEffectGO = Instantiate(hitEffectPrefab, target.position, target.rotation);
                Destroy(deathEffectGO, 2f);
            }

            // 🛠️ [수정] 적을 바로 삭제하지 않고, 데미지를 50 깎아줍니다!
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attack); // 에너미의 HP를 50 감소시킴!
            }

            Destroy(gameObject);        // 총알 자신은 파괴
        }
        #endregion
    }
}