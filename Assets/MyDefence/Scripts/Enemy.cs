using UnityEngine;

namespace MyDefence
{
    public class Enemy : MonoBehaviour
    {
        public int hp = 100;
        private int killReward = 50;

        [Header("--- 이동 속도 설정 ---")]
        public float baseSpeed = 5f;        // 🏃 원래 적의 기본 속도
        public float currentSpeed;          // 👟 실시간으로 변하는 현재 속도

        [Header("--- 사망 이펙트 설정 ---")]
        public GameObject deathEffectPrefab;

        private float damageBuffer = 0f;
        private bool isUnderLaser = false;  // ⚡ 현재 레이저를 맞고 있는 중인가?

        void Start()
        {
            // 시작할 때는 현재 속도를 기본 속도로 세팅!
            currentSpeed = baseSpeed;
        }

        void Update()
        {
            // ❗[참고] 여기에 원래 적이 앞으로 이동하는 로직이 있을 거야.
            // 이동할 때 반드시 'baseSpeed' 대신 'currentSpeed'를 곱해서 움직이게 해줘!
            // 예: transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }

        // ⭐ 모든 Update가 끝나고 실행되는 LateUpdate에서 레이저 상태를 체크해 복구해줍니다.
        void LateUpdate()
        {
            if (!isUnderLaser)
            {
                // 이번 프레임에 레이저를 안 맞았다면 속도를 원래대로 복구!
                currentSpeed = baseSpeed;
            }

            // 스위치를 꺼두고, 다음 프레임에 레이저 타워가 다시 켜주길 기다립니다.
            isUnderLaser = false;
        }

        // [과제 3번] 레이저 타격하는 동안 Enemy의 속도 40% 감속 함수
        public void ApplyLaserEffects()
        {
            isUnderLaser = true; // 레이저 맞는 중이라고 스위치 ON!

            // 40% 감속이니까 원래 속도의 60%(0.6f)로 만들어버립니다.
            currentSpeed = baseSpeed * 0.6f;
        }

        // 일반 총알 / 미사일용 피격 함수
        public void TakeDamage(int damage)
        {
            hp -= damage;
            if (hp <= 0) Die();
        }

        // 레이저 타워용 지속 데미지 함수
        public void TakeDamageFloat(float damage)
        {
            damageBuffer += damage;

            if (damageBuffer >= 1f)
            {
                int intDamage = (int)damageBuffer;
                hp -= intDamage;
                damageBuffer -= intDamage;

                if (hp <= 0) Die();
            }
        }

        void Die()
        {
            GameData.money += killReward;
            Debug.Log($"적 처치! 50 Gold 획득! 현재 잔액: {GameData.money}");

            if (deathEffectPrefab != null)
            {
                GameObject effectGO = Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                Destroy(effectGO, 2f);
            }

            Destroy(gameObject);
        }
    }
}