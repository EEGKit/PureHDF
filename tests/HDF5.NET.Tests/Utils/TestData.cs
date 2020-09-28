﻿using System.Collections.Generic;
using System.Linq;

namespace HDF5.NET.Tests
{
    public class TestData
    {
        private static TestStructL1 _nn_a = new TestStructL1()
        {
            ByteValue = 1,
            UShortValue = 2,
            UIntValue = 3,
            ULongValue = 4,
            L2Struct1 = new TestStructL2()
            {
                ByteValue = 99,
                UShortValue = 65535,
                EnumValue = TestEnum.b,
            },
            SByteValue = 5,
            ShortValue = -6,
            IntValue = 7,
            LongValue = -8,
            L2Struct2 = new TestStructL2()
            {
                ByteValue = 9,
                EnumValue = TestEnum.b,
            }
        };

        private static TestStructL1 _nn_b = new TestStructL1()
        {
            ByteValue = 2,
            UShortValue = 4,
            UIntValue = 6,
            ULongValue = 8,
            L2Struct1 = new TestStructL2()
            {
                ByteValue = 99,
                UShortValue = 65535,
                EnumValue = TestEnum.a,
            },
            SByteValue = -10,
            ShortValue = 12,
            IntValue = 14,
            LongValue = -16,
            L2Struct2 = new TestStructL2()
            {
                ByteValue = 18,
                UShortValue = 20,
                EnumValue = TestEnum.b,
            }
        };

        private static TestStructString _string_a = new TestStructString()
        {
            FloatValue = (float)1.299e9,
            StringValue1 = "Hello",
            StringValue2 = "World",
            ByteValue = 123,
            ShortValueWithCustomName = -15521,
            L2Struct = new TestStructL2()
            {
                ByteValue = 15,
                UShortValue = 20,
                EnumValue = TestEnum.a,
            }
        };

        private static TestStructString _string_b = new TestStructString()
        {
            FloatValue = (float)2.299e-9,
            StringValue1 = "Hello!",
            StringValue2 = "World!",
            ByteValue = 0,
            ShortValueWithCustomName = 15521,
            L2Struct = new TestStructL2()
            {
                ByteValue = 18,
                UShortValue = 21,
                EnumValue = TestEnum.b,
            }
        };

        static TestData()
        {
            TestData.AttributeNumericalTestData = new List<object[]>
            {
                new object[] { "A1", new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 } },
                new object[] { "A2", new ushort[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 } },
                new object[] { "A3", new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 } },
                new object[] { "A4", new ulong[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 } },
                new object[] { "A5", new sbyte[] { 0, 1, 2, 3, 4, 5, 6, -7, 8, 9, 10, 11 } },
                new object[] { "A6", new short[] { 0, 1, 2, 3, 4, 5, 6, -7, 8, 9, 10, 11 } },
                new object[] { "A7", new int[] { 0, 1, 2, 3, 4, 5, 6, -7, 8, 9, 10, 11 } },
                new object[] { "A8", new long[] { 0, 1, 2, 3, 4, 5, 6, -7, 8, 9, 10, 11 } },
                new object[] { "A9", new float[] { 0, 1, 2, 3, 4, 5, 6, (float)-7.99, 8, 9, 10, 11 } },
                new object[] {"A10", new double[] { 0, 1, 2, 3, 4, 5, 6, -7.99, 8, 9, 10, 11 } },
            };

            TestData.DatasetNumericalTestData = new List<object[]>
            {
                new object[] { "D1", new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 } },
                new object[] { "D2", new ushort[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 } },
                new object[] { "D3", new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 } },
                new object[] { "D4", new ulong[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 } },
                new object[] { "D5", new sbyte[] { 0, 1, 2, 3, 4, 5, 6, -7, 8, 9, 10, 11 } },
                new object[] { "D6", new short[] { 0, 1, 2, 3, 4, 5, 6, -7, 8, 9, 10, 11 } },
                new object[] { "D7", new int[] { 0, 1, 2, 3, 4, 5, 6, -7, 8, 9, 10, 11 } },
                new object[] { "D8", new long[] { 0, 1, 2, 3, 4, 5, 6, -7, 8, 9, 10, 11 } },
                new object[] { "D9", new float[] { 0, 1, 2, 3, 4, 5, 6, (float)-7.99, 8, 9, 10, 11 } },
                new object[] {"D10", new double[] { 0, 1, 2, 3, 4, 5, 6, -7.99, 8, 9, 10, 11 } },
            };

            TestData.NonNullableTestStructData = new TestStructL1[] { _nn_a, _nn_b, _nn_a, _nn_a, _nn_b, _nn_b, _nn_b, _nn_b, _nn_a, _nn_a, _nn_b, _nn_a };
            TestData.StringTestStructData = new TestStructString[] { _string_a, _string_b, _string_a, _string_a, _string_b, _string_b, _string_b, _string_b, _string_a, _string_a, _string_b, _string_a };
            TestData.TinyData = new byte[] { 99 };
            TestData.SmallData = Enumerable.Range(0, 100).ToArray();
            TestData.MediumData = Enumerable.Range(0, 10_000).ToArray();
            TestData.HugeData = Enumerable.Range(0, 10_000_000).ToArray();
        }

        public static IList<object[]> AttributeNumericalTestData { get; }

        public static IList<object[]> DatasetNumericalTestData { get; }

        public static TestStructL1[] NonNullableTestStructData { get; }

        public static TestStructString[] StringTestStructData { get; }

        public static byte[] TinyData { get; }
        public static int[] SmallData { get; }
        public static int[] MediumData { get; }
        public static int[] HugeData { get; }
    }
}