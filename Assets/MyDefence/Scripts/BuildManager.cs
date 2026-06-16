using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace MyDefence
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;

        #region Variables
        [Header("타워 프리팩 프리셋")]
        public TowerBlueprint machineGunBlueprint;
        public TowerBlueprint rocketTowerBlueprint;
        public TowerBlueprint laserTowerBlueprint;

        private TowerBlueprint towerToBuild = null;

        public TileUI tileUI;
        private Tile selectedTile;
        private MachinGunTower selectedTower;

        // UI 레이어 감지용
        private PointerEventData _eventData;
        private List<RaycastResult> _raycastResults = new List<RaycastResult>();
        #endregion

        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (selectedTile == null) return;

            if (Input.GetMouseButtonDown(0))
            {
                // UI 레이어 위를 클릭했는지 체크
                if (IsPointerOverUI()) return;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // 타일 클릭 → Tile.cs가 처리하므로 패스
                    if (hit.collider.GetComponent<Tile>() != null) return;
                }

                // UI도 타일도 아닌 곳 → TileUI 닫기
                DeSelectTile();
            }
        }

        // UI 레이어만 정확하게 감지
        private bool IsPointerOverUI()
        {
            if (_eventData == null) _eventData = new PointerEventData(EventSystem.current);
            _eventData.position = Input.mousePosition;

            _raycastResults.Clear();
            EventSystem.current.RaycastAll(_eventData, _raycastResults);

            for (int i = 0; i < _raycastResults.Count; i++)
            {
                if (_raycastResults[i].gameObject.layer == LayerMask.NameToLayer("UI"))
                    return true;
            }
            return false;
        }

        #region Custom Methods

        public void SelectTowerToBuild(TowerBlueprint blueprint)
        {
            if (towerToBuild == blueprint)
            {
                towerToBuild = null;
                Debug.Log("타워 선택을 취소하였습니다.");
                return;
            }
            towerToBuild = blueprint;
            Debug.Log($"{blueprint.prefab.name} 타워를 선택하였습니다! 가격: {blueprint.cost}");
        }

        public void SelectMachineGun() => SelectTowerToBuild(machineGunBlueprint);
        public void SelectRocketTower() => SelectTowerToBuild(rocketTowerBlueprint);
        public void SelectLaserTower() => SelectTowerToBuild(laserTowerBlueprint);

        #endregion

        #region Public Methods

        public bool HasSelectedTower() => towerToBuild != null;

        public GameObject GetTowerToBuild()
        {
            if (towerToBuild == null) return null;
            return towerToBuild.prefab;
        }

        public int GetSelectedTowerCost()
        {
            if (towerToBuild == null) return 0;
            return towerToBuild.cost;
        }

        public void SelectTile(Tile tile)
        {
            if (selectedTile == tile)
            {
                DeSelectTile();
                return;
            }

            selectedTower = null;
            selectedTile = tile;
            tileUI.ShowTileUI(tile);
        }

        public void DeSelectTile()
        {
            selectedTower = null;
            selectedTile = null;
            tileUI.HideTileUI();
        }

        public TowerBlueprint GetSelectedBlueprint() => towerToBuild;

        public bool HasUpgradeCost()
        {
            if (selectedTile == null || selectedTile.towerBlueprint == null) return false;
            return GameData.money >= selectedTile.towerBlueprint.upgradeCost;
        }

        public int GetUpgradeCost()
        {
            if (selectedTile == null || selectedTile.towerBlueprint == null) return 0;
            return selectedTile.towerBlueprint.upgradeCost;
        }

        public void OnTileUpgraded()
        {
            if (tileUI != null)
                tileUI.OnUpgraded();
        }

        #endregion
    }
}