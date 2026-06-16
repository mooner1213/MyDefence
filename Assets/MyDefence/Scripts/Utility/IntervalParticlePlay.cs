using UnityEngine;
namespace MyDefence
{
    /// <summary>
    /// 지정된 시간 간격으로 파티클 이펙트 플레이를 시켜주는 클래스
    /// </summary>
    public class IntervalParticlePlay : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private float interval = 3f;

        private float timer = 0f;

        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                particle.Stop();
                particle.Play();
                timer = 0f;
            }
        }
    }
}
