using UnityEngine;

namespace MyDefence
{
    // 이 클래스는 유니티 오브젝트에 붙이지 않고 데이터만 관리할 거라서
    // 뒤에 붙어있던 ': MonoBehaviour'를 지워줬어!
    public class GameData
    {
        // static을 붙여서 게임 전체에서 단 하나만 존재하는 공용 돈 통장을 만들었어.
        // 초기값은 과제 조건대로 400 Gold!
        public static int money = 400;
    }
}