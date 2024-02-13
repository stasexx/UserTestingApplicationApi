using System.Net.Http.Headers;
using System.Text;
using Application.Models.Identity;
using Newtonsoft.Json;
using Tests.TestExtensions;

namespace Tests.Tests;

public class TestsBase : IClassFixture<TestingFactory<Program>>
{
    private protected HttpClient _httpClient;

    public TestsBase(TestingFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        factory.InitializeDatabase();
    }

    public async Task LoginAsync(string name)
    {
        var loginDto = new
        {
            Name = name
        };
        
        var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("users/login", content);
        
        response.EnsureSuccessStatusCode();
        
        var tokensModel = JsonConvert.DeserializeObject<TokensModel>(await response.Content.ReadAsStringAsync());
        
        if (tokensModel != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokensModel.AccessToken);
        }
    }
}