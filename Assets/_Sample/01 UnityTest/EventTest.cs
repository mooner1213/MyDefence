using UnityEngine;

public class EventTest : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("[1] Awake 실행");  // 1회만 실행, 가장먼저 실행함.
    }

    private void Start()
    {
        Debug.Log("[2] Start 실행");  // 1회만 실행
    }

    private void OnEnable()
    {
        Debug.Log("[7] OnEnable 실행");  // (활성화 될 때마다)1회만 실행
    }

    private void FixedUpdate()
    {
        Debug.Log("[3] FixedUpdate 실행");  // 1초에 50번 고정 호출, 물리연산
    }

    private void Update()
    {
        Debug.Log("[4] Update 실행");  // 매 프레임마다 호출, 게임 로직 연산
    }

    private void LateUpdate()
    {
        Debug.Log("[5] LateUpdate 실행");  // update 실행 바로 뒤에 따라서 실행, 카메라 연산 등
    }

    private void OnDisable()
    {
        Debug.Log("[7-1] OnDisable 실행");  // (비 활성화 될 때마다)1회만 실행
    }

    private void OnDestroy()
    {
        Debug.Log("[6] OnDestroy 실행");  // 소멸 시 마지막으로 실행
    }
}
