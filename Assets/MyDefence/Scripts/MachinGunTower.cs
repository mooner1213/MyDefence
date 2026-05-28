using UnityEngine;

namespace MyDefence
{
    public class MachinGunTower : MonoBehaviour
    {
        #region Variables
        [Header("타워 설정")]
        public float rotateSpeed = 5f;
        public float attackRange = 7f;

        // 실제로 회전시킬 타워의 머리 부분 (PartToRotate)
        public Transform head;

        // [중요 수정] 이제 이 변수는 인스펙터에서 넣지 않습니다. 코드로만 관리합니다.
        // 타겟(적)의 위치 정보
        private Transform enemy;

        [Header("방향 보정 (필요시 조절)")]
        // 타워 모델의 축이 꺾여있을 때 사용 (이전 단계에서 썼던 것)
        public float angleOffset = 90f;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            // 1. 타겟이 없다면? 계속 적을 찾습니다.
            if (enemy == null)
            {
                // "Enemy" 태그를 가진 오브젝트를 찾습니다.
                GameObject targetEnemy = GameObject.FindWithTag("Enemy");

                // [확실한 예외 처리] 적을 '진짜로' 찾았을 때만 enemy 변수에 넣어줍니다!
                if (targetEnemy != null)
                {
                    enemy = targetEnemy.transform;
                }
            }

            // 2. 타겟이 진짜로 존재하고, 회전할 머리가 있다면! 조준을 시작합니다.
            // (이 안의 코드는 enemy가 null이 아닐 때만 실행되므로 안전합니다!)
            if (enemy != null && head != null)
            {
                Vector3 myPos = this.transform.position;
                Vector3 enemyPos = enemy.position;

                // 평면 거리 계산
                myPos.y = 0f;
                enemyPos.y = 0f;
                float flatDistance = Vector3.Distance(myPos, enemyPos);

                if (flatDistance <= attackRange)
                {
                    // 적을 향한 방향을 구합니다.
                    Vector3 dir = enemy.position - head.position;

                    // 수평 회전만 하도록 Y축은 0으로 고정!
                    dir.y = 0f;

                    // 방향이 0이 아닐 때만 회전!
                    if (dir != Vector3.zero)
                    {
                        // 기본적으로 적을 바라보는 회전값
                        Quaternion lookEnemy = Quaternion.LookRotation(dir);

                        // [보정] 모델이 돌아가 있는 만큼 Y축 각도를 강제로 더해줍니다!
                        Quaternion offsetRotation = Quaternion.Euler(0f, angleOffset, 0f);
                        Quaternion finalRotation = lookEnemy * offsetRotation;

                        // 머리(head)를 최종 보정된 각도로 부드럽게 돌립니다.
                        head.rotation = Quaternion.Lerp(head.rotation, finalRotation, Time.deltaTime * rotateSpeed);
                    }
                }
                else
                {
                    // 범위를 벗어나면 타겟을 해제합니다.
                    enemy = null;
                }
            }
        }
    }
}
        #endregion