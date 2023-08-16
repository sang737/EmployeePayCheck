using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ApiTests;

public class IntegrationTest : IDisposable
{
    private HttpClient? _httpClient;

    protected HttpClient HttpClient
    {
        get
        {
            if (_httpClient == default)
            {
                //_httpClient = new HttpClient
                //This step is to instantiate the API project by configuring all the required services. 
                _httpClient = new WebApplicationFactory<Program>().CreateClient();
                {
                    //task: update your port if necessary
                    //BaseAddress = new Uri("https://localhost:7124");
                };
                _httpClient.DefaultRequestHeaders.Add("accept", "text/plain");
            }

            return _httpClient;
        }
    }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}

