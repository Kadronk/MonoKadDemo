using Microsoft.Xna.Framework;

namespace MonoKad
{
    public class BasicEffectOverrides
    {
        public string TexturePath;

        public Vector3 DiffuseColor;
        public float SpecularPower;
        public Vector3 SpecularColor;
        
        public bool LightingEnabled;
        public Vector3 AmbientLightColor;
        public DirectionalLightOverrides[] Lights = new DirectionalLightOverrides[3];
    }

    public struct DirectionalLightOverrides
    {
        public bool Enabled;
        public Vector3 Direction;
        public Vector3 DiffuseColor;
        public Vector3 SpecularColor;
    }
}