using AutoMapper;

namespace CleanArch.Utils.AutoMapper;

public static class MappingValueTransformers
{
    public static readonly ValueTransformerConfiguration Trimmer = new(typeof(string), (string value) => string.IsNullOrWhiteSpace(value) ? value : value.Trim());
}
