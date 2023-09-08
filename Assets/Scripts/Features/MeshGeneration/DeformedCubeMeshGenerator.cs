using UnityEngine;
using static CannonShootingPrototype.Data.Static.Constants.MeshGeneration.MeshConstants;

namespace CannonShootingPrototype.Features.MeshGeneration
{
    public class DeformedCubeMeshGenerator : IMeshGenerator
    {
        public readonly float _maxVertexPositionOffset;
        public DeformedCubeMeshGenerator(float maxVertexPositionOffset) =>
            _maxVertexPositionOffset = maxVertexPositionOffset;

        public Mesh Generate()
        {
            var mesh = new Mesh
            {
                vertices = GenerateVertices(),
                triangles = GenerateTriangles()
            };

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.Optimize();
            
            return mesh;
        }

        public Vector3[] GenerateVertices()
        {
            var bottomVertex1 = new Vector3(-VertexPositionOffset, -VertexPositionOffset, -VertexPositionOffset);
            var bottomVertex2 = new Vector3(-VertexPositionOffset, -VertexPositionOffset, +VertexPositionOffset);
            var bottomVertex3 = new Vector3(+VertexPositionOffset, -VertexPositionOffset, +VertexPositionOffset);
            var bottomVertex4 = new Vector3(+VertexPositionOffset, -VertexPositionOffset, -VertexPositionOffset);

            var topVertex1 = new Vector3(-VertexPositionOffset, +VertexPositionOffset, -VertexPositionOffset);
            var topVertex2 = new Vector3(-VertexPositionOffset, +VertexPositionOffset, +VertexPositionOffset);
            var topVertex3 = new Vector3(+VertexPositionOffset, +VertexPositionOffset, +VertexPositionOffset);
            var topVertex4 = new Vector3(+VertexPositionOffset, +VertexPositionOffset, -VertexPositionOffset);
            
            bottomVertex1 += Random.insideUnitSphere * _maxVertexPositionOffset;
            bottomVertex2 += Random.insideUnitSphere * _maxVertexPositionOffset;
            bottomVertex3 += Random.insideUnitSphere * _maxVertexPositionOffset;
            bottomVertex4 += Random.insideUnitSphere * _maxVertexPositionOffset;
            
            topVertex1 += Random.insideUnitSphere * _maxVertexPositionOffset;
            topVertex2 += Random.insideUnitSphere * _maxVertexPositionOffset;
            topVertex3 += Random.insideUnitSphere * _maxVertexPositionOffset;
            topVertex4 += Random.insideUnitSphere * _maxVertexPositionOffset;

            RandomizeVertices(bottomVertex1, bottomVertex2, bottomVertex3, bottomVertex4,
                topVertex1, topVertex2, topVertex3, topVertex4);

            Vector3[] vertices = new[]
            {
                bottomVertex4, bottomVertex3, bottomVertex2, bottomVertex1, // Bottom
                topVertex1,  topVertex2,  topVertex3,  topVertex4,          // Top
                bottomVertex1, topVertex1, topVertex4, bottomVertex4,       // Back
                bottomVertex3, topVertex3, topVertex2, bottomVertex2,       // Front
                bottomVertex2, topVertex2, topVertex1, bottomVertex1,       // Left
                bottomVertex4, topVertex4, topVertex3, bottomVertex3        // Right
            };

            return vertices;
        }

        public int[] GenerateTriangles()
        {
            var triangles = new int[NumberOfTrianglesOfCube];
            int vertexIndex = 0;
            int[] sideTriangles = GenerateQuadTriangles();
            
            for (int sideIndex = 0; sideIndex < NumberOfSidesOfCube; sideIndex++)
            {
                int sideIndexOffset = sideIndex * NumberOfVerticesInQuad;

                for (int sharedVertexIndex = 0; sharedVertexIndex < NumberOfSharedVerticesInQuad; sharedVertexIndex++)
                    triangles[vertexIndex++] = sideIndexOffset + sideTriangles[sharedVertexIndex];
            }

            return triangles;
        }

        private int[] GenerateQuadTriangles() => new[] { 0, 1, 2, 0, 2, 3 };

        private Vector3[] RandomizeVertices(params Vector3[] vertices)
        {
            for (int vertexIndex = 0; vertexIndex < vertices.Length; vertexIndex++)
                vertices[vertexIndex] += Random.insideUnitSphere * _maxVertexPositionOffset;
            return vertices;
        }
    }
}