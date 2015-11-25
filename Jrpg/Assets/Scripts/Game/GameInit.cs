namespace Assets.Scripts.Game
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;

    using UnityEngine;

    public class GameInit : MonoBehaviour
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public GameInit()
        {
            this.SceneGameData = new SceneGameData();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [SerializeField]
        public GameProgressDisplay ProgressDisplay;

        [SerializeField]
        public GameSceneType StartupScene;

        [SerializeField]
        public SceneGameData SceneGameData;

        public void Awake()
        {
            GameSystem.Instance.Initialize(this);
            GameSystem.Instance.Transition(this.StartupScene);
        }
    }
}
