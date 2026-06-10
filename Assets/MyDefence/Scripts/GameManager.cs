using UnityEngine;

namespace MyDefence
{
    public class GameManager : MonoBehaviour
    {
        void Update()
        {
            // ⌨️ 키보드의 'M' 키를 "누른 그 순간(GetKeyDown)"을 감지해!
            if (Input.GetKeyDown(KeyCode.M))
            {
                // static 변수 통장에 100,000 골드를 더해준다!
                GameData.money += 100000;

                // 돈이 잘 들어왔는지 콘솔창에 치트키 발동 로그 띄우기
                Debug.Log("💰 치트키 발동! 10만 골드 지급 완료! 현재 잔액: " + GameData.money);
            }
        }
    }
}