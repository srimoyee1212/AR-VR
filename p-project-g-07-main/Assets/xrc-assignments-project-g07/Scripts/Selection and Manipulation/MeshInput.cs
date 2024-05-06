using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeshInput : MonoBehaviour
{
    [SerializeField] private InputActionReference m_MeshManipulationInputActionReference;
    private MeshManipulation m_MeshManipulation;
    void Start()
    {
        m_MeshManipulation = GetComponent<MeshManipulation>();
        m_MeshManipulationInputActionReference.action.performed += m_MeshManipulation.MeshManipulationPerformed;
    }
    
    private void OnEnable()
    {
        m_MeshManipulationInputActionReference.action.Enable();
    }
}
