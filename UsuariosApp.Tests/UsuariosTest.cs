using Bogus;
using Bogus.DataSets;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Services.Models;
using Xunit;

namespace UsuariosApp.Tests
{
    public class UsuariosTest
    {
        [Fact]
        public async Task<CriarUsuarioResponseModel> CriarUsuarioComSucesso()
        {
            var faker = new Faker();
            var request = new CriarUsuarioRequestModel
            {
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = "@Teste123",
                SenhaConfirmacao = "@Teste123"
            };

            //serializando os dados em JSON
            var content = new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //enviando a requisição para a API (ENDPOINT)
            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = await client.PostAsync("/api/Usuarios/Criar", content);

            //verificando se a resposta foi sucesso
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            //deserializar os dados obtidos do teste
            var response = JsonConvert.DeserializeObject<CriarUsuarioResponseModel>
                (result.Content.ReadAsStringAsync().Result);

            //verificando os dados obtidos pelo teste
            response?.Id.Should().NotBeNull();
            response?.Nome.Should().Be(request.Nome);
            response?.Email.Should().Be(request.Email);
            response?.DataHoraCadastro.Should().NotBeNull();

            return response;
        }

        [Fact]
        public async Task EmailDeveSerUnicoParaCadaUsuario()
        {
            //criando um usuário na API
            var usuario = await CriarUsuarioComSucesso();

            var request = new CriarUsuarioRequestModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = "@Teste123",
                SenhaConfirmacao = "@Teste123"
            };

            //serializando os dados em JSON
            var content = new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //enviando a requisição para a API (ENDPOINT)
            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = await client.PostAsync("/api/Usuarios/Criar", content);

            //verificando se a resposta foi sucesso
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //verificando a mensagem obtida
            result.Content.ReadAsStringAsync().Result
                .Should().Contain("O email informado já está cadastrado.");
        }

        [Fact]
        public async Task AutenticarUsuarioComSucesso()
        {
            //criando um usuário na API
            var usuario = await CriarUsuarioComSucesso();

            //enviando os dados de autenticação
            var request = new AutenticarUsuarioRequestModel
            {
                Email = usuario.Email,
                Senha = "@Teste123"
            };

            //serializando os dados em JSON
            var content = new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //enviando a requisição para a API (ENDPOINT)
            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = await client.PostAsync("/api/Usuarios/Autenticar", content);

            //verificando se a resposta foi sucesso
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //deserializar os dados obtidos do teste
            var response = JsonConvert.DeserializeObject<AutenticarUsuarioResponseModel>
                (result.Content.ReadAsStringAsync().Result);

            //verificando os dados obtidos pelo teste
            response?.Id.Should().Be(usuario.Id);
            response?.Nome.Should().Be(usuario.Nome);
            response?.Email.Should().Be(usuario.Email);
            response?.DataHoraAcesso.Should().NotBeNull();
            response?.DataHoraExpiracao.Should().NotBeNull();
            response?.AccessToken.Should().NotBeNull();
        }

        [Fact]
        public async Task AcessoNegadoDeUsuario()
        {
            var faker = new Faker();
            var request = new AutenticarUsuarioRequestModel
            {
                Email = faker.Internet.Email(),
                Senha = "@Teste321"
            };

            var content = new StringContent
               (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //enviando a requisição para a API (ENDPOINT)
            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = await client.PostAsync("/api/Usuarios/Autenticar", content);

            //verificando se a resposta foi acesso negado
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            result.Content.ReadAsStringAsync().Result
                .Should().Contain("Usuário não encontrado.");
        }
    }
}