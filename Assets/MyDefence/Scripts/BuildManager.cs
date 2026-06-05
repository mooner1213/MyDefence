using UnityEngine;

namespace MyDefence
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance; // 싱글톤 패턴으로 BuildManager 클래스의 인스턴스(객체)를 담을 정적(static) 변수 선언

        #region Variables
        [Header("타워 프리팩 프리셋")]
        public GameObject TurretPrefab;      // 1번 머신건 타워 프리팹을 담을 바구니 (오타 Prefeb -> Prefab 수정)
        public GameObject AnotherTowerPrefab; // 4번 다른 타워 프리팹을 담을 바구니

        // 현재 유저가 어떤 타워를 선택했는지 '기억'하는 임시 보관 바구니예요. (선택 안 했을 땐 null)
        private GameObject towerToBuild = null;
        #endregion

        void Awake()
        {
            Instance = this;
        }

        #region Custom Methods (UI 버튼에서 호출할 함수들)
        // 2-2) 첫 번째 머신건 타워 버튼 클릭 시 호출
        public void SelectMachineGun()
        {
            towerToBuild = TurretPrefab;
            Debug.Log("머신건 타워를 선택 하였습니다!!");
        }

        // 4-1) 두 번째 다른 타워 버튼 클릭 시 호출
        public void SelectAnotherTower()
        {
            towerToBuild = AnotherTowerPrefab;
            Debug.Log("다른 타워 선택 하였습니다!");
        }
        #endregion

        #region Public Methods (Tile에서 확인할 기능들)
        // 현재 선택된 타워가 있는지 없는지 알려주는 함수 (null이 아니면 true)
        public bool HasSelectedTower()
        {
            return towerToBuild != null;
        }

        // 현재 선택된 타워 프리팹을 타일에 전달해주는 함수
        public GameObject GetTowerToBuild()
        {
            return towerToBuild;
        }
        #endregion
    }
}