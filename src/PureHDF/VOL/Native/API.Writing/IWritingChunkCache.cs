﻿namespace PureHDF.VOL.Native;

/// <summary>
/// Caches chunks during write operations.
/// </summary>
public interface IWritingChunkCache
{
    /// <summary>
    /// Tries to get the chunk at the given index.
    /// </summary>
    /// <param name="chunkIndex">The linear chunk index.</param>
    /// <param name="chunkAllocator">The chunk allocator is used whenever the chunk is not already cached.</param>
    /// <param name="chunkWriter">The chunk writer is used whenever the chunk is flushed from the cache.</param>
    public Memory<byte> GetChunk(
        ulong chunkIndex,
        Func<Memory<byte>> chunkAllocator,
        Action<ulong, Memory<byte>> chunkWriter);

    /// <summary>
    /// Flushes the chunk.
    /// </summary>
    /// <param name="chunkWriter">The chunk writer used for chunks being flushed from the cache.</param>
    public void Flush(Action<ulong, Memory<byte>> chunkWriter);
}