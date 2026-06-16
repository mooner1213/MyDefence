using UnityEngine;
using System; // 👈 [System.Serializable]을 쓰기 위해 꼭 필요해!

// 이 클래스를 유니티 인스펙터 창에 노출시키겠다는 "포장지" 표시야!
[System.Serializable]
public class TowerBlueprint
{
    // 타워의 모양과 기능이 담긴 원본(프리랩)을 담는 변수야.
    public GameObject prefab;

    // 이 타워를 지을 때 필요한 골드(가격)를 저장하는 변수야.
    public int cost;

    // 1차 업그레이드
    public GameObject upgradePrefab; // 타워 업그레이드에 필요한 프리펩 오브젝트
    public int upgradeCost; // 타워 업그레이드 비용

}