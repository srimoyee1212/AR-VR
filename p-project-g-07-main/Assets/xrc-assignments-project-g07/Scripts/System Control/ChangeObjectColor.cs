using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace XRC.Assignments.Project.G07
{
    public class ChangeObjectColor : MonoBehaviour
    {
        [SerializeField] private GameObject m_ChooseColorCanvas;
        [SerializeField] private GameObject m_RayInteractorObject;
        [SerializeField] private Button[] m_StandardColorButtons;
        [SerializeField] private Button m_RandomColorButton;
        [SerializeField] private Slider m_RedColorSlider;
        [SerializeField] private Slider m_GreenColorSlider;
        [SerializeField] private Slider m_BlueColorSlider;
        [SerializeField] private Slider m_AlphaSlider;
        [SerializeField] private InputActionReference m_CreateObjectInputActionReference;
        
        private TargetObjectProvider m_TargetObjectProvider;
        private Transform m_TargetObjectTransform;
        private Vector3 m_TargetObjectInitialPosition;
        private Quaternion m_TargetObjectInitialRotation;
        private Renderer m_TargetObjectRenderer;
        private Color m_CurrentColor = new Color(0.48f, 0.48f, 0.48f, 1f);
        
        // Start is called before the first frame update
        void Start()
        {
            m_TargetObjectProvider = GetComponent<TargetObjectProvider>();
            foreach (Button button in m_StandardColorButtons)
            {
                button.onClick.AddListener(() => UpdateTargetObjectColor(button.image.color));
            }
            m_RandomColorButton.onClick.AddListener(GenerateRandomColor);
            m_RedColorSlider.onValueChanged.AddListener(ChangeRedColor);
            m_GreenColorSlider.onValueChanged.AddListener(ChangeGreenColor);
            m_BlueColorSlider.onValueChanged.AddListener(ChangeBlueColor);
            m_AlphaSlider.onValueChanged.AddListener(ChangeAlpha);
        }

        // Update is called once per frame
        void Update()
        {
            m_RedColorSlider.value = m_CurrentColor.r;
            m_GreenColorSlider.value = m_CurrentColor.g;
            m_BlueColorSlider.value = m_CurrentColor.b;
            m_AlphaSlider.value = m_CurrentColor.a;
        }
        
        private void UpdateTargetObjectColor(Color color)
        {
            m_CurrentColor = color;
            m_TargetObjectRenderer.material.color = color;
        }
        
        private void ChangeRedColor(float value)
        {
            Color newColor = new Color(value, m_CurrentColor.g, m_CurrentColor.b, m_CurrentColor.a);
            UpdateTargetObjectColor(newColor);
        }
        
        private void ChangeGreenColor(float value)
        {
            Color newColor = new Color(m_CurrentColor.r, value, m_CurrentColor.b, m_CurrentColor.a);
            UpdateTargetObjectColor(newColor);
        }
        
        private void ChangeBlueColor(float value)
        {
            Color newColor = new Color(m_CurrentColor.r, m_CurrentColor.g, value, m_CurrentColor.a);
            UpdateTargetObjectColor(newColor);
        }
        
        private void ChangeAlpha(float value)
        {
            Color newColor = new Color(m_CurrentColor.r, m_CurrentColor.g, m_CurrentColor.b, value);
            UpdateTargetObjectColor(newColor);
        }
        
        private void GenerateRandomColor()
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            UpdateTargetObjectColor(randomColor);
        }
        
        public void ChooseColorActionPerformed(InputAction.CallbackContext obj)
        {
            m_TargetObjectProvider.ReleaseObjects();
            m_TargetObjectTransform.SetPositionAndRotation(m_TargetObjectInitialPosition, m_TargetObjectInitialRotation);
            m_ChooseColorCanvas.SetActive(!m_ChooseColorCanvas.activeSelf);
            m_RayInteractorObject.SetActive(!m_RayInteractorObject.activeSelf);
            if (m_CreateObjectInputActionReference.action.enabled)
            {
                m_CreateObjectInputActionReference.action.Disable();
            }
            else
            {
                m_CreateObjectInputActionReference.action.Enable();
            }
        }

        public void SetTargetObject(Transform targetObjectTransform, Vector3 targetObjectPosition, Quaternion targetObjectRotation)
        {
            m_TargetObjectTransform = targetObjectTransform;
            m_TargetObjectInitialPosition = targetObjectPosition;
            m_TargetObjectInitialRotation = targetObjectRotation;
            m_TargetObjectRenderer = targetObjectTransform.GetComponent<Renderer>();
        }
    }
}