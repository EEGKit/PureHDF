﻿using Xunit;

namespace PureHDF.Tests.Writing;

public partial class DatasetTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CanWrite_Space_Null(bool preferCompact)
    {
        // Arrange
        var file = new H5File();

        file["Null"] = new H5Dataset<int[]>();

        var filePath = Path.GetTempFileName();
        var options = new H5WriteOptions(PreferCompactDatasetLayout: preferCompact);

        // Act
        file.Write(filePath, options);

        // Assert
        try
        {
            var actual = TestUtils.DumpH5File(filePath);

            var expected = File
                .ReadAllText($"DumpFiles/space_null.dump")
                .Replace("<file-path>", filePath)
                .Replace("<type>", "DATASET");

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
        var fileDims = new ulong[] { 3, 3 };

        file["2D"] = new H5Dataset(data, fileDims: fileDims);

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
                .Replace("<type>", "DATASET");

            Assert.Equal(expected, actual);
        }
        finally
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}