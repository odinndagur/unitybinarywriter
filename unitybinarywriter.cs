static double[] GetDoublesAlt(byte[] bytes)
{
    var result = new double[bytes.Length / sizeof(double)];
    Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
    return result;
}

private void LoadTilemap(string aFileName)
 {
     try
     {
         using (var fileStream = System.IO.File.OpenRead(aFileName))
         using (var reader = new System.IO.BinaryReader(fileStream))
         {
             var magic = reader.ReadString();
             // check your file magic to identify your file, so you can be sure
             // you access the right file
             if (magic != "YourFileMagic")
                 throw new System.Exception("Wrong file format");
             // check your file version in order to be future proof
             var version = reader.ReadInt32();
             if (version != 1)
                 throw new System.Exception("Not supported file version");
             // read our own 
             regionMapSize.x = reader.ReadInt32();
             regionMapSize.y = reader.ReadInt32();
             regionsize = reader.ReadInt32();
             tilemapRegionValues = new float[regionMapSize.x, regionMapSize.y, regionsize, regionsize];
             for (int regionX = 0; regionX < regionMapSize.x; regionX++)
             {
                 for (int regionY = 0; regionY < regionMapSize.y; regionY++)
                 {
                     for (int tilemapX = 0; tilemapX < regionsize; tilemapX++)
                     {
                         for (int tilemapY = 0; tilemapY < regionsize; tilemapY++)
                         {
                             float value = reader.ReadSingle();
                             tilemapRegionValues[regionX, regionY, tilemapX, tilemapY] = value;
                         }
                     }
                 }
             }
         }
     }
     catch(System.Exception e)
     {
         // handle errors here.
     }
 }