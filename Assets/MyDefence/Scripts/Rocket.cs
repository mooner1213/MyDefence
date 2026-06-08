using UnityEngine;
namespace MyDefence
{

    public class Rocket : MonoBehaviour
    {
        [Header("--- 미사일 설정 ---")]
        public float speed = 50f;          // 이동 속도 50
        public float damageRange = 3.5f;   // 과제 조건 4번: 새로운 필드 - damageRange (반경 3.5)

        private Transform target;          // 런처가 넘겨줄 조준 타겟

        [Header("--- 이펙트 설정 ---")]
        public GameObject explosionPrefab; // 방금 만든 ExplosionEffect 프리팹을 담을 바구니

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

        // 💥 타겟에 도달했을 때 실행되는 함수
        void HitTarget()
        {
            Debug.Log("타격한다");

            // [신규 추가] 타격 지점에 폭발 이펙트 프리팹을 소환합니다!
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            Explode();
            Destroy(gameObject);
        }

        // 💣 폭발 범위 안의 모든 적을 처리하는 함수
        void Explode()
        {
            // 내 위치(미사일 충돌 지점)를 중심으로, 반지름이 damageRange(3.5)인 가상의 구체를 그려서 
            // 그 안에 닿은 모든 Collider(충돌체)들을 colliders 배열에 담습니다.
            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);

            // 발견된 충돌체들을 하나씩 검사합니다.
            foreach (Collider collider in colliders)
            {
                // 만약 부딪힌 물체의 태그가 "Enemy" 라면?
                if (collider.CompareTag("Enemy"))
                {
                    // 그 적을 파괴합니다. (과제 조건: 범위 내 모든 enemy들 데미지 입고 kill)
                    Destroy(collider.gameObject);
                }
            }
        }

        // 📐 과제 조건 5번: 데미지 범위(3.5)를 기즈모로 표시
        // 게임을 플레이하는 도중 미사일을 선택하면 씬(Scene) 뷰에 하얀색 원으로 폭발 범위가 보입니다.
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white; // 하얀색 선으로
            Gizmos.DrawWireSphere(transform.position, damageRange); // 내 위치 기준 반지름 3.5짜리 구체를 그림
        }
    }
}