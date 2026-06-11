using UnityEngine;

namespace MyDefence
{
    // 이 클래스는 유니티 오브젝트에 붙이지 않고 데이터만 관리할 거라서
    // 뒤에 붙어있던 ': MonoBehaviour'를 지워줬어!
    public static class GameData
    {
        public static int money = 1000;       // 소지금
        public static int lives = 10;         // [과제 0] 초기 라이프 10개
        public static int roundsSurvived = 25; // [과제 1] UI 연결용 라운드 카운트 (예시값)
    }
   
}