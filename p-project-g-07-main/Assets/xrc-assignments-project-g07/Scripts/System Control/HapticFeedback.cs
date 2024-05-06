using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRC.Assignments.Project.G07
{
    public class HapticFeedback : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float m_Intensity = 0.5f;
        [SerializeField] private float m_Duration = 0.1f;
        [SerializeField] private XRBaseController m_LeftHandController;
        [SerializeField] private XRBaseController m_RightHandController;
        
        public void TriggerHaptic()
        {
            if (m_Intensity > 0)
            {
                if (m_LeftHandController)
                {
                    m_LeftHandController.SendHapticImpulse(m_Intensity, m_Duration);   
                }

                if (m_RightHandController)
                {
                    m_RightHandController.SendHapticImpulse(m_Intensity, m_Duration);   
                }
            }
        }
    }
}