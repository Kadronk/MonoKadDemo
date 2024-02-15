using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class FunnyTriangleObject : GameObject
    {
        public FunnyTriangleObject(Vector3 position, string modelFileName) {
            Position = position;
            Rotation = Quaternion.Identity;
        
            // AddRenderer<TriangleRenderer>();
            MeshRenderer meshRen = AddRenderer<MeshRenderer>();
            meshRen.Mesh = AssetLoader.GetAsset<Mesh>(modelFileName);
            meshRen.Effect = AssetLoader.GetAsset<BasicEffect>("BasicEffect");
        }
    }   
}