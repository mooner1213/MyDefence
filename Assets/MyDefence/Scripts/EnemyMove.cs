using UnityEngine;

namespace MyDefence
{
    public class EnemyMove : MonoBehaviour
    {
        private Transform targetTransform;

        // 🛑 [기존 speed 제거 혹은 기본값 주석] 
        // 이제 Enemy 스크립트의 속도를 받아올 거라 이 스크립트 고유의 speed는 안 써도 됨!

        // 🤝 같이 붙어있는 Enemy 스크립트를 참조할 바구니
        private Enemy enemyComponent;

        void Start()
        {
            // 1. 같은 오브젝트에 붙어있는 Enemy 스크립트를 가져옵니다.
            enemyComponent = GetComponent<Enemy>();

            GameObject endObject = GameObject.Find("End");

            if (endObject != null)
            {
                targetTransform = endObject.transform;
            }
            else
            {
                Debug.LogError("맵에 'End'라는 이름을 가진 오브젝트가 없어요! 확인해 주세요.");
            }
        }

        void Update()
        {
            if (targetTransform != null)
            {
                Vector3 direction = targetTransform.position - transform.position;
                direction.y = 0;

                float distance = direction.magnitude;

                if (distance < 0.2f)
                {
                    // [추가] 도착 시 라이프 1 감소
                    GameData.lives--;

                    Debug.Log("종점 도착! 라이프가 깎였습니다.");
                    Destroy(gameObject);
                }
                else
                {
                    // 🏃‍♂️ [핵심 수정] 기존의 speed 대신, enemyComponent.currentSpeed를 사용합니다!
                    // 만약 Enemy 스크립트를 못 찾았다면 기본값 5.0f로 예외 처리까지 완벽하게!
                    float moveSpeed = (enemyComponent != null) ? enemyComponent.currentSpeed : 5.0f;

                    // 이제 실시간으로 변하는 moveSpeed(40% 감속 반영됨)로 이동해!
                    transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);
                }
            }
        }
    }
}