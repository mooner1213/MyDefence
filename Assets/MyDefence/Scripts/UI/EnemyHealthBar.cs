using UnityEngine;
using UnityEngine.UI;

namespace MyDefence
{
    /// <summary>
    /// 적의 HP를 실시간으로 HealthBar에 반영하는 클래스
    /// Enemy 오브젝트에 붙이고 BarImage를 연결해서 사용
    /// </summary>
    public class EnemyHealthBar : MonoBehaviour
    {
        [Header("--- HP바 설정 ---")]
        public Image barImage;          // BarImage (Image Type: Filled 로 설정 필수!)
        public Transform hpBarCanvas;   // HealthBar Canvas (카메라를 항상 바라보게 할 용도)

        private Enemy enemy;            // 같은 오브젝트의 Enemy 스크립트
        private int maxHp;              // 최대 HP (시작 시 저장)
        private Camera mainCam;         // 메인 카메라 (HP바가 항상 카메라를 바라보게)

        void Start()
        {
            enemy = GetComponent<Enemy>();
            mainCam = Camera.main;

            // 시작 시 HP를 최대치로 저장
            maxHp = enemy.hp;

            // 처음엔 꽉 찬 상태로 초기화
            if (barImage != null)
                barImage.fillAmount = 1f;
        }

        void Update()
        {
            // HP 비율 계산해서 fillAmount에 반영 (0.0 ~ 1.0)
            if (barImage != null && enemy != null)
                barImage.fillAmount = (float)enemy.hp / maxHp;

            // HP바 Canvas가 항상 카메라를 바라보도록 회전
            if (hpBarCanvas != null && mainCam != null)
                hpBarCanvas.LookAt(hpBarCanvas.position + mainCam.transform.rotation * Vector3.forward,
                                   mainCam.transform.rotation * Vector3.up);
        }
    }
}