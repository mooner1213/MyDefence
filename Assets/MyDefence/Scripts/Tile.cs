using UnityEngine;

public class Tile : MonoBehaviour
{
    // [변수 선언] 터렛을 올릴 수 있는 공간(바구니)들을 만듭니다.

    // 1. 이 타일에 터렛이 이미 설치되었는지 참(True)/거짓(False)으로 기억하는 바구니입니다.
    // 처음에는 터렛이 없으니 거짓(false)으로 시작해요.
    public bool isOccupied = false;

    // 2. 이 타일 위에 실제로 올라간 터렛 물체(GameObject)를 담아둘 바구니입니다.
    // 현재는 비어있다는 뜻으로 null(비어있음)을 넣어둡니다.
    public GameObject myTurret = null;

    // Start 함수는 게임이 시작될 때 '딱 한 번' 실행되는 곳이에요.
    void Start()
    {
        // 게임 시작 시 타일의 상태를 확인해봅니다.
        Debug.Log("타일이 준비되었습니다! 현재 설치 여부: " + isOccupied);
    }

    // Update 함수는 게임이 실행되는 동안 매 프레임(1초에 수십 번)마다 계속 실행되는 심장과 같은 곳이에요.
    void Update()
    {
        // 여기서는 매 순간 타일에 변화가 있는지 감시할 수 있어요.
    }
}