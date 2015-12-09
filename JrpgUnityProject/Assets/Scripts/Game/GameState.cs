namespace Assets.Scripts.Game
{
    using System.Collections.Generic;

    using Assets.Scripts.Actors;
    using Assets.Scripts.Systems;

    public static class GameState
    {
        public static IList<CharacterActor> CharacterActors { get; private set; }
        public static IList<MonsterActor> MonsterActors { get; private set; } 

        public static void ClearCombatActors()
        {
            CharacterActors.Clear();
            MonsterActors.Clear();
        }

        public static void AddCharacterActor(CharacterActor actor)
        {
            CharacterActors.Add(actor);
        }

        public static void AddMonsterActor(MonsterActor actor)
        {
            MonsterActors.Add(actor);
        }
    }
}
