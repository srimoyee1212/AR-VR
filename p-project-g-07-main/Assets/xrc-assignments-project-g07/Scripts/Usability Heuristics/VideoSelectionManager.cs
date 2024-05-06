using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

namespace XRC.Assignments.Project.G07

{
    public class VidSelector : MonoBehaviour
    {

        public InputActionProperty m_left;
        public InputActionProperty m_right;
        public GameObject leftButton;
        public GameObject rightButton;
        public VideoClip[] videos;
        private SelectorHighlight leftHighlight;
        private SelectorHighlight rightHighlight;
        private VideoPlayer vidplay;
        private int vid;

        // Start is called before the first frame update
        void Start()
        {
            leftHighlight = leftButton.GetComponent<SelectorHighlight>();
            rightHighlight = rightButton.GetComponent<SelectorHighlight>();
            vidplay = GetComponent<VideoPlayer>();
            vid = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_left.action.WasPressedThisFrame())
            {
                if (vid + 1 < videos.Length)
                {
                    vid++;
                    vidplay.clip = videos[vid];
                }

                leftHighlight.Selected();
            }

            if (m_left.action.WasReleasedThisFrame())
            {
                leftHighlight.Deselected();
            }

            if (m_right.action.WasPressedThisFrame())
            {
                if (vid - 1 >= 0)
                {
                    vid--;
                    vidplay.clip = videos[vid];
                }

                rightHighlight.Selected();
            }

            if (m_right.action.WasReleasedThisFrame())
            {
                rightHighlight.Deselected();
            }
        }
    }

    internal class SelectorHighlight
    {
        public void Selected()
        {
            throw new System.NotImplementedException();
        }

        public void Deselected()
        {
            throw new System.NotImplementedException();
        }
    }
}