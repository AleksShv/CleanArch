namespace CleanArch.DomainServices.Catalog.Consts;

public static class ProductConsts
{
    public static readonly (int Min, int Max) TitleLength = (5, 25);
    public static readonly (int Min, int Max) DescriptionLength = (15, 125);
    public static readonly (decimal Min, decimal Max) Price = (1.00m, 1_000.00m);
}
