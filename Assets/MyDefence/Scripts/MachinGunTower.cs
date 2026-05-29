using UnityEngine;

namespace MyDefence
{
    public class MachinGunTower : MonoBehaviour
    {
        #region Variables
        [Header("타워 설정")]
        public float rotateSpeed = 5f; // 회전 속도
        public float attackRange = 7f; // 사정거리

        public Transform head; // 회전할 머리 (PartToRotate)
        private Transform enemy; // 조준 중인 적
        public float angleOffset = 0f; // 방향 보정값 (0으로 고정!)

        [Header("슈팅 설정")]
        public GameObject bulletPrefab; // 발사할 탄환 프리팹을 담을 바구니 (과제 2-2)
        public Transform firePoint;     // 총알이 복사되어 나올 총구 위치 (과제 2-1)
        public float fireRate = 1f;     // 1초에 몇 번 쏠 것인가? (과제 1: 1초마다 1발)
        private float fireCountdown = 0f; // 다음 발사까지 남은 시간을 재는 모래시계 스톱워치
        #endregion

        #region Unity Event Method
        private void Update()
        {
            // 매 프레임마다 가장 가까운 적을 타겟으로 잡습니다.
            UpdateTarget();

            // 조준할 적과 머리가 모두 존재할 때만 실행합니다.
            if (enemy != null && head != null)
            {
                // 적을 바라보는 회전 로직 구역입니다.
                Vector3 dir = enemy.position - head.position; // 방향 계산
                dir.y = 0f; // Y축 고정

                if (dir != Vector3.zero) // 방향이 정상일 때만 회전
                {
                    Quaternion lookEnemy = Quaternion.LookRotation(dir); // 목표 회전값
                    Quaternion offsetRotation = Quaternion.Euler(0f, angleOffset, 0f); // 보정 각도
                    Quaternion finalRotation = lookEnemy * offsetRotation; // 최종 회전값
                    head.rotation = Quaternion.Lerp(head.rotation, finalRotation, Time.deltaTime * rotateSpeed); // 부드러운 회전
                }

                // ---- [여기서부터 슈팅 타이머 로직 시작!] ----
                if (fireCountdown <= 0f) // 만약 모래시계의 모래가 다 떨어졌다면? (0초 이하가 되었다면)
                {
                    Shoot(); // 총을 쏘는 함수를 실행합니다! (과제 1, 2)
                    fireCountdown = 1f / fireRate; // 모래시계를 다시 1초로 가득 채워줍니다.
                }

                fireCountdown -= Time.deltaTime; // 모래시계에서 매 프레임마다 흐른 시간만큼 모래를 빼줍니다.
            }
        }

        // 총을 발사하여 총알을 생성하는 함수입니다.
        private void Shoot()
        {
            Debug.Log("Shoot!!!!!"); // 과제 1번 조건: 콘솔창에 로그 찍기

            // 과제 2-3번 조건: Instantiate를 이용해 총구(firePoint)의 위치와 회전값으로 총알 복사본을 생성합니다!
            GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // 방금 만든 총알 복사본에서 'Bullet' 스크립트 컴포넌트를 쏙 꺼내옵니다.
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null) // 총알 스크립트가 성공적으로 꺼내졌다면?
            {
                bullet.Seek(enemy); // 총알에게 "너는 저 적(enemy)을 향해 날아가라!" 하고 타겟을 지정해 줍니다.
            }
        }

        // 최단 거리 적 타겟팅 함수
        private void UpdateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // 맵의 모든 적 검색
            float shortestDistance = Mathf.Infinity; // 최단 거리 초기화
            GameObject nearestEnemy = null; // 가장 가까운 적 바구니

            foreach (GameObject currentEnemy in enemies) // 적들을 한 마리씩 검사
            {
                Vector3 myPos = this.transform.position; // 내 위치
                Vector3 enemyPos = currentEnemy.transform.position; // 적 위치
                myPos.y = 0f; enemyPos.y = 0f; // Y축 무시

                float distanceToEnemy = Vector3.Distance(myPos, enemyPos); // 거리 계산
                if (distanceToEnemy < shortestDistance) // 더 가까운 적 발견 시
                {
                    shortestDistance = distanceToEnemy; // 거리 갱신
                    nearestEnemy = currentEnemy; // 적 갱신
                }
            }

            if (nearestEnemy != null && shortestDistance <= attackRange) // 사정거리 안이라면
            {
                enemy = nearestEnemy.transform; // 타겟 지정
            }
            else
            {
                enemy = null; // 타겟 해제
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red; // 빨간색 설정
            Gizmos.DrawWireSphere(this.transform.position, attackRange); // 범위 그리기
        }
        #endregion
    }
}