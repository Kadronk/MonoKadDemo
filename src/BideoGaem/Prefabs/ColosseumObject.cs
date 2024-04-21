using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class ColosseumObject : GameObject
    {
        public ColosseumObject() {
            Rotation = Quaternion.CreateFromAxisAngle(Vector3.Right, -MathF.PI / 2.0f /*-90 degrees*/);
            
            CreateMeshRendererAndRigidbody("HellColosseum.fbx/ArenaMiddlePlatform", "HellColosseumArenaMiddlePlatform.mat");
            CreateMeshRendererAndRigidbody("HellColosseum.fbx/ArenaDecor", "HellColosseumArenaDecor.mat");
            CreateMeshRendererAndRigidbody("HellColosseum.fbx/ArenaExits", "HellColosseumArenaExits.mat");
            CreateMeshRendererAndRigidbody("HellColosseum.fbx/CityBuildings.001", "HellColosseumCityBuildings.mat");
        }

        void CreateMeshRendererAndRigidbody(string meshKey, string effectKey) {
            MeshRenderer ren = AddRenderer<MeshRenderer>();
            StaticbodyMesh rb = AddBehaviour<StaticbodyMesh>();
            Mesh mesh = AssetLoader.GetAsset<Mesh>(meshKey);
            ren.Mesh = mesh;
            ren.Effect = AssetLoader.GetAsset<BasicEffect>(effectKey);
            rb.InitMesh(mesh);
        }
    }
}