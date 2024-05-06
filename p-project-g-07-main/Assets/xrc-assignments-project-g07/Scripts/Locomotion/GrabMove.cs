using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class GrabMove : MonoBehaviour
    {
        [SerializeField] private List<GameObject> m_Environments;
        [SerializeField] private Transform m_LeftControllerTransform;
        [SerializeField] private Transform m_RightControllerTransform;
        [SerializeField] private GameObject m_XROrigin;
        [SerializeField] private float m_MinScale;
        [SerializeField] private float m_MaxScale;
        
        // private Vector3 m_InitialScale;
        // private Vector3 m_PivotPoint;
        //
        // private Vector3 m_LeftControllerPosition;
        // private Vector3 m_RightControllerPosition;
        // private Vector3 m_PrevPivotPoint;
        // private float m_CurrentScale = 1f;
        // private float m_prevDistance = 0f;

        public HapticFeedback m_HapticFeedback;
        public Vector3 m_PivotPosition;

        private Vector3 m_CurrentLeftControllerForward;
        private Vector3 m_CurrentRightControllerForward;
        private Vector3 m_PrevLeftControllerForward;
        private Vector3 m_PrevRightControllerForward;
        private Vector3 m_PrevPivotPosition;
        private float m_InitialDistanceBetweenControllers;
        private float m_InitialOriginScale;
        private Vector3 m_AverageControllerForward;
        private Vector3 m_PrevAverageControllerForward;
        private Vector3 m_LineRotationVector;
        private Vector3 m_PrevLineRotationVector;
        
        private GrabMoveFeedback m_GrabMoveFeedback;
        
        // Start is called before the first frame update
        void Start()
        {
            foreach (GameObject env in m_Environments)
            {
                env.transform.SetParent(m_XROrigin.transform);
            }
            
            m_GrabMoveFeedback = GetComponent<GrabMoveFeedback>();
            m_HapticFeedback = GetComponent<HapticFeedback>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        public void StartGrabMove(InputAction.CallbackContext context)
        {
            Vector3 leftControllerPosition = m_LeftControllerTransform.position;
            Vector3 rightControllerPosition = m_RightControllerTransform.position;
            m_PivotPosition = (leftControllerPosition + rightControllerPosition) / 2f;
            m_InitialDistanceBetweenControllers = Vector3.Distance(leftControllerPosition, rightControllerPosition);
            m_InitialOriginScale = m_XROrigin.transform.localScale.x;
            m_AverageControllerForward = (m_LeftControllerTransform.forward + m_RightControllerTransform.forward) / 2f;
            m_LineRotationVector = (rightControllerPosition - leftControllerPosition).normalized;
        }

        public void OngoingGrabMove(InputAction.CallbackContext context)
        { 
            m_GrabMoveFeedback.DrawSphere(m_PivotPosition);
            m_GrabMoveFeedback.DrawLine(m_LeftControllerTransform.position, m_RightControllerTransform.position);
            
            float distanceBetweenControllers = Vector3.Distance(m_LeftControllerTransform.position, m_RightControllerTransform.position);
            float targetScale = distanceBetweenControllers != 0f 
                ? m_InitialOriginScale * (m_InitialDistanceBetweenControllers / distanceBetweenControllers)
                : m_XROrigin.transform.localScale.x;
            Debug.Log("Unclamped Target Scale: " + targetScale);
            targetScale = Mathf.Clamp(targetScale, m_MinScale, m_MaxScale);
            Debug.Log("Target Scale: " + targetScale);
            
            m_XROrigin.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            
            Vector3 leftControllerPosition = m_LeftControllerTransform.position;
            Vector3 rightControllerPosition = m_RightControllerTransform.position;
            
            m_PrevPivotPosition = m_PivotPosition;
            m_PivotPosition = (leftControllerPosition + rightControllerPosition) / 2;
            
            // m_XROrigin.transform.Translate(m_PivotPosition - m_PrevPivotPosition, Space.World);
            Vector3 translation = m_PrevPivotPosition - m_PivotPosition;
            m_XROrigin.transform.position += translation;
            
            m_PrevAverageControllerForward = m_AverageControllerForward;
            m_AverageControllerForward = (m_LeftControllerTransform.forward + m_RightControllerTransform.forward) / 2f;
            // Vector3 axisOfOrthogonalRotation = Vector3.Cross(m_PrevAverageControllerForward, m_AverageControllerForward).normalized;
            Vector3 axisOfOrthogonalRotation = (rightControllerPosition - leftControllerPosition).normalized;
            
            // Project the vectors onto a plane defined by the rotation axis
            Vector3 previousProjected = Vector3.ProjectOnPlane(m_PrevAverageControllerForward, axisOfOrthogonalRotation).normalized;
            Vector3 currentProjected = Vector3.ProjectOnPlane(m_AverageControllerForward, axisOfOrthogonalRotation).normalized;
            
            float orthogonalRotationAngle = Vector3.SignedAngle(previousProjected,
                currentProjected, axisOfOrthogonalRotation);
            m_XROrigin.transform.RotateAround(m_PivotPosition, axisOfOrthogonalRotation, -orthogonalRotationAngle);
            
            m_PrevLineRotationVector = m_LineRotationVector;
            m_LineRotationVector = (rightControllerPosition - leftControllerPosition).normalized;
            Vector3 axisOfLineRotation = Vector3.Cross(m_PrevLineRotationVector, m_LineRotationVector).normalized;
            float deltaAngle = Vector3.SignedAngle(m_PrevLineRotationVector, m_LineRotationVector, axisOfLineRotation);
            deltaAngle *= 0.7f;
            m_XROrigin.transform.RotateAround(m_PivotPosition, axisOfLineRotation, deltaAngle);

            leftControllerPosition = m_LeftControllerTransform.position;
            rightControllerPosition = m_RightControllerTransform.position;
            
            // m_InitialDistanceBetweenControllers = Vector3.Distance(leftControllerPosition, rightControllerPosition);
            m_PivotPosition = (leftControllerPosition + rightControllerPosition) / 2f;
            m_AverageControllerForward = (m_LeftControllerTransform.forward + m_RightControllerTransform.forward) / 2f;
            
            m_GrabMoveFeedback.DrawSphere(m_PivotPosition);
            m_GrabMoveFeedback.DrawLine(leftControllerPosition, rightControllerPosition);
            m_HapticFeedback.TriggerHaptic();
        }

        public void EndGrabMove(InputAction.CallbackContext context)
        {
            m_GrabMoveFeedback.DisableSphere();
            m_GrabMoveFeedback.DisableLine();
        }
    }
}