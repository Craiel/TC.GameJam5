﻿using Tiled2Unity;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerController : MonoBehaviour
    {

        PlayerController()
        {
            CurrentTile = new Vector2(0,0);
        }
        /// <summary>
        /// Public
        /// </summary>
        public Vector2 CurrentTile;
        public Vector2 TargetTile;
        [SerializeField]
        public float WaitTime;
        [SerializeField]
        public Animator PlayerAnimator;

        private const int ZOffset = 0;
        private TiledMap currentMap;
        private float updateTime;
        private enum  direction
        {
            up,
            down,
            left,
            right,
            stay
        }

        public void Awake()
        {

            this.CurrentTile.x = 6;
            this.CurrentTile.y = 1;
            this.TargetTile.x = 6;
            this.TargetTile.y = 1;
            var go = GameObject.Find("test");
            this.currentMap = go.GetComponent<TiledMap>();
            Camera.main.GetComponent<PlayerCamera>().target = this.transform;
            this.Move(TargetTile);
        }

        public void Update()
        {
            if (CurrentTile != TargetTile)
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
                    case direction.down:
                        TargetTile.y = Mathf.Clamp(TargetTile.y + 1,0,currentMap.NumTilesHigh-1);
                        PlayerAnimator.SetTrigger("down");
                        break;
                    case direction.up:
                        TargetTile.y = Mathf.Clamp(TargetTile.y - 1,0,currentMap.NumTilesHigh-1);
                        PlayerAnimator.SetTrigger("up");
                        break;
                    case direction.left:
                        TargetTile.x = Mathf.Clamp(TargetTile.x - 1,0,currentMap.NumTilesWide-1);
                        PlayerAnimator.SetTrigger("left");
                        break;
                    case direction.right:
                        TargetTile.x = Mathf.Clamp(TargetTile.x + 1,0,currentMap.NumTilesWide-1);
                        PlayerAnimator.SetTrigger("right");
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
            CurrentTile = TargetTile;

        }

        private direction ProcessInput()
        {
            if (Input.inputString != "")
            {
                KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), Input.inputString.ToUpper());
                switch (key)
                {
                    case KeyCode.S:
                        return direction.down;
                    case KeyCode.W:
                        return direction.up;
                    case KeyCode.A:
                        return direction.left;
                    case KeyCode.D:
                        return direction.right;
                }
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                var pos = Input.mousePosition;
                pos = Camera.main.ScreenToWorldPoint(pos);
                var xDiff = pos.x - CurrentTile.x * currentMap.TileWidth;
                var yDiff = pos.y - CurrentTile.y * currentMap.TileHeight * -1;
                if (Mathf.Abs(xDiff) > Mathf.Abs(yDiff)) //Moving left or right
                {
                    if (xDiff > 0)
                    {
                        return direction.right;
                    }
                    else
                    {
                        return direction.left;
                    }
                    
                }
                else // Moving up or down
                {
                    if (yDiff > 0)
                    {
                        return direction.up;
                    }
                    else
                    {
                        return direction.down;
                    }

                }
            }
            

            return direction.stay;
        }
    }
}