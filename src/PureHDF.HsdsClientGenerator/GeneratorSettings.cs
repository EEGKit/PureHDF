﻿namespace PureHDF.HsdsClientGenerator
{
    public record GeneratorSettings(
        string? Namespace,
        string ClientName,
        string ExceptionType);
}