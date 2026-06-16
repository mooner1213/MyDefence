using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace MyDefence
{
    public class TileUI : MonoBehaviour
    {
        #region Variables
        public GameObject ui;

        [Header("--- 버튼 ---")]
        public Button upgradeButton;
        public Button sellButton;

        [Header("--- 텍스트 ---")]
        public TextMeshProUGUI upgradePriceText;
        public TextMeshProUGUI sellPriceText;

        private Tile selectedTile;
        #endregion

        #region Custom Methods

        public void ShowTileUI(Tile tile)
        {
            selectedTile = tile;

            Vector3 offset = Vector3.up * 2.5f;
            this.transform.position = tile.transform.position + offset;

            RefreshUI();

            // BuildManager(항상 활성화)에서 코루틴 실행
            BuildManager.Instance.StartCoroutine(ReplayAnimation());
        }

        private IEnumerator ReplayAnimation()
        {
            ui.SetActive(false);
            yield return null; // 한 프레임 대기
            ui.SetActive(true);
        }

        public void HideTileUI()
        {
            ui.SetActive(false);
            selectedTile = null;
        }

        private void RefreshUI()
        {
            if (selectedTile == null) return;

            int sellPrice = selectedTile.GetSellPrice();
            if (sellPriceText != null)
                sellPriceText.text = $"{sellPrice}G";

            if (selectedTile.IsUpgraded())
            {
                if (upgradePriceText != null)
                    upgradePriceText.text = "DONE";
                if (upgradeButton != null)
                    upgradeButton.interactable = false;
            }
            else
            {
                if (selectedTile.towerBlueprint != null)
                {
                    int upgradeCost = selectedTile.towerBlueprint.upgradeCost;
                    if (upgradePriceText != null)
                        upgradePriceText.text = $"{upgradeCost}G";
                }
                if (upgradeButton != null)
                    upgradeButton.interactable = true;
            }
        }

        public void UpgradeTower()
        {
            if (selectedTile == null) return;
            selectedTile.UpgradeTower();
        }

        public void SellTower()
        {
            if (selectedTile == null) return;
            selectedTile.SellTower();
        }

        public void OnUpgraded()
        {
            RefreshUI();
        }

        #endregion
    }
}