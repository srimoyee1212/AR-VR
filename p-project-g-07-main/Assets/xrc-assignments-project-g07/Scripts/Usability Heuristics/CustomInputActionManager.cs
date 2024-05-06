using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class CustomInputActionManager : MonoBehaviour
    {
        [SerializeField] private Sprite[] m_ModeSprite;
        [SerializeField] private GameObject[] m_ModeObject;
        [SerializeField] private InputActionReference m_ChangeModeInputActionReference;
        [SerializeField] private SpriteRenderer m_DynamicButtonSpriteRenderer;
        
        private float m_Counter = 0;
        private HapticFeedback m_HapticFeedback;
        
        // Start is called before the first frame update
        void Start()
        {
            m_ChangeModeInputActionReference.action.performed += ChangeModeActionPerformed;
            m_HapticFeedback = GetComponent<HapticFeedback>();
        }

        private void ChangeModeActionPerformed(InputAction.CallbackContext obj)
        {
            Vector2 thumbstickInput = obj.ReadValue<Vector2>();
            int counter = (int) m_Counter;
            Debug.Log("Counter: " + counter);
            EnableMode(counter % 2);
            m_Counter += Mathf.Abs(thumbstickInput.x * 0.1f);
        }
        
        private void EnableMode(int mode)
        {
            for (int i = 0; i < m_ModeObject.Length; i++)
            {
                if (i == mode)
                {
                    m_ModeObject[i].SetActive(true);
                    m_DynamicButtonSpriteRenderer.sprite = m_ModeSprite[i];
                    m_HapticFeedback.TriggerHaptic();
                }
                else
                {
                    m_ModeObject[i].SetActive(false);
                }
            }
        }

        private void OnEnable()
        {
            m_ChangeModeInputActionReference.action.Enable();
        }
        
        private void OnDisable()
        {
            m_ChangeModeInputActionReference.action.Disable();
        }
    }
}