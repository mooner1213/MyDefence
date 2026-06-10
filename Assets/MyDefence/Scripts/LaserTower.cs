using UnityEngine;

namespace MyDefence
{
    public class LaserTower : MonoBehaviour
    {
        [Header("--- 로직 설정 ---")]
        // 로켓 타워랑 똑같이 사정거리를 'attackRange'라는 이름으로 맞춰줄게!
        public float attackRange = 12f;

        [Header("--- 연결할 프리팹 및 위치 ---")]
        public LineRenderer lineRenderer;
        public Transform firePoint;

        private Transform target = null;    // 현재 조준하고 있는 적(타겟)의 위치

        [Header("--- 회전 및 조준 설정 ---")]
        public Transform partToRotate;     // ⭐ 유니티에서 회전시킬 타워의 상체(목) 오브젝트
        public float turnSpeed = 10f;

        [Header("--- 레이저 데미지 설정 ---")]
        public float damagePerSecond = 50f;

        void Start()
        {
            // 🤖 [로켓 타워 복사] 0초 후부터 0.5초마다 주기적으로 적을 찾음!
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }

        void Update()
        {
            // 만약 조준할 적이 없다면 레이저 끄고 리턴!
            if (target == null)
            {
                if (lineRenderer.enabled)
                    lineRenderer.enabled = false;
                return;
            }

            // 🎯 [로켓 타워 복사] 적을 향해 고개 돌리기 (LockOn)
            LockOn();

            // ⚡ [레이저 빔 실시간 연결]
            if (!lineRenderer.enabled)
                lineRenderer.enabled = true;

            // 🛠️ 월드 좌표계 기준으로 깔끔하게 두 줄로 변경!
            lineRenderer.SetPosition(0, firePoint.position); // 시작점은 총구 위치!
            lineRenderer.SetPosition(1, target.position);    // 끝점은 적 위치!

            // 🩸 지속 데미지 주기
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamageFloat(damagePerSecond * Time.deltaTime);
            }
        }

        // 🎯 [로켓 타워 복사] 가장 가까운 적을 찾는 함수 (레이더 돌리기)
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

        // 🔄 [로켓 타워 복사] 적을 부드럽게 바라보는 함수 (순수 똑같이 이식!)
        void LockOn()
        {
            if (partToRotate == null) return;

            // 1. 방향 벡터 구하기
            Vector3 dir = target.position - partToRotate.position;

            // 2. Quaternion 회전값 계산
            Quaternion lookRotation = Quaternion.LookRotation(dir);

            // 3. 💥 로켓 타워 고유의 Slerp -> eulerAngles 변환 필터링 방식 그대로 적용!
            Vector3 rotation = Quaternion.Slerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

            // 4. 계산된 회전값을 타워의 상체에 적용
            partToRotate.rotation = Quaternion.Euler(rotation);
        }
    }
}