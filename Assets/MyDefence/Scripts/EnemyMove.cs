using UnityEngine;

namespace MyDefence
{
    public class EnemyMove : MonoBehaviour
    {
        // 이동할 목표(종점)의 위치를 저장할 변수야.
        private Transform targetTransform;

        // 이동 속도 (숫자를 바꾸면 더 빨라지거나 느려져!)
        public float speed = 3.0f;

        void Start()
        {
            // [핵심] 게임이 시작되면 "End"라는 이름을 가진 오브젝트를 하이어라키에서 스스로 찾아내라!
            GameObject endObject = GameObject.Find("End");

            if (endObject != null)
            {
                // 찾았다면, 그 오브젝트의 위치(Transform)를 우리 목적지로 설정하자!
                targetTransform = endObject.transform;
            }
            else
            {
                // 혹시 "End"라는 오브젝트를 못 찾으면 콘솔창에 경고를 띄워줘.
                Debug.LogError("맵에 'End'라는 이름을 가진 오브젝트가 없어요! 확인해 주세요.");
            }
        }

        void Update()
        {
            // 목적지(targetTransform)가 정상적으로 설정되어 있을 때만 움직이자!
            if (targetTransform != null)
            {
                // 1. 현재 내 위치에서 End 위치를 바라보는 방향 구하기
                Vector3 direction = targetTransform.position - transform.position;

                // Y축(높이) 때문에 오브젝트가 땅으로 파묻히거나 공중으로 뜨는 걸 막기 위해 Y축 값은 0으로 고정해줄게!
                direction.y = 0;

                // 2. 종점까지 남은 거리 계산하기
                float distance = direction.magnitude;

                // 3. 도착 판정 (남은 거리가 0.2미터 이하로 좁혀지면 도착!)
                if (distance < 0.2f)
                {
                    Debug.Log("종점 도착!!!!");

                    // 나 자신(Enemy)을 파괴해서 없애기
                    Destroy(gameObject);
                }
                else
                {
                    // 4. 아직 도착 안 했으면 목적지 방향으로 부드럽게 이동하기
                    // .normalized는 방향만 남기는 순수한 나침반 화살표 역할을 해줘.
                    transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
                }
            }
        }
    }
}