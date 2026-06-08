using UnityEngine;
namespace MyDefence
{
    public class DestroyEffect : MonoBehaviour
    {
        private Light explosionLight;
        private float currentIntensity;

        void Start()
        {
            explosionLight = GetComponentInChildren<Light>();

            if (explosionLight != null)
            {
                // 시작하자마자 눈이 멀 정도로 밝기를 세게 줍니다 (기본 15~20 이상)
                explosionLight.intensity = 20f;
                currentIntensity = explosionLight.intensity;
            }

            // 이펙트는 0.5초면 다 터지므로 굳이 1.5초 안 기다리고 0.7초 뒤에 바로 파괴시킵니다.
            Destroy(gameObject, 0.7f);
        }

        void Update()
        {
            if (explosionLight != null)
            {
                // Lerp보다 더 칼같이 줄어들도록 직관적으로 밝기를 빼버립니다.
                // 매 프레임 엄청난 속도로 밝기가 감소해서 0.1초 만에 섬광이 꺼집니다.
                currentIntensity -= Time.deltaTime * 60f;
                explosionLight.intensity = Mathf.Max(0f, currentIntensity);
            }
        }
    }
}