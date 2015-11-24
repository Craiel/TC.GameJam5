namespace Assets.Scripts
{
    using System.Collections;

    using JetBrains.Annotations;

    using Jrpg.Game;
    using Jrpg.Game.Contracts;

    using UnityEngine;
    
    public class GameInit : MonoBehaviour
    {
        private IGame game;

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static GameInit Instance;

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        [UsedImplicitly]
        private void Start()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(this);
            
            /*this.game = Core.Factory.Resolve<IGame>();
            this.game.UnityLink = this;
            this.game.Initialize();*/
        }

        [UsedImplicitly]
        private void Update()
        {
            this.game.Update(Time.time);
        }
        
        public void Run(IEnumerator coroutine)
        {
            this.StartCoroutine(coroutine);
        }

        public float RandomRange(float min, float max)
        {
            return Random.Range(min, max);
        }
    }
}
