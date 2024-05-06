using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class UndoRedo : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_UndoInputActionReference;
        private Stack<UndoStackItem> m_UndoStack;
        
        // Start is called before the first frame update
        void Start()
        {
            m_UndoInputActionReference.action.performed += UndoActionPerformed;
            m_UndoStack = new Stack<UndoStackItem>();
        }

        private void UndoActionPerformed(InputAction.CallbackContext obj)
        {
            UndoStackItem undoStackItem = m_UndoStack.Pop();
            if (undoStackItem.additionalInfo == "Create Object")
            {
                Destroy(undoStackItem.gameObject);
            }
        }
        
        public void RecordAction(UndoStackItem undoStackItem)
        {
            m_UndoStack.Push(undoStackItem);
        }

        private void OnEnable()
        {
            m_UndoInputActionReference.action.Enable();
        }
        
        private void OnDisable()
        {
            m_UndoInputActionReference.action.Disable();
        }
    }
}