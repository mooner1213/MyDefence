using UnityEngine;

namespace MySample
{
    /// <summary>
    /// 회전 테스트 예제 스크립트
    /// </summary>
    public class RotateTest : MonoBehaviour
    {
        #region Variables
        // 화전 속도
        public float rotateSpeed = 5f;

        // 이동 속도
        public float moveSpeed = 1.0f;

        // 회전 값 변수
        // private float x = 0;

        // 목표 오브젝트 트랜스폼 인스턴스
        public Transform target;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            // Y축 회전하여 오른쪽 바라보기
            // this.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

            // X축 회전하여 아래 바라보기
            // this.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

            // Z축 회전하여 왼쪽 바라보기
            // this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }

        private void Update()
        {
            // 축 회전
            // x += 1;
            // this.transform.rotation = Quaternion.Euler(x, 0, 0); // x 축 기준으로 회전
            // this.transform.rotation = Quaternion.Euler(0, x, 0); // y 축 기준으로 회전
            // this.transform.rotation = Quaternion.Euler(0, 0, x); // z 축 기준으로 회전

            // [1] Rotate - 지구의 자전
            // this.transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed); // x 축 기준으로 회전
            // this.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed); // y 축 기준으로 회전
            // this.transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed); // z 축 기준으로 회전

            // [1-1] RotateAround - 지구의 공전
            // this.transform.RotateAround(target.position, Vector3.up, 20 * Time.deltaTime * rotateSpeed); // 목표 오브젝트를 중심으로 y 축 기준으로 회전

            /*
            // [2] 원하는(목표) 방향을 따라 회전
            // 목표 방향 구하기
            Vector3 dir = target.position - this.transform.position;

            // 목표 방향에 해당되는 회전값 구하기
            Quaternion lookRotation = Quaternion.LookRotation(dir);

            // 트랜스폼의 회전값을 구한 회전값에 대입
            // this.transform.rotation = lookRotation;

            // this.transform.rotation (0, 0, 0) => lookRotation (0, 41, 0)
            // Quaternion Lerp(Quaternion a, Quaternion b, float t);
            Quaternion qRotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
            // this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

            // Quaternion 으로부터 오일러 값(x, y, z) 구하기
            Vector3 euler = qRotation.eulerAngles;

            // y축 회전하는 회전값을 구한다.
            this.transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
            */


            // 이동 dir * Time.deltaTime speed
            Vector3 dir = target.position - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(dir); // 타겟을 바라보기

            /*
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Quaternion qRotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
            Vector3 euler = qRotation.eulerAngles;
            this.transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
            */

            transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.World);
            // transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
        }
        #endregion
    }
}

/*

a = 0, b = 10, t = 0.1
a = Lerp (a, b, t);

0 = Lerp (0, 10, 0.1);
a = 1, b = 10, t = 0.1 
 
1 = Lerp (1, 10, 0.1);
a = 1.9, b = 10, t = 0.1

1.9 = Lerp (1.9, 10, 0.1);
a = 2.71, b = 10, t = 0.1
 */