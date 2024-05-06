 using UnityEngine;

namespace XRC.Assignments.Rendering
{
    /// <summary>
    /// Class used by MyCamera to calculate various rendering-related values
    /// </summary>
    public static class MyCameraUtil
    {
        /// <summary>
        /// Calculates the specular component of the Blinn-Phong shading model
        /// </summary>
        /// <param name="normal">Normal vector of the hit surface</param>
        /// <param name="bisector">Bisector for the specular component</param>
        /// <param name="lightColor">Color of the light being used</param>
        /// <param name="specularColor">Specular color of the object's material</param>
        /// <param name="specularExponent">The Phong exponent for the Blinn-Phong model</param>
        /// <returns>The specular component</returns>
        public static Color CalculateSpecularComponent(Vector3 normal, Vector3 bisector, Color lightColor, Color specularColor, float specularExponent)
        {
            Color specularComponent = Color.black;

            // TODO - Implement according to summary and instructions
            // <solution>
            // Your code here
            float specularProduct = Mathf.Max(Vector3.Dot(normal, bisector), 0.0f);
            float specularPower = Mathf.Pow(specularProduct, specularExponent);
            specularComponent = lightColor * specularColor * specularPower;
            // </solution>

            return specularComponent;
        }

        /// <summary>
        /// Calculate the bisector for the specular component
        /// </summary>
        /// <param name="lightDirection">Light direction</param>
        /// <param name="viewDirection">View direction</param>
        /// <returns>The bisector for the specular component</returns>
        public static Vector3 CalculateBisector(Vector3 lightDirection, Vector3 viewDirection)
        {
            Vector3 bisector = Vector3.zero;

            // TODO - Implement according to summary and instructions
            // <solution>
            // Your code here
            bisector = (lightDirection + viewDirection).normalized;
            // </solution>

            return bisector;
        }

        /// <summary>
        /// Calculates the view direction based on the ray cast into the scene from the focal point through the pixel
        /// </summary>
        /// <param name="ray">The ray from the focal point through the pixel </param>
        /// <returns>The view direction</returns>
        public static Vector3 CalculateViewDirection(Ray ray)
        {
            Vector3 viewDirection = Vector3.zero;

            // TODO - Implement according to summary and instructions
            // <solution>
            // Your code here
            viewDirection = -ray.direction;
            // </solution>

            return viewDirection;
        }

        /// <summary>
        /// Calculates the diffuse (Lambertian) component of the Blinn-Phong shading model
        /// </summary>
        /// <param name="normal">Normal vector of the hit surface</param>
        /// <param name="lightDirection"></param>
        /// <param name="lightColor">Color of the light being used</param>
        /// <param name="diffuseColor">Diffuse color of the object's material</param>
        /// <returns>The diffuse component</returns>
        public static Color CalculateDiffuseComponent(Vector3 normal, Vector3 lightDirection, Color lightColor, Color diffuseColor)
        {
            Color diffuseComponent = Color.black;

            // TODO - Implement according to summary and instructions
            // <solution>
            // Your code here
            float diffuseProduct = Mathf.Max(Vector3.Dot(normal.normalized, lightDirection.normalized),0.0f);
            diffuseComponent = lightColor * diffuseColor * diffuseProduct;
            // </solution>

            return diffuseComponent;
        }

