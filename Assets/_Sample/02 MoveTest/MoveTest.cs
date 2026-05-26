using UnityEngine;

namespace MySample
{
    public class MoveTest : MonoBehaviour
    {
        // 이동 목표지점 변수 선언 및 초기화
        private Vector3 targetPosition = new Vector3(10f, 1f, 10f);

        // 이동 목표 위치에 있는 오브젝트의 트랜스폼 인스턴스 생성
        public Transform target;
        public float speed = 10.0f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // this.gameObject, gameobject : MoveTest 스크립트가 붙어있는 게임 오브젝트의 객체(인스턴스)
            // this.gameObject.transform, gameObject.transform, this.transform, transform
            // : MoveTest 스크립트가 붙어있는 게임 오브젝트의 트랜스폼 객체(인스턴스)
            // this.transform.position = new Vector3 ( 10f, 1f, 10f);
            // this.gameObject.transform.position = new Vector3(10f, 1f, 10f);

            // this.transform.position = targetPosition;

            // Debug.Log($"타겟 위치: {target.position}");
            // this.transform.position = target.position;
        }

        // Update is called once per frame
        void Update()
        {
            // 플레이어의 위치를 앞으로 이동 : z축 값이 증가한다.
            // this.transform.position z축 값이 증가 - vector3 연산
            // this.transform.position.z = this.transform.position.z + 1.0f;
            // this.transform.position = this.transform.position + new Vector3(0f, 0f, 1f);

            // 앞, 뒤, 좌, 우, 위, 아래
            // this.transform.position += Vector3.forward; // 앞으로 이동
            // this.transform.position += Vector3.back; // 뒤로 이동
            // this.transform.position += Vector3.left; // 왼쪽으로 이동
            // this.transform.position += Vector3.right; // 오른쪽으로 이동
            // this.transform.position += Vector3.up; // 위로 이동
            // this.transform.position += Vector3.down; // 아래로 이동

            // Vector3.one = Vector3(1f, 1f, 1f) // 단위벡터
            // vector3.zero = Vector3(0f, 0f, 0f) // 초기값

            // 앞으로 이동
            // this.transform.position += new Vector3(0f, 0f, 1f) * Time.deltaTime * 10f;
            // transform.position += Vector3.forward * Time.deltaTime * speed;

            // 이동요소
            // 방향 : 이동할 방향 지정
            // Time.deltaTime : 동일한 시간에 동일한 거리를 이동하게 해줌
            // 속도 : 이동 속도 조절

            // Translate -> 주요 사용
            // this.transform.Translate(Vector3.forward * Time.deltaTime * speed);

            // 타겟 위치로 이동(방향, Time.deltaTime, speed)
            // dir.normalized : dir 방향으로 크기 1인 벡터, 단위벡터
            // dir.magnitude : dir 벡터의 크기, 길이
            // 이동 방향 구하기 : 목표지점 = 현재지점, 도착 예정위치 - 출발(현재) 위치
            Vector3 dir = target.position - this. transform.position;

            // this.transform.Translate(dir.normalized * Time.deltaTime * speed);
            // this.transform.Translate(dir.normalized * Time.deltaTime * speed, Space.Self);
            this.transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);

            // Space.Self, Space.World
            // this.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
            // this.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
    }
}

/*
 
n 프레임 : 초당 n번 실행(보여주기)
20 프레임 : 초당 20번 실행
20프레임이면 1 프레임당 걸리는 시간 -> 1초 / 20프레임 = 0.05초
 
Time.deltaTime : 실제 한 프레임에 걸리는 시간을 저장한 변수
 
 */