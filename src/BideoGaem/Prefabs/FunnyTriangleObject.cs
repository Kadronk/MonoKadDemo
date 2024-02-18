using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class FunnyTriangleObject : GameObject
    {
        public FunnyTriangleObject(Vector3 position) {
            Position = position;
            Rotation = Quaternion.CreateFromAxisAngle(Vector3.Right, -MathF.PI / 2.0f /*-90 degrees*/);
        
            // AddRenderer<TriangleRenderer>();
            MeshRenderer meshRen = AddRenderer<MeshRenderer>();
            meshRen.Mesh = AssetLoader.GetAsset<Mesh>("RainbowCube.fbx/Mesh");
            meshRen.Effect = new BasicEffect(KadGame.Instance.GraphicsDevice) { VertexColorEnabled = true };
        }
    }   
}