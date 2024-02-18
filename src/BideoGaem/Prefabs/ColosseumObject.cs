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
            
            MeshRenderer ren;
            
            ren = AddRenderer<MeshRenderer>();
            ren.Mesh = AssetLoader.GetAsset<Mesh>("HellColosseum.fbx/ArenaMiddlePlatform");
            ren.Effect = AssetLoader.GetAsset<BasicEffect>("HellColosseumArenaMiddlePlatform.mat");
        
            ren = AddRenderer<MeshRenderer>();
            ren.Mesh = AssetLoader.GetAsset<Mesh>("HellColosseum.fbx/ArenaDecor");
            ren.Effect = AssetLoader.GetAsset<BasicEffect>("HellColosseumArenaDecor.mat");
            
            ren = AddRenderer<MeshRenderer>();
            ren.Mesh = AssetLoader.GetAsset<Mesh>("HellColosseum.fbx/ArenaExits");
            ren.Effect = AssetLoader.GetAsset<BasicEffect>("HellColosseumArenaExits.mat");
        
            ren = AddRenderer<MeshRenderer>();
            ren.Mesh = AssetLoader.GetAsset<Mesh>("HellColosseum.fbx/CityBuildings.001");
            ren.Effect = AssetLoader.GetAsset<BasicEffect>("HellColosseumCityBuildings.mat");
            
        }
    }
}