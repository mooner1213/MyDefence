using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyDefence
{
    public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region variables
        private Color StartColor;
        public Color EndColor = Color.green;
        private Renderer rend;
        private bool isOccupied = false;
        private GameObject MyTurret;

        public TowerBlueprint towerBlueprint;
        private bool isUpgraded = false;

        private PointerEventData _eventData;
        private List<RaycastResult> _raycastResults = new List<RaycastResult>();

        public TileUI tileUI;

        [Header("--- 이펙트 설정 ---")]
        public GameObject buildEffectPrefab;
        public GameObject sellEffectPrefab;     // GoldPopup 스크립트가 붙어있는 프리팹
        #endregion

        #region Unity Event Method
        void Start()
        {
            rend = GetComponent<Renderer>();
            StartColor = rend.material.color;
        }

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsPointerOverUI()) return;
            if (!BuildManager.Instance.HasSelectedTower()) return;
            rend.material.color = EndColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rend.material.color = StartColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsPointerOverUI()) return;

            if (isOccupied)
            {
                BuildManager.Instance.SelectTile(this);
                return;
            }

            if (!BuildManager.Instance.HasSelectedTower())
            {
                Debug.Log("타워를 선택하지 않아 설치할 수 없습니다.!!");
                return;
            }

            int towerCost = BuildManager.Instance.GetSelectedTowerCost();
            if (GameData.money < towerCost)
            {
                Debug.Log("돈이 부족합니다");
                return;
            }

            GameData.money -= towerCost;
            Debug.Log("건설하고 남은돈 : " + GameData.money);

            GameObject towerPrefab = BuildManager.Instance.GetTowerToBuild();
            MyTurret = Instantiate(towerPrefab, transform.position, Quaternion.identity);

            if (buildEffectPrefab != null)
            {
                GameObject fx = Instantiate(buildEffectPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                Destroy(fx, 2f);
            }

            towerBlueprint = BuildManager.Instance.GetSelectedBlueprint();
            isUpgraded = false;
            isOccupied = true;
            rend.material.color = StartColor;
        }

        public bool IsUpgraded() => isUpgraded;

        public int GetSellPrice()
        {
            if (towerBlueprint == null) return 0;
            int sellPrice = towerBlueprint.cost / 2;
            if (isUpgraded) sellPrice += towerBlueprint.upgradeCost / 2;
            return sellPrice;
        }

        public void UpgradeTower()
        {
            if (isUpgraded)
            {
                Debug.LogWarning("이 타일의 타워는 이미 업그레이드가 완료되었습니다!");
                return;
            }

            if (!BuildManager.Instance.HasUpgradeCost())
            {
                Debug.Log("업그레이드 건설 비용이 부족합니다.");
                return;
            }

            int upgradeCost = BuildManager.Instance.GetUpgradeCost();
            GameData.money -= upgradeCost;

            Destroy(MyTurret);
            GameObject upgradePrefab = towerBlueprint.upgradePrefab;
            MyTurret = Instantiate(upgradePrefab, transform.position, Quaternion.identity);

            if (buildEffectPrefab != null)
            {
                GameObject fx = Instantiate(buildEffectPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                Destroy(fx, 2f);
            }

            isUpgraded = true;
            Debug.Log("업그레이드 완료! 남은 돈: " + GameData.money);
            BuildManager.Instance.OnTileUpgraded();
        }

        public void SellTower()
        {
            if (MyTurret == null) return;

            int sellPrice = GetSellPrice();
            GameData.money += sellPrice;
            Debug.Log($"타워 판매! +{sellPrice} Gold. 현재 잔액: {GameData.money}");

            // 💰 판매된 타워 바로 위에 골드 팝업 생성 후 가격 세팅
            if (sellEffectPrefab != null)
            {
                // MyTurret 위치 기준으로 살짝 위에 생성
                Vector3 popupPos = MyTurret.transform.position + Vector3.up * 1.5f;
                GameObject fx = Instantiate(sellEffectPrefab, popupPos, Quaternion.identity);

                // GoldPopup 스크립트에 판매 가격 전달 → "+150G" 텍스트 표시
                GoldPopup popup = fx.GetComponent<GoldPopup>();
                if (popup != null)
                    popup.SetAmount(sellPrice);

                Destroy(fx, 2f);
            }

            Destroy(MyTurret);
            isOccupied = false;
            isUpgraded = false;
            towerBlueprint = null;

            BuildManager.Instance.DeSelectTile();
        }
        #endregion
    }
}