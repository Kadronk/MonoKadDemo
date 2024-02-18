using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoKad.Components
{
    [Obsolete]
    public class TriangleRenderer : Renderer
    {
        private VertexPositionColor[] _vertices;
        
        private VertexBuffer _vertexBuffer; // TODO when assets exist: needs to be associated to a shared mesh or asset

        public override void Awake() {
            _vertices = new VertexPositionColor[] {
                new VertexPositionColor(new Vector3(0f, 20.0f, 0.0f), Color.Green),
                new VertexPositionColor(new Vector3(-20.0f, -20.0f, 0.0f), Color.Red),
                new VertexPositionColor(new Vector3(20f, -20.0f, 0.0f), Color.Blue),
            };
            
            _vertexBuffer = new VertexBuffer(KadGame.Instance.GraphicsDevice, typeof(VertexPositionColor), _vertices.Length, BufferUsage.WriteOnly);
            _vertexBuffer.SetData(_vertices);
        }

        public override void Draw() {
            BasicEffect effect = AssetLoader.GetAsset<BasicEffect>("BasicEffect");
            effect.Projection = KadGame.Instance.CurrentCamera.ProjectionMatrix;
            effect.View = KadGame.Instance.CurrentCamera.ViewMatrix;
            effect.World = GameObject.TransformMatrix;
            KadGame.Instance.GraphicsDevice.SetVertexBuffer(_vertexBuffer);
            
            // Turn off backface culling
            KadGame.Instance.GraphicsDevice.RasterizerState = new RasterizerState() {
                CullMode = CullMode.None
            };

            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                KadGame.Instance.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, _vertices.Length);
            }
        }
    }
}
