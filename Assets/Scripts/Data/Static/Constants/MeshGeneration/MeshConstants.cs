namespace CannonShootingPrototype.Data.Static.Constants.MeshGeneration
{
    public static class MeshConstants
    {
        public const float VertexPositionOffset = 0.5f;
        
        public const int NumberOfVerticesInTriangle = 3;
        
        public const int NumberOfTrianglesInQuad = 2;
        public const int NumberOfVerticesInQuad = 4;
        public const int NumberOfSharedVerticesInQuad = NumberOfVerticesInTriangle * NumberOfTrianglesInQuad;
        
        public const int NumberOfSidesOfCube = 6;
        public const int NumberOfTrianglesOfCube =
            NumberOfTrianglesInQuad * NumberOfVerticesInTriangle * NumberOfSidesOfCube;
    }
}