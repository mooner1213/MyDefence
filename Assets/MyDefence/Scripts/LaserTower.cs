using UnityEngine;

namespace MyDefence
{
    public class LaserTower : MonoBehaviour
    {
        [Header("--- 로직 설정 ---")]
        public float attackRange = 12f;

        [Header("--- 연결할 프리팹 및 위치 ---")]
        public LineRenderer lineRenderer;
        public Transform firePoint;

        [Header("--- [과제 1번] 레이저 이펙트 및 조명 ---")]
        //public GameObject impactEffectPrefab; // 💥 레이저 타격 파티클 프리팹 (타격 지점에 생성)
        public Light laserLight;              // 💡 레이저용 조명 (타워 근처나 타격점에 불빛 주기)

        public GameObject spawnedImpactEffect; // 실시간으로 켜고 끌 파티클 오브젝트 바구니
        private Transform target = null;

        [Header("--- 회전 및 조준 설정 ---")]
        public Transform partToRotate;
        public float turnSpeed = 10f;

        [Header("--- 레이저 데미지 설정 ---")]
        // 과제 2번 조건: 1초당 30 데미지로 세팅!
        public float damagePerSecond = 30f;

        [Header("---레이저 파티클 설정---")]
        public ParticleSystem LaserEffect;

        void Start()
        {
            InvokeRepeating("UpdateTarget", 0f, 0.5f);

            // 시작할 때 타격 이펙트 프리팹이 있다면 미리 하나 복사해서 꺼두기 (실시간 생성/삭제 렉 방지!)
            /*if (impactEffectPrefab != null)
            {
                spawnedImpactEffect = Instantiate(impactEffectPrefab);
                spawnedImpactEffect.SetActive(false);
            }*/

            // 조명도 처음엔 꺼둡니다.
            if (laserLight != null) laserLight.enabled = false;
        }

        void Update()
        {
            // 만약 조준할 적이 없다면 모든 이펙트와 레이저를 끄고 리턴!
            if (target == null)
            {
                if (lineRenderer.enabled) lineRenderer.enabled = false;
                if (spawnedImpactEffect != null && spawnedImpactEffect.activeSelf) spawnedImpactEffect.SetActive(false);
                if (laserLight != null) laserLight.enabled = false;
                return;
            }

            // 🎯 적을 향해 고개 돌리기
            LockOn();

            // ⚡ 레이저 빔 및 조명 켜기
            if (!lineRenderer.enabled) lineRenderer.enabled = true;
            if (laserLight != null) laserLight.enabled = true;

            // 레이저 선 연결
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, target.position);

            // 💥 [과제 1번] 레이저 맞는데 맞는 방향에 맞게 파티클 위치, 방향 조정
            if (spawnedImpactEffect != null)
            {
                if (!spawnedImpactEffect.activeSelf) spawnedImpactEffect.SetActive(true);

                // 1. 위치를 적의 중심(혹은 피격 위치)으로 이동
                spawnedImpactEffect.transform.position = target.position;

                // 2. 튀는 방향 조절: 적에서 타워(총구)를 바라보는 방향으로 파티클이 튀도록 회전시킵니다.
                Vector3 lookDir = firePoint.position - target.position;
                if (lookDir != Vector3.zero)
                {
                    spawnedImpactEffect.transform.position += lookDir.normalized / 2;
                    spawnedImpactEffect.transform.rotation = Quaternion.LookRotation(lookDir);
                }
            }

            // 💡 [과제 1번] 레이저용 조명 위치 최적화 (타격 지점을 밝혀주면 이쁩니다)
            if (laserLight != null)
            {
                laserLight.transform.position = target.position + Vector3.up * 0.5f; // 적의 살짝 위쪽을 밝힘
            }

            // 🩸 [과제 2번 & 3번] 지속 데미지 및 감속 적용
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 매 프레임 데미지 전달 (Time.deltaTime을 곱해 1초에 딱 30씩 깎이게 함)
                enemy.TakeDamageFloat(damagePerSecond * Time.deltaTime);

                // 실시간 40% 감속 효과 부여
                enemy.ApplyLaserEffects();
            }
        }

        void UpdateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= attackRange)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }

        void LockOn()
        {
            if (partToRotate == null) return;
            Vector3 dir = target.position - partToRotate.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Slerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(rotation);
        }
    }
}