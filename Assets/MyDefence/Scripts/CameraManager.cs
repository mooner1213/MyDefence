using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Variables
    [Header("카메라 이동 설정")]
    public float moveSpeed = 15f;       // 카메라가 키보드나 마우스로 움직이는 속도 바구니예요.
    public float borderThickness = 20f; // 마우스를 화면 끝쪽 20픽셀 안에 가져다 대면 움직이게 할 기준 폭이에요.

    [Header("줌(Zoom) 설정")]
    public float scrollSpeed = 20f;     // 마우스 휠을 굴릴 때 줌이 되는 속도예요.
    public float minY = 10f;            // 카메라가 최대로 내려갈 수 있는 높이(줌인 제한)예요.
    public float maxY = 25f;            // 카메라가 최대로 올라갈 수 있는 높이(줌아웃 제한)예요.

    private bool isMovementLocked = false; // 카메라 이동을 막는 자물쇠 역할을 하는 변수예요. (false면 이동 가능, true면 이동 불가능)
    #endregion


    #region Unity Event Method
    // Update 함수는 매 프레임마다(1초에 수십 번씩) 실행되는 게임의 심장과 같은 곳이에요!
    private void Update()
    {
        // 4번 과제: ESC 키를 누르면 카메라 이동 상태를 토글(참/거짓 전환)합니다.
        // GetKeyDown은 키를 '탁!' 누른 그 순간 딱 한 번만 감지해요.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ! 연산자는 반대로 바꾸는 청개구리 연산자예요. (true였으면 false로, false였으면 true로)
            isMovementLocked = !isMovementLocked;
        }

        // 만약 자물쇠가 잠겨있다면(isMovementLocked가 true라면) 
        // 아래의 이동/줌 기능을 전부 건너뛰고 Update 함수를 여기서 끝내버립니다(return).
        if (isMovementLocked)
        {
            return;
        }

        // 자물쇠가 안 잠겨있을 때만 아래 코드들이 실행돼요!
        MoveCamera(); // 카메라 이동 함수 실행
        ZoomCamera(); // 카메라 줌 함수 실행
    }
    #endregion


    #region Custom Methods (카메라 기능 구현)
    // 카메라를 키보드와 마우스 경계로 이동시키는 함수예요.
    private void MoveCamera()
    {
        // Vector3는 3차원 공간의 (X, Y, Z) 좌표를 담는 바구니예요.
        // 처음에는 움직임이 없으니 (0, 0, 0)으로 시작해요.
        Vector3 movement = Vector3.zero;

        // --- 1번 과제: A, S, D, W 키 입력 처리 ---
        // Input.GetKey는 키를 누르고 있는 '동안' 계속 감지해요.
        if (Input.GetKey(KeyCode.W)) { movement.z += 1f; } // 위로 이동 (Z축 증가)
        if (Input.GetKey(KeyCode.S)) { movement.z -= 1f; } // 아래로 이동 (Z축 감소)
        if (Input.GetKey(KeyCode.D)) { movement.x += 1f; } // 오른쪽으로 이동 (X축 증가)
        if (Input.GetKey(KeyCode.A)) { movement.x -= 1f; } // 왼쪽으로 이동 (X축 감소)

        // --- 2번 과제: 마우스 화면 끝부분 경계 인식 처리 ---
        // Input.mousePosition은 현재 마우스의 화면 상 좌표(X, Y)를 알려줘요.
        Vector3 mousePos = Input.mousePosition;

        // 마우스 X 좌표가 0보다 크고 화면 왼쪽 끝(borderThickness = 20) 안에 들어왔을 때
        if (mousePos.x >= 0 && mousePos.x <= borderThickness)
        {
            movement.x -= 1f; // 왼쪽으로 이동
        }
        // 마우스 X 좌표가 화면 오른쪽 끝(Screen.width - 20)보다 바깥에 있을 때
        else if (mousePos.x >= Screen.width - borderThickness && mousePos.x <= Screen.width)
        {
            movement.x += 1f; // 오른쪽으로 이동
        }

        // 마우스 Y 좌표가 0보다 크고 화면 아래쪽 끝 안에 들어왔을 때
        if (mousePos.y >= 0 && mousePos.y <= borderThickness)
        {
            movement.z -= 1f; // 아래로 이동 (3차원에서는 Z축이 앞뒤/상하 평면 이동이에요)
        }
        // 마우스 Y 좌표가 화면 위쪽 끝(Screen.height - 20)보다 바깥에 있을 때
        else if (mousePos.y >= Screen.height - borderThickness && mousePos.y <= Screen.height)
        {
            movement.z += 1f; // 위로 이동
        }

        // movement 변수의 방향 크기를 항상 1로 맞춰서, 대각선으로 이동할 때 컴퓨터가 너무 빨라지지 않게 보정해줘요.
        movement.Normalize();

        // transform.Translate는 이 스크립트가 붙은 오브젝트(카메라)를 실제로 움직이게 만드는 명령어예요.
        // Time.deltaTime은 컴퓨터 성능이 달라도 모두가 똑같은 속도로 움직이게 해주는 '시간 보정 키트'입니다.
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    // 마우스 휠을 이용해 카메라의 높이(Y축)를 조절하는 함수예요.
    private void ZoomCamera()
    {
        // --- 3번 과제: 마우스 휠 스크롤 값 받기 ---
        // Input.GetAxis("Mouse ScrollWheel")은 휠을 위로 굴리면 양수(+), 아래로 굴리면 음수(-)를 반환해요.
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // 현재 카메라의 위치 좌표를 임시 바구니(pos)에 복사해옵니다.
        Vector3 pos = transform.position;

        // 휠을 굴린 양만큼 Y값(높이)을 조절해요. 
        // 휠을 위로 굴리면(scrollInput > 0) 높이가 낮아져야(줌인) 하므로 마이너스(-)를 해줍니다.
        pos.y -= scrollInput * scrollSpeed * Time.deltaTime;

        // Mathf.Clamp는 값이 특정 최소/최대 범위를 벗어나지 못하게 꽉 잡아주는 아주 유용한 집게예요!
        // pos.y 값을 무조건 minY(10f)와 maxY(25f) 사이로만 고정시킵니다.
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // 계산이 끝난 새로운 좌표를 카메라의 실제 위치(transform.position)에 다시 쏙 넣어줍니다.
        transform.position = pos;
    }
    #endregion
}