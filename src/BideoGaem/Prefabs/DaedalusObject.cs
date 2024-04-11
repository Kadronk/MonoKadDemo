using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class DaedalusObject : GameObject
    {
        public DaedalusObject(Vector3 position) {
            Position = position;
            Rotation = Quaternion.CreateFromAxisAngle(Vector3.Right, -MathF.PI / 2.0f /*-90 degrees*/);
        
            // AddRenderer<TriangleRenderer>();
            MeshRenderer meshRen = AddRenderer<MeshRenderer>();
            meshRen.Mesh = AssetLoader.GetAsset<Mesh>("DeadalusCube.fbx/Mesh");
            meshRen.Effect = AssetLoader.GetAsset<BasicEffect>("UnlitVertexColor.mat");
        }
    }   
}