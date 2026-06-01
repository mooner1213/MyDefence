using UnityEngine;
namespace MySample
{
    public class SingletonTest : MonoBehaviour
    {
        #region
        private void Start()
        {
            SingletonClass.Instance.number = 10;
            Debug.Log(SingletonClass.Instance.number.ToString());
        }
        #endregion
    }
}

/*

디자인 패턴

싱글톤 패턴
- 프로젝트 내에서 하나의 인스턴스만 존재하게 한다. (예시: 게임 매니저, 사운드 매니저 등) 즉, new를 한번만 하게끔 한다.
- 클래스의 인스턴스에게 전역적으로 접근이 가능하게 한다, 인스턴스 변수를 static으로 선언.

- 싱글톤 클래스의 인스턴스 변수는 자신 클래스의 코드블록 안에서 선언하고 객체를 가져온다.
 
 */