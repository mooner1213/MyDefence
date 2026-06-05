using UnityEngine;
using TMPro; // TextMeshPro 기능 사용

namespace MySample
{
    /// <summary>
    /// UI 샘플 예제 - 버튼 호출 함수 구현
    /// </summary>
    public class UITest : MonoBehaviour
    {
        #region Variables
        private int n = 0; // 점프 횟수 변수
        public int Score = 0; // 점수 변수

        public TextMeshProUGUI ScoreText; // 점수를 화면에 보여주는 텍스트
        #endregion

        #region Custom Methods
        // Fire 버튼 클릭 시 호출 되는 함수(등록되는 함수)
        // : public void 버튼이름() { 실행할 코드 }
        public void Fire()
        {
            Debug.Log("발사!"); // 콘솔창에 로그 출력
        }

        // Jump 버튼 클릭 시 호출 되는 함수
        // 버튼을 누를 때 마다 점수(Score)가 10점씩 증가
        public void Jump()
        {
            Score += 10;
            n++;
            Debug.Log($"{n}번 점프하였습니다.");
            
            if (ScoreText != null)
            {
                ScoreText.text = $"Score : {Score}";
            }
        }
        #endregion
    }
}