using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoKad
{
    public class FunnyTriangle : GameObject
    {
        private VertexPositionColor[] _vertices;
        
        private VertexBuffer _vertexBuffer; // TODO when assets exist: needs to be associated to a shared mesh or asset
        private BasicEffect _effect;

        public FunnyTriangle(BasicEffect effect) {
            _vertices = new VertexPositionColor[] {
                new VertexPositionColor(new Vector3(0f, 20.0f, 0.0f), Color.Green),
                new VertexPositionColor(new Vector3(-20.0f, -20.0f, 0.0f), Color.Red),
                new VertexPositionColor(new Vector3(20f, -20.0f, 0.0f), Color.Blue),
            };
            
            _vertexBuffer = new VertexBuffer(KadGame.Instance.GraphicsDevice, typeof(VertexPositionColor), _vertices.Length, BufferUsage.WriteOnly);
            _vertexBuffer.SetData(_vertices);
            _effect = effect;
        }

        public override void Draw(GameTime gameTime) {
            KadGame.Instance.GraphicsDevice.SetVertexBuffer(_vertexBuffer);
            
            // Turn off backface culling
            KadGame.Instance.GraphicsDevice.RasterizerState = new RasterizerState() {
                CullMode = CullMode.None
            };

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes) {
                pass.Apply();
                KadGame.Instance.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, _vertices.Length);
            }
        }
    }  
}