using System.Collections;
using UnityEngine;
using TMPro;
namespace MyDefence
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("스폰 설정")]
        public GameObject enemyPrefab;
        public Transform spawnPoint;

        [Header("UI 설정")]
        public TextMeshProUGUI timerText;

        private int waveCount = 1;
        private float spawnInterval = 5f;

        void Start()
        {
            StartCoroutine(WaveSystemRoutine());
        }

        IEnumerator WaveSystemRoutine()
        {
            while (true)
            {
                // 1. 5초 카운트다운 진행
                float remainingTime = spawnInterval;
                while (remainingTime > 0)
                {
                    timerText.text = "Next Wave in: " + remainingTime.ToString("F0") + "s";
                    yield return new WaitForSeconds(0.1f);
                    remainingTime -= 0.1f;
                }

                timerText.text = "WAVE START!";

                // 2. [핵심 수정 부분] 현재 웨이브 마리 수만큼 적을 소환하되, 시간차를 둡니다!
                for (int i = 0; i < waveCount; i++)
                {
                    SpawnEnemy(); // 일단 한 마리 소환!

                    // 💡 만약 다음 소환할 적이 더 남아있다면, 아주 잠깐 쉬어갑니다.
                    // 0.8초 뒤에 다음 녀석이 나오게 설정했어요. 이 숫자를 조절하면 간격을 바꾸실 수 있어요!
                    if (i < waveCount - 1)
                    {
                        yield return new WaitForSeconds(0.8f);
                    }
                }

                // 3. 다음 웨이브 준비
                waveCount++;

                // 적들이 다 나오는 걸 보고 다음 카운트다운으로 넘어가기 위해 1초 대기
                yield return new WaitForSeconds(1f);
            }
        }

        // 적을 고정된 위치에 소환하는 함수 (원래대로 돌려놓았어요!)
        void SpawnEnemy()
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}