        /// <summary>
        /// Calculates the normal based on the ray cast information and the desired shading type.
        /// This method needs to call CalculateMappedNormal(...) when the shading type requires normal mapping.
        /// When normal mapping is required, barycentric coordinates of the hit need to be provided to the
        /// CalculateMappedNormal(...) method. See the method signature for CalculateMappedNormal(...) for information
        /// on what should be passed to that method from this method.
        /// </summary>
        /// <param name="hit">The RaycastHit containing information about the ray casting</param>
        /// <param name="myShadingType">Enum indicating the desired shading type for this object</param>
        /// <param name="mesh">The mesh of the object that was hit</param>
        /// <returns></returns>
        public static Vector3 CalculateNormal(RaycastHit hit, MyShadingType myShadingType, Mesh mesh)
        {
            // The return value, to be calculated below
            Vector3 normal = Vector3.zero;
            
            // The following variables need to be set appropriately in the solution code
            // and passed on to the normal mapping method when required, depending on the shading type
            Vector3 barycenter = Vector3.zero;
            Vector3 vertexNormal1 = Vector3.zero;
            Vector3 vertextNormal2 = Vector3.zero;
            Vector3 vertextNormal3 = Vector3.zero;
            
            // TODO - Implement according to summary and instructions
            // <solution>
            // Your code here
            switch (myShadingType)
            {
                case MyShadingType.Unlit:
                case MyShadingType.Flat:
                case MyShadingType.NormalColoring:
                    normal = hit.normal;
                    break;
                case MyShadingType.Smooth:
                    barycenter = hit.barycentricCoordinate;
                    int triangleIdx = hit.triangleIndex*3;
                    vertexNormal1 = mesh.normals[mesh.triangles[triangleIdx]];
                    vertextNormal2 = mesh.normals[mesh.triangles[triangleIdx+1]];
                    vertextNormal3 = mesh.normals[mesh.triangles[triangleIdx+2]];
                    normal = CalculateMappedNormal(hit, barycenter, vertexNormal1, vertextNormal2, vertextNormal3);
                    break;
            }
            // </solution>

            return normal;
        }

        /// <summary>
        /// Calculates the light direction based on the light currently being processed and the ray cast
        /// </summary>
        /// <param name="myLight">Light being processed</param>
        /// <param name="hit">Raycast hit containing information about the ray casting</param>
        /// <returns>The light direction</returns>
        public static Vector3 CalculateLightDirection(GameObject myLight, RaycastHit hit)
        {
            Vector3 lightDirection = Vector3.zero;

            // TODO - Implement according to summary and instructions
            // <solution>
            // Your code here
            lightDirection = (myLight.transform.position - hit.point).normalized;
            // </solution>

            return lightDirection;
        }

        /// <summary>
        /// Calculates the mapped normal using the barycentric coordinates of the hit and the vertex normals 
        /// The resulting mapped normal represents a smooth surface across the primitive being hit, based on the vertex normals
        /// </summary>
        /// <param name="hit">RaycastHit containing information about the ray casting</param>
        /// <param name="barycenter">Barycentric coordinates of the hit</param>
        /// <param name="vertexNormal1">Vertex normal 1</param>
        /// <param name="vertextNormal2">Vertex normal 2</param>
        /// <param name="vertextNormal3">Vertex normal 3</param>
        /// <returns>The mapped normal vector</returns>
        private static Vector3 CalculateMappedNormal(RaycastHit hit, Vector3 barycenter, Vector3 vertexNormal1, Vector3 vertextNormal2, Vector3 vertextNormal3)
        {
            Vector3 normal = Vector3.zero;
            
            // TODO - Implement according to summary and instructions
            // <solution>
            // Your code here
            Vector3 worldNormal1 = hit.transform.TransformDirection(vertexNormal1);
            Vector3 worldNormal2 = hit.transform.TransformDirection(vertextNormal2);
            Vector3 worldNormal3 = hit.transform.TransformDirection(vertextNormal3);

            normal = worldNormal1 * barycenter.x + worldNormal2 * barycenter.y + worldNormal3 * barycenter.z;
            // </solution>
            
            return normal;
        }

        /// <summary>
        /// Calculates a color value based on the normal, where x, y, z correspond to r, g, b, respectively.
        /// The normal component values [-1, 1] are mapped to color values [0, 1] according to the assignment instructions
        /// </summary>
        /// <param name="normal">The normal vector</param>
        /// <returns>Color value based on the normal</returns>
        public static Color CalculateNormalColor(Vector3 normal)
        {
            Color outputColor = Color.black;
            
            // TODO - Implement according to summary and instructions
            // <solution>
            // Your code here
            outputColor = new Color((normal.x + 1.0f) / 2.0f, (normal.y + 1.0f) / 2.0f, (normal.z + 1.0f) / 2.0f);
            // </solution>
            
            return outputColor;
        }
    }
}