using Microsoft.Xna.Framework.Graphics;

namespace MonoKad.Components
{
    public class MeshRenderer : Renderer
    {
        public Mesh Mesh { get => _mesh; set => _mesh = value; }
        public BasicEffect Effect { get => _effect; set => _effect = value; }

        private Mesh _mesh;
        private BasicEffect _effect; // will be of type "Effect" one day

        public override void Draw() {
            if (_mesh == null) {
                Console.WriteLine("This MeshRenderer has no mesh!");
                return;
            }
            
            _effect.Projection = KadGame.Instance.CurrentCamera.ProjectionMatrix;
            _effect.View = KadGame.Instance.CurrentCamera.ViewMatrix;
            _effect.World = GameObject.GetTransformMatrix();
            KadGame.Instance.GraphicsDevice.SetVertexBuffer(_mesh.VertexBuffer);
            KadGame.Instance.GraphicsDevice.Indices = _mesh.IndexBuffer;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes) {
                pass.Apply();
                KadGame.Instance.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, _mesh.VertexCount);
            }
        }
    }
}