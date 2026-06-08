using System.Reflection;
using UnityEngine;
namespace MyDefence
{

    public class RocketTower : MonoBehaviour
    {
        [Header("--- 로직 설정 ---")]
        public float attackRange = 12f;      // 과제 조건: 공격 범위 12
        public float fireRate = 4f;         // 과제 조건: 4초에 1회 발사

        [Header("--- 연결할 프리팹 및 위치 ---")]
        public GameObject missilePrefab;    // 발사할 미사일 프리팹을 담는 바구니
        public Transform firePoint;         // 미사일이 날아기작할 발사구 위치 (유니티에서 인스펙터로 연결)

        private float fireCountdown = 0f;   // 다음 발사까지 남은 시간을 계산하는 타이머
        private Transform target = null;    // 현재 조준하고 있는 적(타겟)의 위치

        [Header("--- 회전 및 조준 설정 ---")]
        public Transform partToRotate;     // ⭐ 유니티에서 회전시킬 타워의 상체(목) 오브젝트를 연결할 바구니
        public float turnSpeed = 10f;      // 고개가 돌아가는 속도 (클수록 빨라져요)

        [Header("--- 이펙트 설정 ---")]
        public GameObject muzzleFlashPrefab; // 로켓 발사 파티클 프리팹 바구니

        void Start()
        {
            // 게임이 시작되면 0초 후부터 0.5초마다 주기적으로 가장 가까운 적을 찾는 함수를 실행합니다.
            // 매 프레임(Update)마다 적을 찾으면 컴퓨터가 너무 힘들어하기 때문이에요!
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }

        void Update()
        {
            // 만약 조준할 적이 없다면 아래 로직을 타지 않고 패스합니다.
            if (target == null) return;

            // 🎯 [신규 추가] 적을 향해 고개 돌리기 (LockOn 기능)
            LockOn();

            // --- 기존 발사 타이머 로직 ---
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }

        // 🎯 가장 가까운 적을 찾는 함수 (비유: 레이더 돌리기)
        void UpdateTarget()
        {
            // "Enemy"라는 태그를 가진 게임 오브젝트들을 모두 찾아 배열(리스트)에 넣습니다.
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float shortestDistance = Mathf.Infinity; // 가장 가까운 거리를 저장할 변수 (처음엔 무한대로 설정)
            GameObject nearestEnemy = null;          // 가장 가까운 적을 저장할 변수

            // 화면에 있는 모든 적을 하나씩 검사합니다.
            foreach (GameObject enemy in enemies)
            {
                // 타워와 적 사이의 거리를 계산합니다.
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                // 만약 방금 계산한 거리가 기존에 알고 있던 가장 가까운 거리보다 더 가깝다면?
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy; // 가장 가까운 거리를 갱신하고
                    nearestEnemy = enemy;               // 그 적을 가장 가까운 적이라고 기억합니다.
                }
            }

            // 가장 가까운 적이 있고, 그 적이 사정거리(12) 안에 있다면 타겟으로 지정합니다.
            if (nearestEnemy != null && shortestDistance <= attackRange)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null; // 사정거리를 벗어났거나 적이 없다면 타겟을 비웁니다.
            }
        }

        // 🚀 미사일을 실제로 발사하는 함수
        void Shoot()
        {
            Debug.Log("Shoot!!!!");

            // 🔥 [수정] 발사할 때 로켓 전용 파티클 이펙트를 소환합니다!
            if (muzzleFlashPrefab != null && firePoint != null)
            {
                Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            }

            // 미사일 생성 로직 (기존과 동일)
            if (missilePrefab != null && firePoint != null)
            {
                GameObject missileGO = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
                Rocket missile = missileGO.GetComponent<Rocket>();
                if (missile != null)
                {
                    missile.Setup(target);
                }
            }
        }

        // 🔄 적을 부드럽게 바라보는 함수
        void LockOn()
        {
            if (partToRotate == null) return;

            // 1. 내(타워) 위치에서 적(target) 위치를 바라보는 방향 벡터를 구합니다.
            Vector3 dir = target.position - partToRotate.position;

            // 2. 그 방향을 바라보는 유니티 전용 회전값(Quaternion)을 계산합니다.
            Quaternion lookRotation = Quaternion.LookRotation(dir);

            // 3. Quaternion.Slerp를 사용해 현재 회전 상태에서 적을 바라보는 회전 상태까지 
            // turnSpeed의 속도로 매 프레임 '스르륵' 부드럽게 보간(중간값을 계산)해 줍니다.
            Vector3 rotation = Quaternion.Slerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

            // 4. 계산된 회전값을 타워의 상체(partToRotate)에 적용합니다.
            partToRotate.rotation = Quaternion.Euler(rotation);
        }

        // 📐 유니티 에디터 화면(Scene)에서 공격 범위를 시각적으로 보여주는 보너스 기능!
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red; // 빨간색 선으로
            Gizmos.DrawWireSphere(transform.position, attackRange); // 내 위치 중심으로 반지름 12짜리 원을 그립니다.
        }
    }
}