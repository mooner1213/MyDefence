using UnityEngine;

namespace MyDefence
{
    public class BuildManager : MonoBehaviour
    {
        // 싱글톤 패턴으로 BuildManager 클래스의 인스턴스(객체)를 담을 정적(static) 변수 선언
        public static BuildManager Instance;

        #region Variables
        [Header("타워 프리팩 프리셋")]
        // 🔴 기존 GameObject 대신, 방금 만든 세트 상품(TowerBlueprint)으로 변경!
        public TowerBlueprint machineGunBlueprint;   // 머신건 타워 세트 (프리팹 + 가격)
        public TowerBlueprint rocketTowerBlueprint; // 로켓 타워 세트 (프리팹 + 가격)
        public TowerBlueprint laserTowerBlueprint;

        // 🔴 현재 유저가 어떤 타워 '세트'를 선택했는지 기억하는 바구니로 업그레이드!
        private TowerBlueprint towerToBuild = null;
        #endregion

        void Awake()
        {
            Instance = this;
        }

        #region Custom Methods (UI 버튼에서 호출할 함수들)

        // ⭐️ 과제 조건: 선택 기능과 취소 기능을 하나로 묶어주는 핵심 함수야!
        public void SelectTowerToBuild(TowerBlueprint blueprint)
        {
            // 만약 이미 선택한 타워를 또 누르면? 선택을 취소(null)해버려!
            if (towerToBuild == blueprint)
            {
                towerToBuild = null;
                Debug.Log("타워 선택을 취소하였습니다.");
                return; // 함수를 여기서 즉시 종료!
            }

            // 새로운 타워를 눌렀다면 그 타워 세트를 바구니에 저장!
            towerToBuild = blueprint;
            Debug.Log($"{blueprint.prefab.name} 타워를 선택하였습니다! 가격: {blueprint.cost}");
        }

        // 2-2) 첫 번째 머신건 타워 버튼 클릭 시 호출
        public void SelectMachineGun()
        {
            // 위에서 만든 핵심 함수에 머신건 세트를 쏙 넣어줘
            SelectTowerToBuild(machineGunBlueprint);
        }

        // 4-1) 두 번째 다른 타워 버튼 클릭 시 호출
        public void SelectRocketTower()
        {
            // 위에서 만든 핵심 함수에 로켓 타워 세트를 쏙 넣어줘
            SelectTowerToBuild(rocketTowerBlueprint);
        }

        public void SelectLaserTower()
        {
            SelectTowerToBuild(laserTowerBlueprint);
        }
        #endregion

        #region Public Methods (Tile에서 확인할 기능들)
        // 현재 선택된 타워가 있는지 없는지 알려주는 함수 (null이 아니면 true)
        public bool HasSelectedTower()
        {
            return towerToBuild != null;
        }

        // 🔴 중요: 이제 Tile 스크립트 등에서 프리팹을 가져갈 때 
        // towerToBuild가 '세트 상품'이 되었으니, 그 안의 '.prefab'을 쏙 꺼내서 줘야 해!
        public GameObject GetTowerToBuild()
        {
            if (towerToBuild == null) return null;
            return towerToBuild.prefab;
        }

        // BuildManager.cs의 Public Methods 구역에 추가해줘!
        public int GetSelectedTowerCost()
        {
            if (towerToBuild == null) return 0;
            return towerToBuild.cost; // 현재 선택된 타워의 가격을 알려줌!
        }
        #endregion
    }
}