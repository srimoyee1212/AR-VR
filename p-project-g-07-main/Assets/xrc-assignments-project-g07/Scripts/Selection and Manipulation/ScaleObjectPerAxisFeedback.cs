using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class ScaleObjectPerAxisFeedback : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_ScaleAndCheckMarkSpriteRenderer;
        [SerializeField] private InputActionReference m_ScaleObjectPerAxisInputActionReference;
        [SerializeField] private Sprite m_CheckMarkSprite;
        [SerializeField] private Sprite m_ScaleButtonSprite;

        // Start is called before the first frame update
        void Start()
        {
            m_ScaleObjectPerAxisInputActionReference.action.performed += ScaleObjectPerAxisActionPerformed;
        }

        private void ScaleObjectPerAxisActionPerformed(InputAction.CallbackContext obj)
        {
            if (m_ScaleAndCheckMarkSpriteRenderer.sprite == m_ScaleButtonSprite)
            {
                m_ScaleAndCheckMarkSpriteRenderer.sprite = m_CheckMarkSprite;
            }
            else
            {
                m_ScaleAndCheckMarkSpriteRenderer.sprite = m_ScaleButtonSprite;
            }
        }
    }
}