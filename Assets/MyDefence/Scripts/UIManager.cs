using UnityEngine;
using UnityEngine.UI; // 텍스트 컴포넌트 제어용

namespace MyDefence
{
    public class UIManager : MonoBehaviour
    {
        [Header("--- 상단 기본 UI ---")]
        public Text moneyText;        // 소지금 표시용 텍스트
        public Text livesText;        // 라이프 표시용 텍스트

        [Header("--- 게임오버 팝업 UI ---")]
        public GameObject gameOverUI; // 게임오버 시 켜줄 패널 오브젝트
        public Text roundsText;       // 몇 라운드 생존했는지 출력할 텍스트

        void Start()
        {
            // 게임 시작 시 게임오버 UI는 화면에서 숨깁니다.
            if (gameOverUI != null) gameOverUI.SetActive(false);
        }

        void Update()
        {
            // [과제 0] 실시간 데이터 표시
            if (moneyText != null) moneyText.text = $"MONEY: {GameData.money}";
            if (livesText != null) livesText.text = $"LIVES: {GameData.lives}";

            // [과제 3] 치트키 'O' 누르면 게임오버 팝업 활성화
            if (Input.GetKeyDown(KeyCode.O))
            {
                TriggerGameOver();
            }
        }

        // 게임오버창을 활성화하는 함수
        public void TriggerGameOver()
        {
            if (gameOverUI != null) gameOverUI.SetActive(true);

            // [과제 1] 생존 라운드 수 UI 반영
            if (roundsText != null)
            {
                roundsText.text = $"{GameData.roundsSurvived} ROUNDS SURVIVED";
            }
        }

        // [과제 2] RESTART 버튼 클릭 이벤트
        public void OnClickRestart()
        {
            Debug.Log("Run RESTART");
        }

        // [과제 2] MAIN MENU 버튼 클릭 이벤트
        public void OnClickMainMenu()
        {
            Debug.Log("Goto Menu");
        }
    }
}