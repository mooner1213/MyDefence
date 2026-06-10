using UnityEngine;

namespace MyDefence
{
    public class Enemy : MonoBehaviour
    {
        // 1-1) Enemy에게 체력(hp) : 100 초기화
        public int hp = 100;
        private int killReward = 50;

        [Header("--- 사망 이펙트 설정 ---")]
        // ⭐ 형이 미리 만들어둔 '부서져서 떨어지는 파티클 프리팹'을 담을 바구니!
        public GameObject deathEffectPrefab;

        // 레이저 전용 지속 데미지 버퍼
        private float damageBuffer = 0f;

        // 일반 총알 / 미사일용 피격 함수
        public void TakeDamage(int damage)
        {
            hp -= damage;

            if (hp <= 0)
            {
                Die();
            }
        }

        // 레이저 타워용 지속 데미지 함수 (렉 유발 디버그 로그 완벽 제거!)
        public void TakeDamageFloat(float damage)
        {
            damageBuffer += damage;

            if (damageBuffer >= 1f)
            {
                int intDamage = (int)damageBuffer;
                hp -= intDamage;
                damageBuffer -= intDamage;

                if (hp <= 0)
                {
                    Die();
                }
            }
        }

        // 💀 적이 완전히 죽을 때 실행되는 함수
        void Die()
        {
            // 1-3) kill 하면 리워드로 50 Gold 지급
            GameData.money += killReward;
            Debug.Log($"적 처치! 50 Gold 획득! 현재 잔액: {GameData.money}");

            // 💥 [과제 조건 적용] 죽는 순간 부서지는 파티클 이펙트 생성!
            if (deathEffectPrefab != null)
            {
                // 적이 죽은 바로 그 위치(transform.position)에 이펙트를 소환합니다.
                GameObject effectGO = Instantiate(deathEffectPrefab, transform.position, transform.rotation);

                // 생성된 파티클 이펙트는 2초 뒤에 하이러키 창에서 깔끔하게 자동 삭제! (렉 방지)
                Destroy(effectGO, 2f);
            }

            // enemy kill (Destroy)
            Destroy(gameObject);
        }
    }
}