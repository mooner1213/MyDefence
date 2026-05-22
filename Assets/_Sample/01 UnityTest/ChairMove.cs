using UnityEngine; // 유니티 엔진의 기능을 사용하기 위해 꼭 필요해요.

namespace MySample
{
    public class ChairMove : MonoBehaviour
    {
        // [변수 설정]
        // moveSpeed는 의자가 움직이는 속도를 결정해요.
        // 5.0f라고 적으면 '초당 5미터' 정도로 생각하시면 됩니다.
        // [SerializeField]를 쓰면 유니티 에디터에서 이 속도를 직접 조절할 수 있어요.
        [SerializeField] private float moveSpeed = 5.0f;

        // Update 함수는 매 프레임마다 한 번씩 실행되는 '심장' 같은 함수입니다.
        // (보통 게임은 초당 60프레임을 목표로 하니, 초당 60번 실행될 수도 있어요!)
        void Update()
        {
            // [이동 명령]
            // 1. transform.Translate는 이 오브젝트를 움직이라는 명령어입니다.
            // 2. Vector3.forward는 "앞쪽(Z축)"을 나타내는 미리 정의된 방향입니다. (이미지의 파란 화살표!)
            // 3. * moveSpeed는 지정한 속도만큼 곱해주는 것입니다.
            // 4. * Time.deltaTime은 아주 중요해요! 
            //    프레임과 무관하게 '초' 단위로 일정한 움직임을 만들기 위해 사용합니다.
            //    (성능이 좋은 컴퓨터나 안 좋은 컴퓨터나 똑같이 초당 moveSpeed만큼 가게 해줘요.)

            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}