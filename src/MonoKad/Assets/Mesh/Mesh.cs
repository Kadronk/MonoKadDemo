using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoKad
{
    public class Mesh
    {
        private const int VECTOR3_SIZE = 12; //Vector3 = 3 floats * 4 bytes per float
        private const int VECTOR2_SIZE = 8; //Vector2 = 2 floats * 4 bytes per float
        private const int COLOR_SIZE = 4; //Color = 4 color components * 1 byte per component
        private const int TRIANGLE_SIZE = 12; //Triangle = 3 integer indices * 4 bytes per int

        public int VertexCount => _vertices.Length;
        public Vector3[] Vertices => _vertices;
        public int TriangleCount => _triangles.Length / 3;
        public int[] VertexIndices => _triangles;
        public VertexBuffer VertexBuffer => _vertexBuffer;
        public IndexBuffer IndexBuffer => _indexBuffer;

        private Vector3[] _vertices;
        private int[] _triangles;
        private Vector3[] _normals;
        private Vector3[] _tangents;
        private Vector2[][] _uvChannels;
        private Color[] _colors;
        private BoundingBox _bounds;
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;

        internal Mesh(Assimp.Mesh assimpMesh) {
            if (assimpMesh.HasVertices == false) {
                Console.WriteLine($"Mesh {assimpMesh.Name} has no vertices!");
                return;
            }

            Vector3 bbMin = new Vector3(assimpMesh.BoundingBox.Min.X, assimpMesh.BoundingBox.Min.Y, assimpMesh.BoundingBox.Min.Z);
            Vector3 bbMax = new Vector3(assimpMesh.BoundingBox.Max.X, assimpMesh.BoundingBox.Max.Y, assimpMesh.BoundingBox.Max.Z);
            _bounds = new BoundingBox(bbMin, bbMax);
            
            int vertexElementCount = 1;
            if (assimpMesh.HasNormals)
                vertexElementCount++;
            if (assimpMesh.HasTangentBasis)
                vertexElementCount++;
            for (int i = 0; i < assimpMesh.TextureCoordinateChannelCount; i++) {
                vertexElementCount++;
            }
            if (assimpMesh.HasVertexColors(0))
                vertexElementCount++;
            
            VertexElement[] vertexElements = new VertexElement[vertexElementCount];
            
            int currentOffset = 0;
            int currentElementIndex = 0;
            
            _vertices = new Vector3[assimpMesh.VertexCount];
            vertexElements[currentElementIndex++] = new VertexElement(currentOffset, VertexElementFormat.Vector3, VertexElementUsage.Position, 0);
            currentOffset += VECTOR3_SIZE; 
            for (int i = 0; i < assimpMesh.VertexCount; i++) {
                Assimp.Vector3D v = assimpMesh.Vertices[i];
                _vertices[i] = new Vector3(v.X, v.Y, v.Z);
            }

            if (assimpMesh.HasFaces) {
                _triangles = new int[assimpMesh.FaceCount * 3];
                for (int i = 0; i < assimpMesh.FaceCount; i++) {
                    _triangles[i * 3] = assimpMesh.Faces[i].Indices[0];
                    _triangles[i * 3 + 1] = assimpMesh.Faces[i].Indices[1];
                    _triangles[i * 3 + 2] = assimpMesh.Faces[i].Indices[2];
                }
            }

            if (assimpMesh.HasNormals) {
                _normals = new Vector3[assimpMesh.VertexCount];
                vertexElements[currentElementIndex++] = new VertexElement(currentOffset, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0);
                currentOffset += VECTOR3_SIZE;
                for (int i = 0; i < assimpMesh.VertexCount; i++) {
                    Assimp.Vector3D n = assimpMesh.Normals[i];
                    _normals[i] = new Vector3(n.X, n.Y, n.Z);
                }
            }

            if (assimpMesh.HasTangentBasis) {
                _tangents = new Vector3[assimpMesh.VertexCount];
                vertexElements[currentElementIndex++] = new VertexElement(currentOffset, VertexElementFormat.Vector3, VertexElementUsage.Tangent, 0);
                currentOffset += VECTOR3_SIZE; 
                for (int i = 0; i < assimpMesh.VertexCount; i++) {
                    Assimp.Vector3D t = assimpMesh.Tangents[i];
                    _tangents[i] = new Vector3(t.X, t.Y, t.Z);
                }
            }

            if (assimpMesh.TextureCoordinateChannelCount > 0) {
                _uvChannels = new Vector2[assimpMesh.TextureCoordinateChannelCount][];
                for (int i = 0; i < assimpMesh.TextureCoordinateChannelCount; i++) { // i = channel index
                    _uvChannels[i] = new Vector2[assimpMesh.VertexCount];
                    vertexElements[currentElementIndex++] = new VertexElement(currentOffset, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, i);
                    currentOffset += VECTOR2_SIZE;
                    for (int j = 0; j < assimpMesh.VertexCount; j++) { // j = vertex index in channel
                        Assimp.Vector3D uv = assimpMesh.TextureCoordinateChannels[i][j];
                        _uvChannels[i][j] = new Vector2(uv.X, uv.Y);
                    }
                }
            }

            if (assimpMesh.HasVertexColors(0)) {
                _colors = new Color[assimpMesh.VertexCount];
                vertexElements[currentElementIndex++] = new VertexElement(currentOffset, VertexElementFormat.Color, VertexElementUsage.Color, 0);
                currentOffset += COLOR_SIZE;
                for (int i = 0; i < assimpMesh.VertexCount; i++) {
                    Assimp.Color4D c = assimpMesh.VertexColorChannels[0][i]; // Only care about the first vertex color channel
                    _colors[i] = new Color(c.R, c.G, c.B, c.A);
                }
            }

            if (_triangles != null) {
                _indexBuffer = new IndexBuffer(KadGame.Instance.GraphicsDevice, IndexElementSize.ThirtyTwoBits, assimpMesh.FaceCount * 3, BufferUsage.WriteOnly); //REMETTRE A WRITEONLY
                _indexBuffer.SetData(0, _triangles, 0, _triangles.Length);
            }
            _vertexBuffer = new VertexBuffer(KadGame.Instance.GraphicsDevice, new VertexDeclaration(vertexElements), assimpMesh.VertexCount, BufferUsage.WriteOnly);
            FeedVertexBuffer(currentOffset);
        }

        void FeedVertexBuffer(int vertexStride) {
            int currentOffset = 0;
            
            _vertexBuffer.SetData(currentOffset, _vertices, 0, _vertices.Length, vertexStride); // Vertex positions
            currentOffset += VECTOR3_SIZE;
            if (_normals != null) {
                _vertexBuffer.SetData(currentOffset, _normals, 0, _normals.Length, vertexStride);
                currentOffset += VECTOR3_SIZE;
            }
            if (_tangents != null) {
                _vertexBuffer.SetData(currentOffset, _tangents, 0, _tangents.Length, vertexStride);
                currentOffset += VECTOR3_SIZE;
            }
            foreach (Vector2[] uv in _uvChannels) {
                _vertexBuffer.SetData(currentOffset, uv, 0, uv.Length, vertexStride);
                currentOffset += VECTOR2_SIZE;
            }
            if (_colors != null) {
                _vertexBuffer.SetData(currentOffset, _colors, 0, _colors.Length, vertexStride);
                currentOffset += COLOR_SIZE;
            }
        }
    }   
}