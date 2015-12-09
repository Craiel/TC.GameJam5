namespace Assets.Scripts.Systems
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Assets.Scripts.Actors;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;
    using Assets.Scripts.Gameplay.Combat;

    using UnityEngine;

    public class CombatController : MonoBehaviour
    {
        private IList<CharacterActor> characterList;

        private IList<MonsterActor> monsterList;

        [SerializeField]
        private List<CombatSlot> characters;

        [SerializeField]
        private List<CombatSlot> monsters;

        public void InitializeCombat()
        {
            for (int i = 0; i < this.characterList.Count; i++)
            {
                this.characters[i].Initialize(this.characterList[i]);
            }

            for (int i = 0; i < this.monsterList.Count; i++)
            {
                this.monsters[i].Initialize(this.monsterList[i]);
            }
            //grab on the combat actors into an IList

            //sort the list into our list by initiative
            //this.characterList = allActors.OrderBy(o => o.Initiative).ToList();
        }

        public void Update()
        {
            // Do we need to be in combat?
            var count = 0;
            foreach (CharacterActor actor in this.characterList)
            {
                // If we have a Monster in our list, keep going
                if (actor.ActorType == ActorType.Monster)
                {
                    count++;
                    return;
                }
                    
            }
            if (count == 0)
            {
                // We are done with combat, dont go any furter 
            }

            // Yes - Process combat
            foreach (CharacterActor actor in this.characterList)
            {
                switch (actor.CombatState)
                {
                    case CombatState.Waiting: // not our turn just drop out
                        break;
                    case CombatState.StartTurn: // initialize current combat actor, ui move, whatever setup
                        break;
                    case CombatState.GetUserInput: // if not a mob wait for user input for action. enable ui, feedback, etc
                        break;
                    case CombatState.WaitingforInput: // drop out if we are still waiting on the user
                        break;
                    case CombatState.ProcessInput: // recieve the all from the UI on what the user chose, attacker, targets, etc
                        break;
                    case CombatState.ProcessCombat: // process the action, attacker, and targets and resolve combat
                        break;
                    case CombatState.EndTurn: // turn clean up, death here?, pass turn to next actor, reset current actor to waiting state
                        break;
                }
                
            }
        }
    }
}
