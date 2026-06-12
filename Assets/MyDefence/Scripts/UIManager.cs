using UnityEngine;
using TMPro;              // TextMeshProUGUI를 쓰기 위한 선언
using UnityEngine.SceneManagement; // 씬 전환(LoadScene)을 위한 선언

namespace MyDefence
{
    public class UIManager : MonoBehaviour
    {
        [Header("--- HUD (TextMeshPro) ---")]
        public TextMeshProUGUI moneyText;   // 소지금 텍스트 연결칸
        public TextMeshProUGUI livesText;   // 라이프 텍스트 연결칸

        [Header("--- GAME OVER ---")]
        public GameObject gameOverUI;       // 게임오버 패널 오브젝트
        public TextMeshProUGUI roundsText;  // 몇 라운드 생존했는지 출력할 텍스트

        [Header("--- PAUSE ---")]
        public GameObject pauseUI;          // 일시정지 패널 오브젝트
        private bool isPaused = false;      // 현재 일시정지 상태인지 체크하는 스위치

        void Start()
        {
            // 🎯 [완벽 해결] 씬이 다시 시작될 때, 멈춰있던 데이터들을 초기값으로 리셋해줍니다!
            GameData.lives = 10;          // 라이프를 다시 10개로 만선!
            GameData.money = 1000;        // 돈도 처음 액수로 복구!
            GameData.roundsSurvived = 0;  // 라운드도 0부터 다시 시작!

            // 게임 시작 시 모든 팝업 UI창은 꺼둡니다. (기존 코드)
            if (gameOverUI != null) gameOverUI.SetActive(false);
            if (pauseUI != null) pauseUI.SetActive(false);

            Time.timeScale = 1f; // 시간 정상화 (기존 코드)
        }

        void Update()
        {
            // 1. HUD 텍스트 갱신 (기존 코드)
            if (moneyText != null) moneyText.text = $"{GameData.money}";
            if (livesText != null) livesText.text = $"{GameData.lives}";

            // 2. 라이프 0 이하 시 자동 게임오버 (기존 코드)
            if (GameData.lives <= 0 && gameOverUI.activeSelf == false)
            {
                TriggerGameOver();
            }

            // 3. 치트키 및 ESC 로직 (기존 코드)
            if (Input.GetKeyDown(KeyCode.O)) TriggerGameOver();
            if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();

            // 4. [검증용 임시 추가] 키보드 'I'를 누르면 라운드(웨이브) 숫자가 1씩 증가!
            if (Input.GetKeyDown(KeyCode.I))
            {
                GameData.roundsSurvived++;
                Debug.Log($"현재 라운드 증가: {GameData.roundsSurvived}");
            }
        }

        // 게임오버를 발동시키는 함수
        public void TriggerGameOver()
        {
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
                Time.timeScale = 0f; // 게임 세상 일시정지
            }

            // 🎯 [완벽 복구] 라이프가 0이 되어 게임오버가 된 '그 타이밍'의 실제 웨이브 숫자를 UI에 적용!
            if (roundsText != null)
            {
                // 이제 "현재 웨이브 숫자 + ROUNDS SURVIVED" 로 깔끔하게 한 줄로 출력됨!
                roundsText.text = $"{GameData.roundsSurvived} ROUNDS SURVIVED";
            }
        }

        // 일시정지를 켰다 껐다 하는 함수
        public void TogglePause()
        {
            // 🎯 [핵심 추가] 만약 게임오버 UI가 이미 켜져 있는 상태라면?
            if (gameOverUI != null && gameOverUI.activeSelf == true)
            {
                return; // 아래 일시정지 로직을 실행하지 않고 즉시 함수를 빠져나갑니다!
            }

            // 기존 일시정지 로직
            isPaused = !isPaused;
            if (pauseUI != null)
            {
                pauseUI.SetActive(isPaused);
                Time.timeScale = isPaused ? 0f : 1f;
            }
        }

        // [과제 2] RESTART 버튼 클릭 시 실행할 함수
        public void OnClickRestart()
        {
            Debug.Log("Run RESTART");
            Time.timeScale = 1f; // 씬을 새로고침하기 전에 시간을 정상(1f)으로 복구해야 해!
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // [과제 2 / 7] MAIN MENU 버튼 클릭 시 실행할 함수
        public void OnClickMainMenu()
        {
            Debug.Log("Goto Menu");
            Time.timeScale = 1f; // 시간 복구
            // SceneManager.LoadScene("MainMenu"); // 메인메뉴 씬 이름이 생성되면 주석을 해제해줘
        }
    }
}