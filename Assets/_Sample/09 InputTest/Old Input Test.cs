using UnityEngine;

namespace MySample
{
    /// <summary>
    ///  old Input test 예제
    /// </summary>
    public class OldInputTest : MonoBehaviour
    {
        #region Variables
        #endregion

        #region Unity Event Method
        private void Update()
        {
            // 키 입력 체크 - w키 입력
            if (Input.GetKey("w"))
            {
                Debug.Log("W키를 누르고 있습니다.");
            }

            if (Input.GetKeyDown("w"))
            {
                Debug.Log("W키를 눌렀습니다.");
            }

            if (Input.GetKeyUp("w"))
            {
                Debug.Log("W키에서 손을 눌렀다가 뗐습니다.");
            }

            // GetButton - Input Manager에 정의되어 있는Buttons(Axis)의 이름을 가져와서 체크하는 방식
            // 버튼의 이름은 문자열로 가져온다.
            if (Input.GetButton("Jump"))
            {
                Debug.Log("점프 버튼을 누르고 있습니다.");
            }

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("점프 버튼을 눌렀습니다.");
            }

            if (Input.GetButtonUp("Jump"))
            {
                Debug.Log("점프 버튼에서 손을 눌렀다가 뗐습니다.");
            }

            // GetAxis - Input Manager에 정의되어 있는 Axis(Buttons)의 이름을 가져와서 체크하는 방식
            // a, left : -1 ~ 0
            // d, right : 0 ~ 1
            float hValue = Input.GetAxis("Horizontal");
            Debug.Log($"Horizontal : {hValue}");

            float vValue = Input.GetAxis("Vertical");
            Debug.Log($"Vertical : {vValue}");

            // 스크린상의 마우스 위치값 가져오기
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            Debug.Log($"Mouse Position : ({mouseX}, {mouseY})");
        }
        #endregion
    }
}