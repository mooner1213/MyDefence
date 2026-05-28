using UnityEngine;
using System.Collections;

namespace MySample
{
    public class PrefabTest : MonoBehaviour
    {
        #region Variables
        // 생성할 프리펩 게임 오브젝트 가져오기
        public GameObject prefab;

        // 타이머 변수 선언 - 2개(누적 변수, 간격 변수)
        // private float countdown = 0f; // 시간(Time.deltaTime) 누적 변수
        public float tileTimer = 1f; // 타이머 기준 시간
        #endregion

        #region Unity Ebent Method
        private void Start()
        {
            // [1] 프리펩 게임 오브젝트의 사본 만들기
            // Instantiate(prefab);

            // [2] 지정된 위치(5, 0.05, 8)에 프리펩 게임오브젝트의 사본 만들기
            // Vector3 Position = new Vector3(5, 0.05f, 8);
            // Instantiate(prefab, Position, Quaternion.identity) ;

            // [3] 맵타일 찍기 (가로 10개 * 세로 10개, 타일간 간격은 5)
            /*for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Vector3 tilePosition = new Vector3(i * 5, 0, j * 5);
                    Instantiate(prefab, tilePosition, Quaternion.identity);
                }
            }*/

            // GenerateMapTile(10, 10, this.transform); // 부모 지정

            // [4] 랜덤위치에 타일 10개 찍기
            /*for(int i = 0; i <= 10; i++)
            {
                GenerateRandomMaptile();
            }*/

            // [6] 랜덤위치에 타일을 1초마다 1개씩 찍기 - 10개까지만
            // 타일 1개 찍고 1초 딜레이 -> 타일 1개 찍고 1초 딜레이 ... -> 타일 10개 찍고 종료
            // 코루틴 함수 이용하기 : StartCoroutine(코루틴함수이름());
            Debug.Log("[0] 코루틴 함수 시작");
            StartCoroutine(CreateMapTile());
            Debug.Log("[4] 타일 생성 완료");
        }

        private void Update()
        {
            // [5] 랜덤한 위치에 타일을 1초마다 1개씩 찍기
            // 1초 타이머
            /*countdown += Time.deltaTime; // 매 프레임마다 시간 누적
            if (countdown >= tileTimer)
            {
                // 타이머 기능 실행 - 타일 찍기
                GenerateRandomMaptile();
                // 타이머 초기화
                countdown = 0f;*/

            /*if (countdown <= 0f)
            {
                GenerateRandomMaptile(); // 타일 찍기
                countdown = tileTimer; // 타이머 초기화
            }
            countdown -= Time.deltaTime;*/
        }
        #endregion

        #region Custom Method
        // 코루틴 함수 - 맵타일 찍기
        IEnumerator CreateMapTile()
        {
            /*// 랜덤타일 찍기
            GenerateRandomMaptile();
            Debug.Log("[1] 첫번째 타일 생성");
            // 지연시키기 - 1초
            yield return new WaitForSeconds(1.0f);

            GenerateRandomMaptile();
            Debug.Log("[2] 두번째 타일 생성");

            yield return new WaitForSeconds(1.0f);

            GenerateRandomMaptile();
            Debug.Log("[3] 세번째 타일 생성");*/

            for (int i = 0; i < 10; i++)
            {
                GenerateRandomMaptile();
                Debug.Log($"{i + 1}번째 타일 생성");
                yield return new WaitForSeconds(1.0f);
            }
        }
        // 매개변수로 가로, 세로 타일 갯수를 입력받아 맵타일 찍는 함수
        public void GenerateMapTile(int row, int column, Transform parent)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    // 생성하면서 위치 지정
                    // Vector3 tilePosition = new Vector3(j * 5f, 0.05f, i * -5f);
                    // Instantiate(prefab, tilePosition, Quaternion.identity, parent);

                    GameObject go = Instantiate(prefab, parent);
                    go.transform.position = new Vector3(j * 5f, 0.05f, i * -5f);
                }
            }
        }

        // 10 * 10 맵타일 중 랜덤한 타일 하나 찍기
        void GenerateRandomMaptile()
        {
            int randRow = Random.Range(0, 10);
            int randColumn = Random.Range(0, 10);

            Vector3 position = new Vector3(randColumn * 5f, 0.05f, randRow * -5f);
            Instantiate(prefab, position, Quaternion.identity);
        }
        #endregion
    }
}