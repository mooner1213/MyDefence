using UnityEngine;

namespace MyDefence
{
    public class Bullet : MonoBehaviour
    {
        #region Variables
        private Transform target; // 타워가 지정해 준 날아갈 목표(적)의 위치 바구니 (과제 3-1)
        public float speed = 70f; // 탄환의 날아가는 이동 속도 (과제 3-2 조건: 70)
        #endregion

        #region Unity Event Method
        // 타워가 총알을 만들고 "이 적을 쫓아가라"고 강제로 명할 때 실행되는 함수입니다.
        public void Seek(Transform _target)
        {
            target = _target; // 받아온 목표를 내 바구니에 저장합니다.
        }

        private void Update()
        {
            if (target == null) // 만약 날아가는 도중에 적이 이미 죽어서 사라졌다면?
            {
                Destroy(gameObject); // 총알 자신도 허공에서 스스로 파괴(Kill)하고 끝냅니다.
                return; // 아래 코드를 실행하지 않고 멈춥니다.
            }

            // 과제 3-2번: 적을 향해 이동하기 로직
            Vector3 dir = target.position - transform.position; // 적이 있는 방향 계산 (적 위치 - 내 위치)
            float distanceThisFrame = speed * Time.deltaTime; // 이번 프레임에 총알이 움직여야 할 실제 거리 계산

            // 과제 3-3번: 남은 거리를 비교하여 타겟 충돌 체크 (남은 거리가 한 프레임 이동 거리보다 작으면 명중!)
            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget(); // 명중 처리 함수를 실행합니다!
                return; // 명중했으니 아래 이동 코드는 무시하고 돌아갑니다.
            }

            // 아직 명중 안 했다면? 적을 향해 속도 70으로 똑바로 날아갑니다. (Space.World 기준 평면 이동)
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

        // 적에게 완벽하게 부딪혔을 때 실행되는 함수입니다.
        private void HitTarget()
        {
            Debug.Log("Hit Target!!!"); // 과제 3-3 조건: 콘솔창 로그 찍기

            Destroy(target.gameObject); // 과제 3-3 조건: 타겟 게임 오브젝트를 파괴합니다 (Kill).
            Destroy(gameObject);        // 과제 3-3 조건: 탄환 자신도 파괴합니다 (Kill).
        }
        #endregion
    }
}