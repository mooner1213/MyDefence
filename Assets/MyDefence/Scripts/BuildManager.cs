using UnityEngine;

namespace MyDefence
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance; // 싱글톤 패턴으로 BuildManager 클래스의 인스턴스(객체)를 담을 정적(static) 변수 선언

        public GameObject TurretPrefeb; // 빌드할 터렛 프리팹을 담을 바구니

        void Awake()
        {
             Instance = this;
        }

        public void BuildTurret(Vector3 position)
        {
            Instantiate(TurretPrefeb, position, Quaternion.identity);
        }
    }
}