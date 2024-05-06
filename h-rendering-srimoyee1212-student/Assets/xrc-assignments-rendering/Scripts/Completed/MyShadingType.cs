namespace XRC.Assignments.Rendering
{
    /// <summary>
    /// Enum for indicating what the type of shading.
    /// See additional information in assignment instructions.
    /// </summary>
    public enum MyShadingType
    {
        /// <summary>
        /// Unlit discards all light and viewing information and simply colors the pixel with the unmodified diffuse color
        /// </summary>
        Unlit,

        /// <summary>
        /// NormalColoring overwrites the diffuse color with a color value based on the interpolated normal.  
        /// </summary>
        NormalColoring,

        /// <summary>
        /// Flat shading uses the actual surface normal across the whole surface in a Blinn-Phong model
        /// </summary>
        Flat,

        /// <summary>
        /// Identical to flat shading, except that smooth shading interpolates the three vertex normals, using barycentric coordinates,
        /// to calculate a mapped normal for each hit point, which gives a smooth appearance. This is normal mapping.
        /// </summary>
        Smooth
    }

}