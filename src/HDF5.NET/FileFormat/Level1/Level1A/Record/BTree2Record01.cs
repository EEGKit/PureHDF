﻿using System.IO;

namespace HDF5.NET
{
    public class BTree2Record01 : BTree2Record
    {
        #region Constructors

        public BTree2Record01(BinaryReader reader, Superblock superblock) : base(reader)
        {
            this.HugeObjectAddress = superblock.ReadOffset();
            this.HugeObjectLength = superblock.ReadLength();
            this.HugeObjectId = superblock.ReadLength();
        }

        #endregion

        #region Properties

        public ulong HugeObjectAddress { get; set; }
        public ulong HugeObjectLength { get; set; }
        public ulong HugeObjectId { get; set; }

        #endregion
    }
}