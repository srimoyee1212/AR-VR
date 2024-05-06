using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

namespace XRC.Assignments.Project.G07
{
    public class VideoScript : MonoBehaviour
    {
        public GameObject parentOfVideoPlayer;

        public VideoPlayer vpObj;

        public List<VideoClip> listClip;

        public int videoID;

        private int _currentClip;

        // Start is called before the first frame update
        void Start()
        {
        }

        void Update()
        {
            if (videoID == 0)
                parentOfVideoPlayer.SetActive(false);
            else if (_currentClip != videoID)
            {
                vpObj.clip = listClip[videoID - 1];
                _currentClip = videoID;
            }

        }
    }
}