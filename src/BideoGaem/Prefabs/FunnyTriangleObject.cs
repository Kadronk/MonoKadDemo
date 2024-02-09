using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class FunnyTriangleObject : GameObject
    {
        public FunnyTriangleObject() {
            Position = Vector3.Zero;
            Rotation = Quaternion.Identity;
        
            // AddRenderer<TriangleRenderer>();
            MeshRenderer meshRen = AddRenderer<MeshRenderer>();
            meshRen.Mesh = AssetLoader.GetAsset<Mesh>("RainbowCube.fbx");
            meshRen.Effect = AssetLoader.GetAsset<BasicEffect>("BasicEffect");
        }
    }   
}