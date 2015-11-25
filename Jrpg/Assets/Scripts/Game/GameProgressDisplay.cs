namespace Assets.Scripts.Game
{
    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;
    using UnityEngine.UI;

    public class GameProgressDisplay : MonoBehaviour
    {
        private const string LoadingDetailedTextFormat = "Loading {0} remaining\n{1} +{2}";
        private const string LoadingGenericTextFormat = "Loading ...";

        private int maxProgress;

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [SerializeField]
        public GameObject SmallDisplay;

        [SerializeField]
        public GameObject FullScreenDisplay;

        [SerializeField]
        public GameObject Background;

        [SerializeField]
        public Image ProgressBar;

        [SerializeField]
        public Text ProgressText;

        [SerializeField]
        public Text ProgressDetailText;

        public void Awake()
        {
            DontDestroyOnLoad(this);

            this.SmallDisplay.SetActive(false);
            this.FullScreenDisplay.SetActive(false);
        }

        public void BeginProgress(bool small = false)
        {
            this.maxProgress = 0;

            if (small)
            {
                this.SmallDisplay.SetActive(true);
            }
            else
            {
                this.FullScreenDisplay.SetActive(true);
            }
        }

        public void EndProgress()
        {
            this.SmallDisplay.SetActive(false);
            this.FullScreenDisplay.SetActive(false);
        }

        public void Update()
        {
            float alpha = Mathf.PingPong(Time.time, 1.0f) * 1.1f;
            alpha = Mathf.Clamp(alpha, 0.03f, 1.0f);

            this.SmallDisplay.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

            bool showBackground = this.SmallDisplay.activeInHierarchy || this.FullScreenDisplay.activeInHierarchy;
            this.Background.SetActive(showBackground);

            int pendingCount = ResourceProvider.Instance.PendingForLoad + ResourceProvider.Instance.RequestPool.ActiveRequestCount + BundleProvider.Instance.PendingForLoad;
            if (pendingCount > this.maxProgress)
            {
                this.maxProgress = pendingCount;
            }

            float progress = 0f;
            if (this.maxProgress > 0)
            {
                progress = 1 - (pendingCount / (float)this.maxProgress);
            }

            this.ProgressBar.transform.localScale = new Vector3(progress, 1, 1);
            this.ProgressText.text = string.Format("{0:#,0}%", progress * 100);

            ResourceLoadRequest resourceRequest = ResourceProvider.Instance.RequestPool.GetFirstActiveRequest();
            if (resourceRequest != null)
            {
                string text = string.Format(LoadingDetailedTextFormat, pendingCount, resourceRequest.Key, ResourceProvider.Instance.RequestPool.ActiveRequestCount);
                this.ProgressDetailText.text = text;
            }
            else if (BundleProvider.Instance.CurrentRequest != null)
            {
                string text = string.Format(LoadingDetailedTextFormat, pendingCount, BundleProvider.Instance.CurrentRequest.Key, 0);
                this.ProgressDetailText.text = text;
            }
            else
            {
                this.ProgressDetailText.text = LoadingGenericTextFormat;
            }
        }
    }
}
