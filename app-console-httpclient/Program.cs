using System.Net.Http.Json;
using app_console_httpclient.DTOs;

try
{
    var httpClient = new HttpClient();
    const string urlMinimalApi = "http://localhost:5264";

    Console.WriteLine("Incluir cliente");
    Console.WriteLine("---------------");

    var response = await httpClient.PostAsJsonAsync(urlMinimalApi + "/v1/clientes", new Cliente { Nome = "Peter Parker", Email = "peter.parker@marvel.com" });
    response.EnsureSuccessStatusCode();

    var peterParker = await response.Content.ReadFromJsonAsync<Cliente>();

    response = await httpClient.PostAsJsonAsync(urlMinimalApi + "/v1/clientes", new Cliente { Nome = "Ben Parker", Email = "ben.parker@marvel.com" });
    response.EnsureSuccessStatusCode();

    var benParker = await response.Content.ReadFromJsonAsync<Cliente>();

    response = await httpClient.PostAsJsonAsync(urlMinimalApi + "/v1/clientes", new Cliente { Nome = "Mary Jane", Email = "mary.jane@marvel.com" });
    response.EnsureSuccessStatusCode();

    var maryJane = await response.Content.ReadFromJsonAsync<Cliente>();

    if (peterParker != null) Console.WriteLine($"Cliente incluído - {peterParker.Id} - {peterParker.Nome}");
    if (benParker != null) Console.WriteLine($"Cliente incluído - {benParker.Id} - {benParker.Nome}");
    if (maryJane != null) Console.WriteLine($"Cliente incluído - {maryJane.Id} - {maryJane.Nome}");
    Console.WriteLine("");

    if (maryJane != null)
    {
        Console.WriteLine("Atualizar cliente");
        Console.WriteLine("-----------------");

        maryJane.Nome = "Mary Jane Watson";
        response = await httpClient.PutAsJsonAsync(urlMinimalApi + "/v1/clientes/" + maryJane.Id, maryJane);
        response.EnsureSuccessStatusCode();

        var maryJaneUpdate = await response.Content.ReadFromJsonAsync<Cliente>();
        
        if (maryJaneUpdate != null) Console.WriteLine($"Cliente atualizado - {maryJaneUpdate.Id} - {maryJaneUpdate.Nome}");
        Console.WriteLine("");
    }

    if (benParker != null)
    {
        Console.WriteLine("Excluir cliente");
        Console.WriteLine("---------------");

        response = await httpClient.DeleteAsync(urlMinimalApi + "/v1/clientes/" + benParker.Id);
        response.EnsureSuccessStatusCode();

        var benParkerDelete = await response.Content.ReadFromJsonAsync<Cliente>();

        if (benParkerDelete != null) Console.WriteLine($"Cliente excluído - {benParkerDelete.Id} - {benParkerDelete.Nome}");
        Console.WriteLine("");        
    }

    if (peterParker != null)
    {
        Console.WriteLine("Obter cliente");
        Console.WriteLine("-------------");

        response = await httpClient.GetAsync(urlMinimalApi + "/v1/clientes/" + peterParker.Id);
        response.EnsureSuccessStatusCode();

        var returnClienteQuery = await response.Content.ReadFromJsonAsync<Cliente>();

        if (returnClienteQuery != null) Console.WriteLine($"Cliente obtido - {returnClienteQuery.Id} - {returnClienteQuery.Nome}");
        Console.WriteLine("");
    }

    Console.WriteLine("Obter todos os clientes");
    Console.WriteLine("-----------------------");

    response = await httpClient.GetAsync(urlMinimalApi + "/v1/clientes");
    response.EnsureSuccessStatusCode();

    var returnAllClientesQuery = await response.Content.ReadFromJsonAsync<List<Cliente>>();

    if (returnAllClientesQuery != null)
    {
        foreach (var currentCliente in returnAllClientesQuery)
        {
            Console.WriteLine($"{currentCliente.Id} - {currentCliente.Nome}");
        }    
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
}
