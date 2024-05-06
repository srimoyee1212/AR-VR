using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Locomotion
{
    /// <summary>
    /// Defines the movement modes. 
    /// </summary>
    public enum MovementMode
    {
        None,
        ContinuousMovement,
        Teleportation
    };

    /// <summary>
    /// Defines the rotation modes. 
    /// </summary>
    public enum RotationMode
    {
        None,
        ContinuousTurn,
        SnapTurn
    };

    /// <summary>
    /// Locomotion manager that stores necessary references for the locomotion controller,
    /// and handles technique transitions.
    /// </summary>
    public class LocomotionManager : MonoBehaviour
    {
        [Tooltip("Input Action Asset")] [SerializeField]
        private InputActionAsset m_InputAction = null;

        [Tooltip("Root of the player rig")] [SerializeField]
        private GameObject m_PlayerRig;

        [Tooltip("The player hand aim that Tracked Pose Driver is attached to")] [SerializeField]
        private GameObject m_PlayerHandAim;

        /// <summary>
        /// The input action asset. 
        /// </summary>
        public InputActionAsset inputAction
        {
            get => m_InputAction;
        }

        /// <summary>
        /// The player rig (the root of the player). 
        /// </summary>
        public GameObject playerRig
        {
            get => m_PlayerRig;
        }

        /// <summary>
        /// The player hand aim that follows the movement of player hand. 
        /// </summary>
        public GameObject playerHandAim
        {
            get => m_PlayerHandAim;
        }

        /// <summary>
        /// Whether the locomotion mode is just changed.
        /// </summary>
        public bool modeChanged { get; set; }

        private InputActionReference m_Action_SnapTurn = null;
        private InputActionReference m_Action_ContinuousTurn = null;
        private InputActionReference m_Action_ContinuousMovement = null;
        private InputActionReference m_Action_Teleportation = null;

        private SnapTurnController m_SnapTurnController;
        private ContinuousTurnController m_ContinuousTurnController;
        private ContinuousMovementController m_ContinuousMovementController;
        private TeleportationController m_TeleportationController;

        private MovementMode m_MovementMode;
        private RotationMode m_RotationMode;

        /// <summary>
        /// The movement technique.
        /// </summary>
        public MovementMode movementMode
        {
            get => m_MovementMode;
        }

        /// <summary>
        /// The rotation technique.
        /// </summary>
        public RotationMode rotationMode
        {
            get => m_RotationMode;
        }

        private void Awake()
        {
            m_Action_SnapTurn = InputActionReference.Create(m_InputAction["RightHand/PrimaryButton"]);
            m_Action_ContinuousTurn = InputActionReference.Create(m_InputAction["RightHand/SecondaryButton"]);
            m_Action_ContinuousMovement = InputActionReference.Create(m_InputAction["LeftHand/SecondaryButton"]);
            m_Action_Teleportation = InputActionReference.Create(m_InputAction["LeftHand/PrimaryButton"]);

            m_SnapTurnController = GetComponent<SnapTurnController>();
            m_ContinuousMovementController = GetComponent<ContinuousMovementController>();
            m_ContinuousTurnController = GetComponent<ContinuousTurnController>();
            m_TeleportationController = GetComponent<TeleportationController>();
        }

        protected virtual void OnEnable()
        {
            if (m_Action_SnapTurn) m_Action_SnapTurn.action.Enable();
            if (m_Action_ContinuousTurn) m_Action_ContinuousTurn.action.Enable();
            if (m_Action_ContinuousMovement) m_Action_ContinuousMovement.action.Enable();
            if (m_Action_Teleportation) m_Action_Teleportation.action.Enable();

            m_Action_SnapTurn.action.started += SnapTurn;
            m_Action_ContinuousTurn.action.started += ContinuousTurn;
            m_Action_ContinuousMovement.action.started += ContinuousMovement;
            m_Action_Teleportation.action.started += Teleportation;

            InitializeModes();
        }

        protected virtual void OnDisable()
        {
            if (m_Action_SnapTurn) m_Action_SnapTurn.action.Disable();
            if (m_Action_ContinuousTurn) m_Action_ContinuousTurn.action.Disable();
            if (m_Action_ContinuousMovement) m_Action_ContinuousMovement.action.Disable();
            if (m_Action_Teleportation) m_Action_Teleportation.action.Disable();
            m_Action_SnapTurn.action.started -= SnapTurn;
            m_Action_ContinuousTurn.action.started -= ContinuousTurn;
            m_Action_ContinuousMovement.action.started -= ContinuousMovement;
            m_Action_Teleportation.action.started -= Teleportation;
        }

        /// <summary>
        /// Updates the rotation mode to snap turn.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context.</param>
        private void SnapTurn(InputAction.CallbackContext ctx)
        {
            m_ContinuousTurnController.enabled = false;
            m_SnapTurnController.enabled = true;
            m_RotationMode = RotationMode.SnapTurn;
            modeChanged = true;
        }

        /// <summary>
        /// Updates the rotation mode to continuous turn.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context.</param>
        private void ContinuousTurn(InputAction.CallbackContext ctx)
        {
            m_SnapTurnController.enabled = false;
            m_ContinuousTurnController.enabled = true;
            m_RotationMode = RotationMode.ContinuousTurn;
            modeChanged = true;
        }

        /// <summary>
        /// Updates the movement mode to continuous movement.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context.</param>
        private void ContinuousMovement(InputAction.CallbackContext ctx)
        {
            m_TeleportationController.enabled = false;
            m_ContinuousMovementController.enabled = true;
            m_MovementMode = MovementMode.ContinuousMovement;
            modeChanged = true;
        }

        /// <summary>
        /// Updates the movement mode to teleportation.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context.</param>
        private void Teleportation(InputAction.CallbackContext ctx)
        {
            m_ContinuousMovementController.enabled = false;
            m_TeleportationController.enabled = true;
            m_MovementMode = MovementMode.Teleportation;
            modeChanged = true;
        }

        /// <summary>
        /// Initializes the modes. Called on start.
        /// </summary>
        private void InitializeModes()
        {
            if (m_SnapTurnController.enabled)
            {
                m_RotationMode = RotationMode.SnapTurn;
                m_ContinuousTurnController.enabled = false;
            }
            else if (m_ContinuousTurnController.enabled) m_RotationMode = RotationMode.ContinuousTurn;
            else m_RotationMode = RotationMode.None;

            if (m_TeleportationController.enabled)
            {
                m_MovementMode = MovementMode.Teleportation;
                m_ContinuousMovementController.enabled = false;
            }
            else if (m_ContinuousMovementController.enabled) m_MovementMode = MovementMode.ContinuousMovement;
            else m_MovementMode = MovementMode.None;

            modeChanged = true;
        }
    }
}