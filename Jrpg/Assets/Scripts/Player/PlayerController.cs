using Tiled2Unity;
namespace Assets.Scripts.Player
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;
    using Assets.Scripts.InputSystem;

    using UnityEngine;

    class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Public
        /// </summary>
        public Vector2 TargetTile;
        [SerializeField]
        public float WaitTime;
        
        private Animator PlayerAnimator;

        private const int ZOffset = 0;
        private TiledMap currentMap;
        private float updateTime;
        private enum  direction
        {
            Up,
            Down,
            Left,
            Right,
            Stay
        }

        public void Awake()
        {
            var go = GameObject.Find("test");
            this.currentMap = go.GetComponent<TiledMap>();
            Camera.main.GetComponent<PlayerCamera>().target = this.transform;
            this.PlayerAnimator = this.GetComponent<Animator>();

            this.TargetTile = Components.Instance.Player.OutdoorPosition;
            this.Move(this.TargetTile);
        }

        public void Update()
        {
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
                        TargetTile.y = Mathf.Clamp(TargetTile.y + 1,0,currentMap.NumTilesHigh-1);
                        PlayerAnimator.SetTrigger("Down");
                        break;
                    case direction.Up:
                        TargetTile.y = Mathf.Clamp(TargetTile.y - 1,0,currentMap.NumTilesHigh-1);
                        PlayerAnimator.SetTrigger("Up");
                        break;
                    case direction.Left:
                        TargetTile.x = Mathf.Clamp(TargetTile.x - 1,0,currentMap.NumTilesWide-1);
                        PlayerAnimator.SetTrigger("Left");
                        break;
                    case direction.Right:
                        TargetTile.x = Mathf.Clamp(TargetTile.x + 1,0,currentMap.NumTilesWide-1);
                        PlayerAnimator.SetTrigger("Right");
                        break;
                }
            }
        }

        public void Move(Vector2 tile)
        {
            int y = (int) tile.y * currentMap.TileHeight * -1;
            int x = (int) tile.x * currentMap.TileWidth;
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
                var xDiff = pos.x - Components.Instance.Player.OutdoorPosition.x * currentMap.TileWidth;
                var yDiff = pos.y - Components.Instance.Player.OutdoorPosition.y * currentMap.TileHeight * -1;
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
