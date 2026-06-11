using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MySample
{
    public class Money : MonoBehaviour
    {
        #region Variables

        public int money = 1000; // 현재 소지금을 담을 바구니
        public Image ButtonImage1; // 아이템 구매 버튼의 이미지 컴포넌트 (색깔 변경을 위해)
        public Image ButtonImage2; // 아이템 구매 버튼의 이미지 컴포넌트 (색깔 변경을 위해)
        public TextMeshProUGUI GoldText; // UI 텍스트 컴포넌트
        #endregion

        #region Custom Method

        // UI에 현재 gold 값을 표시하는 함수

        private void Update()
        {
            GoldText.text = "Gold: " + money;

            CheckButtonColors();
        }

        public void Bank()
        {
            money += 500; // Bank 버튼을 누를 때마다 500원씩 증가
        }

        public void Item1()
        {
            if (money >= 2000)
            {
                Debug.Log("아이템 1 을 구매하였습니다!"); // 아이템 구매 성공 메시지 출력
                money -= 2000; // Item1 버튼을 누를 때마다 2000원씩 감소
            }
            else
            {
                Debug.Log("돈이 부족합니다!"); // 돈이 부족할 때 경고 메시지 출력
            }
        }

        public void Item2()
        {
            if (money >= 10000)
            {
                Debug.Log("아이템 2 을 구매하였습니다!"); // 아이템 구매 성공 메시지 출력
                money -= 10000; // Item2 버튼을 누를 때마다 10000원씩 감소
            }
            else
            {
                Debug.Log("돈이 부족합니다!"); // 돈이 부족할 때 경고 메시지 출력
            }
        }

        private void CheckButtonColors()
        {
            // 1번 아이템 버튼 색상 체크 (가격: 2000원)
            if (ButtonImage1 != null)
            {
                if (money >= 2000) ButtonImage1.color = Color.white;
                else ButtonImage1.color = Color.red;
            }

            // 2번 아이템 버튼 색상 체크 (가격: 10000원)
            if (ButtonImage2 != null)
            {
                if (money >= 10000) ButtonImage2.color = Color.white;
                else ButtonImage2.color = Color.red;
            }
        }
        #endregion
    }
}