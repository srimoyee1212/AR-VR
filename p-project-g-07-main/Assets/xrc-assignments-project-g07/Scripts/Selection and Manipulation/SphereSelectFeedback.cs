using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRC.Assignments.Project.G07
{
    public class SphereSelectFeedback : MonoBehaviour
    {
        
        private SphereSelect m_SphereSelect;
        // [SerializeField] private Color color = new Color(0f,0f,1f,0.5f);
        // [SerializeField] private Color hoverColor = new Color(1f,0f,0f,0.5f);
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material HoverMaterial;
        private Renderer sphereRenderer;
        private SphereCollider collider;
        private GameObject sphereObject;
        //private Material defaultMaterial;
        //private Material HoverMaterial;

        
        
        
        private void Start()
        {
            m_SphereSelect = GetComponent<SphereSelect>();
            
            // defaultMaterial = new Material(Shader.Find("Standard"));
            // HoverMaterial = new Material(Shader.Find("Standard"));
            // defaultMaterial.color = new Color(color.r, color.g, color.b, 0.5f);
            // HoverMaterial.color = new Color(hoverColor.r, hoverColor.g, hoverColor.b, 0.5f);
            //
            collider = m_SphereSelect.sphereCollider;
            sphereObject = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Sphere);
            sphereObject.transform.parent = collider.transform;
            sphereObject.transform.position = collider.transform.position;
            sphereObject.transform.localScale = new Vector3(collider.radius*2, collider.radius*2, collider.radius*2);
            
            
            
            sphereRenderer = sphereObject.GetComponent<MeshRenderer>();
            




        }

        private void Update()
        {
            sphereObject.transform.localScale = new Vector3(collider.radius*2, collider.radius*2, collider.radius*2);
            if (m_SphereSelect.directInteractor.interactablesHovered.Count>0)
            {
                
                sphereRenderer.material = HoverMaterial;
            }
            else
            {
                
                sphereRenderer.material = defaultMaterial;
            }
            
            

            
        }
    }
}
