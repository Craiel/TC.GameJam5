namespace Assets.Scripts.Player
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;
    using Assets.Scripts.Gameplay.Map;
    using Assets.Scripts.InputSystem;

    using CarbonCore.Utils.MathUtils;

    using UnityEngine;

    class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Public
        /// </summary>
        public Vector2I TargetTile;
        [SerializeField]
        public float WaitTime;
        
        private Animator PlayerAnimator;

        private bool initialized;
        private Vector2I mapSize;
        private Vector2I mapTileSize;
        private const int ZOffset = 0;
        private float updateTime;
        private enum  direction
        {
            Up,
            Down,
            Left,
            Right,
            Stay
        }

        public void Update()
        {
            // Todo: this should be refactored so we can consciously start the controller instead of a passive awake
            if (!this.initialized)
            {
                OutdoorController controller = Components.Instance.GetComponent<OutdoorController>();
                if (controller == null)
                {
                    // not ready yet...
                    return;
                }

                this.mapSize = controller.MapSize;
                this.mapTileSize = controller.MapTileSize;

                Camera.main.GetComponent<PlayerCamera>().target = this.transform;
                this.PlayerAnimator = this.GetComponent<Animator>();

                this.TargetTile = Components.Instance.Player.OutdoorPosition;
                this.Move(this.TargetTile);
                this.initialized = true;
            }

            if (Components.Instance.Player.OutdoorPosition != TargetTile)
            {
                Move(TargetTile);
            }
            //update only so often
            //(Input.inputString != "" || Input.GetMouseButtonDown(0)) && 
            if (updateTime < Time.time)
            {
                var dir = ProcessInput();
                //KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), Input.inputString.ToUpper());
                switch (dir)
                {
                    case direction.Down:
                        TargetTile = new Vector2I(this.TargetTile.X, Mathf.Clamp(TargetTile.Y - 1, 0, this.mapSize.Y - 1));
                        PlayerAnimator.SetTrigger("Down");
                        break;
                    case direction.Up:
                        TargetTile = new Vector2I(this.TargetTile.X, Mathf.Clamp(TargetTile.Y + 1, 0, this.mapSize.Y - 1));
                        PlayerAnimator.SetTrigger("Up");
                        break;
                    case direction.Left:
                        TargetTile = new Vector2I(Mathf.Clamp(TargetTile.X - 1, 0, this.mapSize.X - 1), this.TargetTile.Y);
                        PlayerAnimator.SetTrigger("Left");
                        break;
                    case direction.Right:
                        TargetTile = new Vector2I(Mathf.Clamp(TargetTile.X + 1, 0, this.mapSize.X - 1), this.TargetTile.Y);
                        PlayerAnimator.SetTrigger("Right");
                        break;
                }
            }
        }

        public void Move(Vector2I tile)
        {
            int y = tile.Y * this.mapTileSize.Y;
            int x = tile.X * this.mapTileSize.X;
            transform.position = new Vector3(x,y,ZOffset);
            updateTime = Time.time + WaitTime;
            Components.Instance.Player.OutdoorPosition = TargetTile;
            Components.Instance.Audio.PlayOneShot(AssetResourceKeys.SfxFootstepsAssetKey, GameAudioType.Sfx);
        }

        private direction ProcessInput()
        {
            if (InputHandler.Instance.GetState(Controls.MoveUp).IsPressed)
            {
                return direction.Up;
            }

            if (InputHandler.Instance.GetState(Controls.MoveDown).IsPressed)
            {
                return direction.Down;
            }

            if (InputHandler.Instance.GetState(Controls.MoveLeft).IsPressed)
            {
                return direction.Left;
            }

            if (InputHandler.Instance.GetState(Controls.MoveRight).IsPressed)
            {
                return direction.Right;
            }
            
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                var pos = Input.mousePosition;
                pos = Camera.main.ScreenToWorldPoint(pos);
                var xDiff = pos.x - Components.Instance.Player.OutdoorPosition.X * this.mapTileSize.X;
                var yDiff = pos.y - Components.Instance.Player.OutdoorPosition.Y * this.mapTileSize.Y;
                if (Mathf.Abs(xDiff) > Mathf.Abs(yDiff)) //Moving Left or Right
                {
                    if (xDiff > 0)
                    {
                        return direction.Right;
                    }
                    else
                    {
                        return direction.Left;
                    }
                    
                }
                else // Moving Up or Down
                {
                    if (yDiff > 0)
                    {
                        return direction.Up;
                    }
                    else
                    {
                        return direction.Down;
                    }

                }
            }
            

            return direction.Stay;
        }
    }
}
