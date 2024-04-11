using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class PhysicBoxObject : GameObject
    {
        public PhysicBoxObject(Vector3 position) {
            Position = position;
            
            MeshRenderer meshRen = AddRenderer<MeshRenderer>();
            meshRen.Mesh = AssetLoader.GetAsset<Mesh>("SimpleCube.fbx/Mesh");
            meshRen.Effect = AssetLoader.GetAsset<BasicEffect>("UnlitVertexColor.mat");
            AddBehaviour<RigidbodyBox>();
        }
    }
}