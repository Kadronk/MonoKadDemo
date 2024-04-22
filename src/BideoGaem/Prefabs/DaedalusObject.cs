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

            Mesh mesh = AssetLoader.GetAsset<Mesh>("DeadalusCube.fbx/Mesh");
            MeshRenderer meshRen = AddRenderer<MeshRenderer>();
            meshRen.Mesh = mesh;
            meshRen.Effect = AssetLoader.GetAsset<BasicEffect>("UnlitVertexColor.mat");
            StaticbodyMesh col = AddBehaviour<StaticbodyMesh>();
            col.InitMesh(mesh);
        }
    }   
}