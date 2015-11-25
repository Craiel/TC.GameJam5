namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;

    using UnityEngine;

    // Placeholder...
    public class IntroUI : MonoBehaviour
    {
        private float startupTime;

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void Awake()
        {
            this.startupTime = Time.time;
        }

        public void Update()
        {
            if (GameSystem.Instance.InTransition)
            {
                return;
            }

            if (Time.time > this.startupTime + 5f)
            {
                GameSystem.Instance.Transition(GameSceneType.MainMenu);
            }
        }
    }
}
