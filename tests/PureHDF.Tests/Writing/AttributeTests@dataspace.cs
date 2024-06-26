﻿using Xunit;

namespace PureHDF.Tests.Writing;

public partial class AttributeTests
{
    [Fact]
    public void CanWrite_Space_Null()
    {
        // Arrange
        var file = new H5File();

        file.Attributes["Null"] = new H5Attribute<int[]>();

        var filePath = Path.GetTempFileName();

        // Act
        file.Write(filePath);

        // Assert
        try
        {
            var actual = TestUtils.DumpH5File(filePath);

            var expected = File
                .ReadAllText($"DumpFiles/space_null.dump")
                .Replace("<file-path>", filePath)
                .Replace("<type>", "ATTRIBUTE");

            Assert.Equal(expected, actual);
        }
        finally
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }

    [Fact]
    public void CanWrite_Space_2D()
    {
        // Arrange
        var file = new H5File();
        var data = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var dimensions = new ulong[] { 3, 3 };

        file.Attributes["2D"] = new H5Attribute(data, dimensions);

        var filePath = Path.GetTempFileName();

        // Act
        file.Write(filePath);

        // Assert
        try
        {
            var actual = TestUtils.DumpH5File(filePath);

            var expected = File
                .ReadAllText($"DumpFiles/space_2D.dump")
                .Replace("<file-path>", filePath)
                .Replace("<type>", "ATTRIBUTE");

            Assert.Equal(expected, actual);
        }
        finally
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}