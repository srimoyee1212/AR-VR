using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace XRC.Assignments.Rendering
{
    /// <summary>
    /// Component for representing a camera in a ray tracer, with properties similar to a standard Unity camera
    /// </summary>
    public class MyCamera : MonoBehaviour
    {
        [SerializeField]
        private Color m_Background = Color.gray;

        [SerializeField]
        private float m_FieldOfView = 60;

        [SerializeField]
        private float m_AspectRatio = 1.0f;

        [SerializeField]
        private float m_FocalLength = 1.0f;

        [Header("RenderTextures")]
        [SerializeField]
        private RenderTexture m_TargetTexture;

        [SerializeField]
        private RenderTexture m_TargetTextureDiffuse;

        [SerializeField]
        private RenderTexture m_TargetTextureSpecular;

        [SerializeField]
        private RenderTexture m_TargetTextureAmbient;

        [Header("Global shading parameters")]
        // This should be a per-material parameter, but adding here for simplicity
        [SerializeField]
        private float m_SpecularExponent = 1.0f;

        [SerializeField]
        private Color m_AmbientColor = Color.gray;

        /// <summary>
        /// Components that can be toggled during runtime for debugging
        /// </summary>
        [Header("Components")]
        [SerializeField]
        private bool m_RenderDiffuseComponent = true;

        [SerializeField]
        private bool m_RenderSpecularComponent = true;

        [SerializeField]
        private bool m_RenderAmbientComponent = true;

        private bool m_DoRender = true;

        private LineRenderer m_LineRenderer;
        private MeshCollider m_MeshCollider;

        /// <summary>
        /// Array for storing lights in the scene
        /// </summary>
        private GameObject[] m_MyLights;

        // Arrays for pixel values
        private Color[] m_Pixels;
        private Color[] m_PixelsAmbient;
        private Color[] m_PixelsDiffuse;
        private Color[] m_PixelsSpecular;

        // Textures    
        private Texture2D m_TextureAll;
        private Texture2D m_TextureAmbient;
        private Texture2D m_TextureDiffuse;
        private Texture2D m_TextureSpecular;

        // For visualizing scene geometry
        private Ray m_RayOfInterest;
        private RaycastHit m_HitOfInterest;
        private Color m_ColorOfInterest;
        private Vector2Int m_PixelOfInterest;

        void Start()
        {
            // Set dimensions for textures
            int width = 256;
            int height = 256;
            m_TextureAll = new Texture2D(width, height);
            m_TextureAll.Apply();

            m_TextureDiffuse = new Texture2D(width, height);
            m_TextureDiffuse.Apply();

            m_TextureSpecular = new Texture2D(width, height);
            m_TextureSpecular.Apply();

            m_TextureAmbient = new Texture2D(width, height);
            m_TextureAmbient.Apply();

            m_LineRenderer = GetComponent<LineRenderer>();
            m_LineRenderer.positionCount = 5;

            // Find all lights (of type MyLights) in scene
            if (m_MyLights == null)
            {
                m_MyLights = GameObject.FindGameObjectsWithTag("MyLight");
            }

            m_Pixels = new Color[m_TextureAll.width * m_TextureAll.height];
            m_PixelsDiffuse = new Color[m_TextureAll.width * m_TextureAll.height];
            m_PixelsSpecular = new Color[m_TextureAll.width * m_TextureAll.height];
            m_PixelsAmbient = new Color[m_TextureAll.width * m_TextureAll.height];

            // Hardcoded pixel of interest for scene geometry visualizatoin
            m_PixelOfInterest = new Vector2Int(140, 50);
        }

        /// <summary>
        /// This calls the render method every frame unless the bool is changed to false.
        /// Currently the bool is not being modified but this can used to disable
        /// computationally heavy ray tracing for debugging purposes
        /// </summary>
        void Update()
        {
            if (m_DoRender)
            {
                Render();
            }
        }

        /// <summary>
        /// This method sets up most of the required rendering for the ray tracer
        /// It calls several calculation methods in MyCameraUtil.
        /// The method cycles through every pixel, and performs the ray tracing and writes back the pixel to the texture
        /// </summary>
        void Render()
        {
            // Set up the viewport and visualize it
            // Could be optimized but keeping in here to allow real-time modification of viewport for debugging
            float theta = Mathf.Deg2Rad * m_FieldOfView;
            float h = Mathf.Tan(theta / 2);
            float viewportHeight = 2.0f * h;
            float viewportWidth = m_AspectRatio * viewportHeight;
            Vector3 origin = transform.position;
            Vector3 horizontal = transform.right * viewportWidth;
            Vector3 vertical = transform.up * viewportHeight;
            Vector3 lowerLeftCorner = origin - horizontal / 2f - vertical / 2 + transform.forward * m_FocalLength;

            // Draw the viewport for visualization purposes 
            VisualizeViewport(lowerLeftCorner, vertical, horizontal);

            // This is image-order rendering, we will cycle through the pixels, starting lower left corner
            for (int j = m_TextureAll.height - 1; j >= 0; --j)
            {
                for (int i = 0; i < m_TextureAll.width - 1; ++i)
                {
                    // Set the camera background color, to be used if there is no hit during ray casting
                    m_Pixels[j * m_TextureAll.width + i] = m_Background;
                    m_PixelsDiffuse[j * m_TextureAll.width + i] = m_Background;
                    m_PixelsSpecular[j * m_TextureAll.width + i] = m_Background;
                    m_PixelsAmbient[j * m_TextureAll.width + i] = m_Background;

                    float u = (float)i / (float)(m_TextureAll.width - 1);
                    float v = (float)j / (float)(m_TextureAll.height - 1);
                    var pixelPosition = ((lowerLeftCorner + u * horizontal + v * vertical));

                    // Prepare ray casting
                    Ray ray = new Ray(origin, pixelPosition - origin);
                    RaycastHit hit;

                    // Perform ray casting
                    if (Physics.Raycast(ray, out hit))
                    {
                        // Get the mesh collider for the registered hit
                        m_MeshCollider = hit.collider as MeshCollider;

                        // Check if the hit object has a valid MeshCollider
                        if (m_MeshCollider == null || m_MeshCollider.sharedMesh == null)
                        {
                            continue;
                        }

                        // Try to get the MyMaterial component of the hit object
                        MyMaterial myMaterial = m_MeshCollider.GetComponent<MyMaterial>();

                        // Check if the hit object has a MyMaterial component
                        if (myMaterial == null)
                        {
                            continue;
                        }

                        // Get the material colors; diffuse and specular
                        Color diffuseColor = myMaterial.diffuseColor;
                        Color specularColor = myMaterial.specularColor;

                        // Get the shading type from the material
                        MyShadingType myShadingType = myMaterial.myShadingType;

                        // Calculate the normal according to the shading type
                        Vector3 normal = MyCameraUtil.CalculateNormal(hit, myShadingType, m_MeshCollider.sharedMesh);

                        // Calculate the view direction. We can do this outside of the light iteration loop
                        Vector3 viewDirection = MyCameraUtil.CalculateViewDirection(ray);

                        // Initialize pixel arrays
                        Color pixelColor = Color.black;
                        Color pixelColorDiffuse = Color.black;
                        Color pixelColorSpecular = Color.black;
                        Color pixelColorAmbient = Color.black;

                        // Cycle through all the lights in the scene, and calculate the pixel color
                        foreach (GameObject myLight in m_MyLights)
                        {
                            // Check if light is active, if not then continue without shading
                            // This allows us to turn off lights while in play mode
                            if (!myLight.activeSelf)
                            {
                                continue;
                            }

                            // Get the color for the light currently being processed
                            Color lightColor = myLight.GetComponent<MyLight>().color;

                            // Calculate additinal values needed for components
                            Vector3 lightDirection = MyCameraUtil.CalculateLightDirection(myLight, hit);
                            Vector3 bisector = MyCameraUtil.CalculateBisector(lightDirection, viewDirection);

                            // Calculate the components
                            Color diffuseComponent = MyCameraUtil.CalculateDiffuseComponent(normal, lightDirection, lightColor, diffuseColor);
                            Color specularComponent = MyCameraUtil.CalculateSpecularComponent(normal, bisector, lightColor, specularColor, m_SpecularExponent);

                            // Assign pixel colors based on the shading type
                            switch (myShadingType)
                            {
                                case MyShadingType.Unlit:
                                    pixelColor = diffuseColor;
                                    break;
                                case MyShadingType.NormalColoring:
                                    Color normalColor = MyCameraUtil.CalculateNormalColor(hit.transform.InverseTransformDirection(normal));
                                    pixelColor = normalColor;
                                    break;
                                case MyShadingType.Flat:
                                case MyShadingType.Smooth:
                                    if (m_RenderDiffuseComponent)
                                    {
                                        pixelColor += diffuseComponent;
                                        pixelColorDiffuse += diffuseComponent;
                                    }

                                    if (m_RenderSpecularComponent)
                                    {
                                        pixelColor += specularComponent;
                                        pixelColorSpecular += specularComponent;
                                    }

                                    if (m_RenderAmbientComponent)
                                    {
                                        pixelColor += m_AmbientColor;
                                        pixelColorAmbient += m_AmbientColor;
                                    }

                                    break;
                            }

                            // Assign the colors to their corresponding pixel array
                            m_Pixels[j * m_TextureAll.width + i] = pixelColor;
                            m_PixelsDiffuse[j * m_TextureAll.width + i] = pixelColorDiffuse;
                            m_PixelsSpecular[j * m_TextureAll.width + i] = pixelColorSpecular;
                            m_PixelsAmbient[j * m_TextureAll.width + i] = pixelColorAmbient;

                            // For visualization of scene geometry, not needed for actual ray tracing
                            if (j == m_PixelOfInterest.y && i == m_PixelOfInterest.x)
                            {
                                m_RayOfInterest = ray;
                                m_HitOfInterest = hit;
                                m_ColorOfInterest = pixelColor;
                            }
                        }
                    }
                }
            }

            // Texture management. Set pixels of various Texture2Ds and assign to render textures

            m_TextureAll.SetPixels(m_Pixels);
            m_TextureAll.Apply();
            Graphics.Blit(m_TextureAll, m_TargetTexture);

            m_TextureDiffuse.SetPixels(m_PixelsDiffuse);
            m_TextureDiffuse.Apply();
            Graphics.Blit(m_TextureDiffuse, m_TargetTextureDiffuse);

            m_TextureSpecular.SetPixels(m_PixelsSpecular);
            m_TextureSpecular.Apply();
            Graphics.Blit(m_TextureSpecular, m_TargetTextureSpecular);

            m_TextureAmbient.SetPixels(m_PixelsAmbient);
            m_TextureAmbient.Apply();
            Graphics.Blit(m_TextureAmbient, m_TargetTextureAmbient);
        }

        /// <summary>
        /// Method to visualize the viewport by setting positions of a line renderer
        /// </summary>
        /// <param name="lowerLeftCorner"></param>
        /// <param name="vertical"></param>
        /// <param name="horizontal"></param>
        private void VisualizeViewport(Vector3 lowerLeftCorner, Vector3 vertical, Vector3 horizontal)
        {
            m_LineRenderer.SetPosition(0, lowerLeftCorner);
            m_LineRenderer.SetPosition(1, lowerLeftCorner + vertical);
            m_LineRenderer.SetPosition(2, lowerLeftCorner + vertical + horizontal);
            m_LineRenderer.SetPosition(3, lowerLeftCorner + horizontal);
            m_LineRenderer.SetPosition(4, lowerLeftCorner);
        }

        /// <summary>
        /// Public method that allows other scripts to access data for visualizing ray casting and shading
        /// Only used for visualization, not for actual ray tracing
        /// </summary>
        /// <returns></returns>
        public (Vector3 origin, Vector3 hitPoint, Color color) GetVisualizerData()
        {
            return (m_RayOfInterest.origin, m_HitOfInterest.point, m_ColorOfInterest);
        }
    }
}