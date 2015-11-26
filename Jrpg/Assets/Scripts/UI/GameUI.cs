namespace Assets.Scripts.UI
{
    using System.Collections;
    using System.Collections.Generic;

    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;

    using CarbonCore.Utils.Compat.Collections;
    using CarbonCore.Utils.Compat.Diagnostics;

    using UnityEngine;

    public class GameUI : MonoBehaviour
    {
        private readonly ExtendedDictionary<GameSceneType, BasePanel> panelTypeMap;

        private BasePanel activePanel;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public GameUI()
        {
            this.panelTypeMap = new ExtendedDictionary<GameSceneType, BasePanel>();
            this.panelTypeMap.EnableReverseLookup = true;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [SerializeField]
        public BasePanel LoadingPanel;

        [SerializeField]
        public BasePanel[] Panels;

        public void Awake()
        {
            // Ensure the game ui never gets destroyed
            DontDestroyOnLoad(this.gameObject);

            this.panelTypeMap.Clear();
            if (this.Panels == null || this.Panels.Length <= 0)
            {
                Diagnostic.Error("No UI Panels defined");
                return;
            }

            foreach (BasePanel panel in this.Panels)
            {
                this.RegisterPanel(panel);
            }
        }

        public void Update()
        {
            if (GameSystem.Instance.InTransition)
            {
                foreach (GameSceneType type in this.panelTypeMap.Keys)
                {
                    this.panelTypeMap[type].Hide();
                }

                this.LoadingPanel.Show();

                // Nothing else to do in transition
                return;
            }

            this.LoadingPanel.Hide();
            if (this.NeedPanelUpdate())
            {
                if (this.activePanel != null)
                {
                    this.activePanel.Hide();
                }

                this.activePanel = this.panelTypeMap[GameSystem.Instance.ActiveSceneType.Value];
                this.activePanel.Show();
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private bool NeedPanelUpdate()
        {
            if (GameSystem.Instance.ActiveSceneType == null)
            {
                // Nothing to update to
                return false;
            }

            if (this.activePanel == null)
            {
                return true;
            }

            if (this.activePanel.Type != GameSystem.Instance.ActiveSceneType.Value)
            {
                return true;
            }

            return false;
        }

        private void RegisterPanel(BasePanel panel)
        {
            if (this.panelTypeMap.ContainsKey(panel.Type))
            {
                Diagnostic.Error("Duplicate UI Panel for {0}: {1} and {2}", panel.Type, panel, this.panelTypeMap[panel.Type]);
                return;
            }

            this.panelTypeMap.Add(panel.Type, panel);
        }
    }
}
