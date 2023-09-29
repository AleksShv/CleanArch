using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using CleanArch.Controllers.Catalog.Products.Requests;
using CleanArch.Controllers.Catalog.Products.Responses;
using CleanArch.Controllers.Common;
using CleanArch.IntegrationTests.Base;
using CleanArch.UseCases.Auth.Login;

namespace CleanArch.IntegrationTests.Catalog;

public class ProductTests : IntegrationTestBase
{
    private readonly HttpClient _client;

    public ProductTests(IntegrationTestWebApplicationFactory factory) 
        : base(factory)
    {
        _client = factory.CreateClient(new()
        {
            AllowAutoRedirect = true,
        });
    }

    public override async Task InitializeAsync()
    {
        var response = await _client.PostAsync("api/auth/login", JsonContent.Create<LoginCommand>(new("productowner@mail.test.com", "123456")));
        var token = (await response.Content.ReadFromJsonAsync<LoginResultDto>())?.AccessToken;

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    [Fact]
    public async Task AddProduct_ValidProduct_CreatesProduct()
    {
        // Arrange
        var request = new AddProductRequest(
            Title: "Product #2",
            Description: "Product #2 description",
            Price: 12.55m);

        // Act
        var response = await _client
            .PostAsync("api/products", JsonContent.Create(request));

        var responseContent = await response.Content
            .ReadFromJsonAsync<Guid>();

        // Assert
        var productResponse = await _client
            .GetAsync($"api/products/{responseContent}");

        var productResponseContent = await productResponse.Content
            .ReadFromJsonAsync<ProductDetailsResponse>();

        Assert.Equal(HttpStatusCode.OK, productResponse.StatusCode);
        Assert.NotNull(productResponseContent);
        Assert.Equal(request.Title, productResponseContent.Title);
        Assert.Equal(request.Description, productResponseContent.Description);
        Assert.Equal(request.Price, productResponseContent.Price);
    }

    [Fact]
    public async Task UpdateProduct_ValidProduct_CacheRewrite()
    {
        // Arrange
        var pageResponse = await _client
            .GetAsync($"api/products?pageIndex=0&pageSize=1");

        var page = await pageResponse.Content
            .ReadFromJsonAsync<PaggingResponse<ProductPaggingItemResponse>>()
            ?? throw new InvalidOperationException("Response content not found");

        var productId = page.Items.Single().Id;

        var productResponse = await _client
            .GetAsync($"api/products/{productId}");

        var product = await productResponse.Content
            .ReadFromJsonAsync<ProductDetailsResponse>();

        var request = new UpdateProductRequest(
            productId,
            "Product #3",
            "Product #3 description",
            12.55m);

        // Act
        await _client.PutAsync($"api/products/{productId}", JsonContent.Create(request));

        // Act
        productResponse = await _client
            .GetAsync($"api/products/{productId}");

        var updatedProduct = await productResponse.Content
            .ReadFromJsonAsync<ProductDetailsResponse>();

        // Assert
        Assert.NotNull(product);
        Assert.NotNull(updatedProduct);
        Assert.Equal(product.Id, updatedProduct.Id);
        Assert.NotEqual(product.Title, updatedProduct.Title);
        Assert.NotEqual(product.Description, updatedProduct.Description);
    }
}
