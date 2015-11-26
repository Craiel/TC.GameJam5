namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;

    using UnityEngine;

    public abstract class BasePanel : MonoBehaviour
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public abstract GameSceneType Type { get; }

        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }
    }
}
