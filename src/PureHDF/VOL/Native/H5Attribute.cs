﻿using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PureHDF;

[DebuggerDisplay("{Name}: Class = '{Message.Datatype.Class}'")]
internal class H5Attribute : IH5Attribute
{
    #region Fields

    private IH5Dataspace? _space;
    private IH5DataType? _type;
    private H5Context _context;

    #endregion

    #region Constructors

    internal H5Attribute(H5Context context, AttributeMessage message)
    {
        _context = context;
        Message = message;

        InternalElementDataType = Message.Datatype.Properties.FirstOrDefault() switch
        {
            ArrayPropertyDescription array => array.BaseType,
            _ => Message.Datatype
        };
    }

    #endregion

    #region Properties

    public string Name => Message.Name;

    public IH5Dataspace Space
    {
        get
        {
            _space ??= new H5Dataspace(Message.Dataspace);

            return _space;
        }
    }

    public IH5DataType Type
    {
        get
        {
            _type ??= new H5DataType(Message.Datatype);

            return _type;
        }
    }

    #endregion

    #region Methods

    public T[] Read<T>()
        where T : unmanaged
    {
        switch (Message.Datatype.Class)
        {
            case DatatypeMessageClass.FixedPoint:
            case DatatypeMessageClass.FloatingPoint:
            case DatatypeMessageClass.BitField:
            case DatatypeMessageClass.Opaque:
            case DatatypeMessageClass.Compound:
            case DatatypeMessageClass.Reference:
            case DatatypeMessageClass.Enumerated:
            case DatatypeMessageClass.Array:
                break;

            default:
                throw new Exception($"This method can only be used with one of the following type classes: '{DatatypeMessageClass.FixedPoint}', '{DatatypeMessageClass.FloatingPoint}', '{DatatypeMessageClass.BitField}', '{DatatypeMessageClass.Opaque}', '{DatatypeMessageClass.Compound}', '{DatatypeMessageClass.Reference}', '{DatatypeMessageClass.Enumerated}' and '{DatatypeMessageClass.Array}'.");
        }

        var buffer = Message.Data;
        var byteOrderAware = Message.Datatype.BitField as IByteOrderAware;
        var destination = buffer;
        var source = destination.ToArray();

        if (byteOrderAware is not null)
            Utils.EnsureEndianness(source, destination, byteOrderAware.ByteOrder, Message.Datatype.Size);

        return MemoryMarshal
            .Cast<byte, T>(Message.Data)
            .ToArray();
    }

    public T[] ReadCompound<T>(Func<FieldInfo, string>? getName = default)
        where T : struct
    {
        getName ??= fieldInfo => fieldInfo.Name;

        var elementCount = Message.Data.Length / InternalElementDataType.Size;
        var result = new T[elementCount];

        ReadUtils.ReadCompound<T>(_context, InternalElementDataType, Message.Data, result, getName);

        return result;
    }

    public Dictionary<string, object?>[] ReadCompound()
    {
        var elementCount = Message.Data.Length / InternalElementDataType.Size;
        var result = new Dictionary<string, object?>[elementCount];

        ReadUtils.ReadCompound(_context, InternalElementDataType, Message.Data, result);

        return result;
    }

    public string[] ReadString()
    {
        return ReadUtils.ReadString(_context, InternalElementDataType, Message.Data);
    }


    internal AttributeMessage Message { get; }

    internal DatatypeMessage InternalElementDataType { get; }

    #endregion
}