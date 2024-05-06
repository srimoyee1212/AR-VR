using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class GrabMoveInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_GrabMoveInputActionReference;
        
        private GrabMove m_GrabMove;

        // Start is called before the first frame update
        void Start()
        {
            m_GrabMove = GetComponent<GrabMove>();
            m_GrabMoveInputActionReference.action.started += m_GrabMove.StartGrabMove;
            m_GrabMoveInputActionReference.action.performed += m_GrabMove.OngoingGrabMove;
            m_GrabMoveInputActionReference.action.canceled += m_GrabMove.EndGrabMove;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            m_GrabMoveInputActionReference.action.Enable();
        }
    }
}
