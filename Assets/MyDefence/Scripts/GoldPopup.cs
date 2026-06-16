using UnityEngine;
using TMPro;

namespace MyDefence
{
    // 타워 판매 시 획득 골드 수치가 위로 둥실 떠오르다가 사라지는 팝업 텍스트
    // SellEffectPrefab 오브젝트에 붙여서 사용 (TextMeshPro 3D 컴포넌트 필요)
    public class GoldPopup : MonoBehaviour
    {
        public TextMeshPro text;        // 3D TextMeshPro 컴포넌트 (World Space용)
        public float floatSpeed = 2f;   // 위로 올라가는 속도
        public float fadeSpeed = 1.5f;  // 투명해지는 속도 (현재 lifetime 기반으로 자동 계산됨)
        public float lifetime = 1.2f;   // 총 존재 시간 (초) → 이 시간이 지나면 자동 삭제

        private Color textColor;        // 텍스트 색상 (알파값 조절용으로 복사해둠)
        private float timer = 0f;       // 생성된 이후 경과 시간

        void Start()
        {
            if (text == null) text = GetComponent<TextMeshPro>(); // 컴포넌트 자동 탐색
            textColor = text.color;
        }

        // 판매 금액을 외부(Tile.SellTower)에서 세팅할 때 호출
        // 예: goldPopup.SetAmount(250) → "+250G" 텍스트 표시
        public void SetAmount(int amount)
        {
            if (text != null)
                text.text = $"+{amount}G";
        }

        void Update()
        {
            timer += Time.deltaTime;

            // 매 프레임 위로 둥실 이동
            transform.position += Vector3.up * floatSpeed * Time.deltaTime;

            // lifetime에 비례해서 알파값을 1 → 0으로 줄여 서서히 투명하게
            float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
            textColor.a = alpha;
            text.color = textColor;

            // 수명이 다하면 오브젝트 삭제
            if (timer >= lifetime)
                Destroy(gameObject);
        }
    }
}