using UnityEngine;
using UnityEngine.EventSystems; // 마우스 이벤트 감지 기능 사용
namespace MyDefence
{
    // IPointerEnterHandler = 마우스가 올라왔을 때 감지
    // IPointerExitHandler = 마우스가 나갔을 때 감지
    // IPointerClickHandler = 마우스 클릭했을 때 감지
    public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region variables
        private Color StartColor;   // 타일의 원래 색깔 기억
        public Color EndColor = Color.green;    // 마우스가 올라왔을 때 바뀔 색깔
        private Renderer rend;  // 타일의 색을 실제로 바꿔줄 컴포넌트
        private bool isOccupied = false;    // 타일에 터렛이 설치가 되어있는지에 대한 여부
        private GameObject MyTurret;    // 타일에 실제로 설치된 터렛을 담아두는 바구니
        public GameObject turretPrefab; // 설치할 타일 프리펩

        #endregion

        #region Unity Event Method
        void Start()
        {
            rend = GetComponent<Renderer>();    // 이 오브젝트의 랜더러 컴포넌트를 꺼내와서 rend에 저장
            StartColor = rend.material.color;   // 현재 색을 StartColor에 저장
        }

        public void OnPointerEnter(PointerEventData eventData)  // 마우스가 타일 위로 올라왔을 때 실행
        {
            rend.material.color = EndColor; // 타일의 색을 EndColor로 적용
        }

        public void OnPointerExit(PointerEventData eventData)   // 마우스가 타일에서 나갔을 때 실행
        {
            rend.material.color = StartColor;   // 기존색을 저장해뒀던 StartColor로 다시 적용
        }

        public void OnPointerClick(PointerEventData eventData)  // 마우스가 타일을 클릭했을 때 실행
        {
            Debug.Log("마우스 클릭 - 여기에 터렛 설치");    // 콘솔창에 클릭할때마다 로그 출력

            if (isOccupied) return;

            BuildManager.Instance.BuildTurret(transform.position); // BuildManager한테 위임
            isOccupied = true;
        }
        #endregion
    }
}