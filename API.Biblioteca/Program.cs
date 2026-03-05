using API.Biblioteca.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

// Variavel de ambiente para desenvolvimento
var builder = WebApplication.CreateBuilder(args);

// Adiciona o serviço de Controllers ao contêiner de serviços. Isso permite que o aplicativo use controladores para lidar com as solicitações HTTP e retornar respostas apropriadas.
builder.Services.AddControllers();

// Adiciona o serviço de OpenAPI (Scalar) ao contêiner de serviços. Isso permite que o aplicativo gere automaticamente uma documentação interativa da API, facilitando a compreensão e o teste dos endpoints disponíveis.
builder.Services.AddOpenApi();


// Adiciona o serviço de Entity Framework Core ao contêiner de serviços, configurando-o para usar o SQL Server como provedor de banco de dados. A string de conexão é obtida do arquivo de configuração (appsettings.json) usando a chave "DefaultConnection". Isso permite que o aplicativo se conecte a um banco de dados SQL Server para armazenar e recuperar dados.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o serviço de Identity ao contêiner de serviços, configurando-o para usar o Entity Framework Core como provedor de armazenamento. Isso permite que o aplicativo gerencie a autenticação e autorização dos usuários, armazenando as informações de identidade no banco de dados configurado anteriormente.
builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Adiciona o serviço de CORS (Cross-Origin Resource Sharing) ao contêiner de serviços, configurando-o para permitir solicitações de qualquer origem, com qualquer método HTTP e qualquer cabeçalho. Isso é útil para permitir que clientes de diferentes domínios acessem a API sem restrições.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Constrói o aplicativo usando as configurações e serviços definidos anteriormente. Isso cria uma instância do aplicativo que pode ser configurada e executada para atender às solicitações HTTP.
var app = builder.Build();



// Pipeline de middleware para o ambiente de desenvolvimento. Se o aplicativo estiver sendo executado em um ambiente de desenvolvimento, ele configurará os seguintes middlewares:
if (app.Environment.IsDevelopment())
{
    // Endpoint para acessar a documentação da API gerada pelo OpenAPI (Scalar). Isso permite que os desenvolvedores visualizem e interajam com a documentação da API durante o desenvolvimento.
    app.MapOpenApi();

    // Scalar API Reference para fornecer uma documentação interativa e fácil de usar para a API. As opções de configuração permitem personalizar o título, o tema e a exibição da barra lateral da documentação.
    app.MapScalarApiReference(options =>
    {
        options.Title = "API.Biblioteca - versão 1.0";
        options.Theme = ScalarTheme.Default;
        options.ShowSidebar = true;
    });

    // Tornar a pagina inicial do aplicativo redirecionar para a documentação da API. Isso facilita o acesso à documentação para os desenvolvedores que estão testando a API durante o desenvolvimento.
    app.MapGet("/", () => Results.Redirect("/scalar"));

}

// Configura o middleware de CORS para permitir solicitações de qualquer origem, com qualquer método HTTP e qualquer cabeçalho. Isso é útil para permitir que clientes de diferentes domínios acessem a API sem restrições.
app.UseCors();

// Configura o middleware de autenticação para proteger os endpoints da API. Isso garante que apenas usuários autenticados possam acessar os recursos protegidos da API.
app.UseAuthentication();

// Configura o middleware de autorização para garantir que os usuários autenticados tenham as permissões necessárias para acessar os recursos protegidos da API. Isso é importante para controlar o acesso aos recursos com base nas funções ou permissões dos usuários.
app.UseAuthorization();

// Configura o middleware de redirecionamento para HTTPS, garantindo que todas as solicitações sejam feitas por meio de uma conexão segura. Isso é importante para proteger os dados transmitidos entre o cliente e o servidor.
app.UseHttpsRedirection();

// Configura o middleware de roteamento para mapear as solicitações HTTP para os controladores correspondentes. Isso permite que o aplicativo responda às solicitações com base nas rotas definidas nos controladores.
app.MapControllers();

// Inicia o aplicativo e começa a ouvir as solicitações HTTP. Isso é o ponto de entrada para o aplicativo, permitindo que ele atenda às solicitações dos clientes.
app.Run();