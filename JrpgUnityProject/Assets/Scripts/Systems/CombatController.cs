namespace Assets.Scripts.Systems
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Assets.Scripts.Actors;
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;
    using Assets.Scripts.Gameplay.Combat;

    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;

    public class CombatController : MonoBehaviour
    {
        private IList<CharacterActor> characterList;

        private IList<MonsterActor> monsterList;

        [SerializeField]
        private List<CombatSlot> characters;

        [SerializeField]
        private List<CombatSlot> monsters;

        private bool started = false;

        public void Awake()
        {
            ResourceProvider.Instance.RegisterResource(AssetResourceKeys.Character1AssetKey);
            CharacterActor charactorTest = new CharacterActor(0, AssetResourceKeys.Character1AssetKey, AssetResourceKeys.Character1AssetKey, AssetResourceKeys.Character1AssetKey);
            this.characterList = new List<CharacterActor>();
            this.characterList.Add(charactorTest);
            var a = this.characterList.Count;
        }

        public void InitializeCombat()
        {
            for (int i = 0; i < this.characterList.Count; i++)
            {
                CombatSlot charSlot = this.characters[i];
                if (charSlot != null)
                {
                    charSlot.Initialize(this.characterList[i]);
                }
                
            }

            //for (int i = 0; i < this.monsterList.Count; i++)
            //{
            //    CombatSlot monsterSlot = this.monsters[i];
            //    if (monsterSlot != null)
            //    {
            //        this.monsters[i].Initialize(this.monsterList[i]);
            //    }
                
            //}
        }

        public void Update()
        {
            // int pendingCount = ResourceProvider.Instance.RequestPool.ActiveRequestCount + BundleProvider.Instance.PendingForLoad;
                if (!started)
                {
                    this.started = true;
                    this.InitializeCombat();
                }
            
            // Do we need to be in combat?

            // Yes - Process combat
            //foreach (CharacterActor actor in this.characterList)
            //{
            //    switch (actor.CombatState)
            //    {
            //        case CombatState.Waiting: // not our turn just drop out
            //            break;
            //        case CombatState.StartTurn: // initialize current combat actor, ui move, whatever setup
            //            break;
            //        case CombatState.GetUserInput: // if not a mob wait for user input for action. enable ui, feedback, etc
            //            break;
            //        case CombatState.WaitingforInput: // drop out if we are still waiting on the user
            //            break;
            //        case CombatState.ProcessInput: // recieve the all from the UI on what the user chose, attacker, targets, etc
            //            break;
            //        case CombatState.ProcessCombat: // process the action, attacker, and targets and resolve combat
            //            break;
            //        case CombatState.EndTurn: // turn clean up, death here?, pass turn to next actor, reset current actor to waiting state
            //            break;
            //    }
                
            //}
        }
    }
}
