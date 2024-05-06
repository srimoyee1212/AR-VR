using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace XRC.Assignments.Project.G07
{
    public class VideoController : MonoBehaviour
    {
        [SerializeField] private Slider m_TimelineSlider;
        [SerializeField] private TextMeshProUGUI m_CurrentTimeText;
        [SerializeField] private TextMeshProUGUI m_TotalTimeText;
        [SerializeField] private TextMeshProUGUI m_PlayPauseToggleButtonText;
        [SerializeField] private TextMeshProUGUI m_FeatureTitleText;
        
        private readonly int[] m_FeatureTimestamps = {0, 7, 16, 26, 43, 60, 68, 80};
        private readonly string[] m_FeatureTitles = {"Select Shape", "Create Object", "Duplicate Shape", "Scale Per Axis", "Color Object", "Delete Object", "Sphere Select", "Undo"};
        private string m_FeatureTitle = "Video Tutorials";
        private VideoPlayer m_VideoPlayer;
        
        // Start is called before the first frame update
        void Start()
        {
            m_VideoPlayer = GetComponent<VideoPlayer>();
            m_TotalTimeText.text = GetVideoTime((int)m_VideoPlayer.length);
            m_TimelineSlider.minValue = 0;
            m_TimelineSlider.maxValue = (float) m_VideoPlayer.length;
        }

        public void OnDropdownValueChanged(int index)
        {
            m_VideoPlayer.time = m_FeatureTimestamps[index];
            m_FeatureTitle = m_FeatureTitles[index];
        }
        
        public void OnPlayPauseButtonClicked()
        {
            if (m_VideoPlayer.isPlaying)
            {
                m_VideoPlayer.Pause();
            }
            else
            {
                m_VideoPlayer.Play();
            }
        }

        // Update is called once per frame
        void Update()
        {
            m_TimelineSlider.value = (float)m_VideoPlayer.time;
            m_CurrentTimeText.text = GetVideoTime((int)m_VideoPlayer.time);
            m_PlayPauseToggleButtonText.text = m_VideoPlayer.isPlaying ? "Pause" : "Play";
            m_FeatureTitleText.text = m_FeatureTitle;
        }
        
        private string GetVideoTime(int videoTime)
        {
            // ReSharper disable once PossibleLossOfFraction
            string hours = Mathf.Floor(videoTime / 3600).ToString("0");
            // ReSharper disable once PossibleLossOfFraction
            string minutes = Mathf.Floor(videoTime / 60).ToString("00");
            string seconds = (videoTime % 60).ToString("00");
            if (hours == "0")
            {
                return minutes + ":" + seconds;
            }
   
            return hours + ":" + minutes + ":" + seconds;
        }
    }
}