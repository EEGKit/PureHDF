﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace PureHDF.VFD;

internal partial class H5FileHandleDriver : H5DriverBase
{
    private readonly ThreadLocal<long> _position = new();
    private readonly FileStream _stream; // it is important to keep a reference, otherwise the SafeFileHandle gets closed during the next GC
    private readonly SafeFileHandle _handle;
    private readonly bool _leaveOpen;

    public H5FileHandleDriver(FileStream stream, bool leaveOpen)
    {
        _stream = stream;
        _handle = _stream.SafeFileHandle;
        _leaveOpen = leaveOpen;
    }

    public override long Position { get => _position.Value; }

    public override long Length => _stream.Length;

    public override void Seek(long offset, SeekOrigin seekOrigin)
    {
        switch (seekOrigin)
        {
            case SeekOrigin.Begin:
                _position.Value = (long)BaseAddress + offset; break;

            case SeekOrigin.Current:
                _position.Value += offset; break;

            default:
                throw new Exception($"Seek origin '{seekOrigin}' is not supported.");
        }
    }

    public override void ReadDataset(Span<byte> buffer)
    {
        ReadCore(buffer);
    }

    public override void Read(Span<byte> buffer)
    {
        throw new NotImplementedException();
    }

    public override byte ReadByte()
    {
        return Read<byte>();
    }

    public override byte[] ReadBytes(int count)
    {
        var buffer = new byte[count];
        ReadCore(buffer);

        return buffer;
    }

    public override ushort ReadUInt16()
    {
        return Read<ushort>();
    }

    public override short ReadInt16()
    {
        return Read<short>();
    }

    public override uint ReadUInt32()
    {
        return Read<uint>();
    }

    public override ulong ReadUInt64()
    {
        return Read<ulong>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ReadCore(Span<byte> buffer)
    {
        var count = RandomAccess.Read(_handle, buffer, Position);

        if (count != buffer.Length)
            throw new Exception("The file is too small");

        _position.Value += count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T Read<T>() where T : unmanaged
    {
        var size = Unsafe.SizeOf<T>();
        Span<byte> buffer = stackalloc byte[size];
        RandomAccess.Read(_handle, buffer, Position);
        _position.Value += size;

        return MemoryMarshal.Cast<byte, T>(buffer)[0];
    }

    private bool _disposedValue;

    protected override void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                if (!_leaveOpen)
                    _stream.Dispose();
            }

            _disposedValue = true;
        }

        base.Dispose(disposing);
    }
}