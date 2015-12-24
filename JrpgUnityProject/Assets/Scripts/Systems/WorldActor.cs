﻿namespace Assets.Scripts.Systems
{
    using CarbonCore.Utils.MathUtils;
    using CarbonCore.Utils.Unity.Data;

    public class WorldActor : BaseActor
    {
        public Vector2I MapPosition { get; private set; }

        public WorldActor()
        {
        }
    }
}
