
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.X86;
using static System.Net.WebRequestMethods;

// Este é o namespace em que a classe TodoAppController está definida. 
// Um namespace é uma maneira de organizar e limitar o escopo dos nomes em um programa .NET
namespace WebApplication1.Controllers
{
    // Este atributo define a rota base para todas as ações dentro deste controlador.
    // Ele usa um espaço reservado [controller], que será substituído pelo nome do controlador.
    // Isso significa que as ações neste controlador serão acessíveis em uma rota que começa com "api/NomeDoControlador".
    [Route("api/[controller]")]

    // Este atributo informa ao ASP.NET Core que a classe do controlador é uma API controller.
    // Isso ativa o comportamento padrão do Web API, como a inferência automática do formato de resposta com base nos cabeçalhos Accept do cliente.
    [ApiController]

    // Esta é a declaração da classe TodoAppController, que herda da classe ControllerBase.
    // ControllerBase é uma classe base para controladores sem suporte a visualizações em aplicativos ASP.NET Core.
    // Isso significa que esse controlador não retorna diretamente visualizações HTML, apenas respostas HTTP
    public class TodoAppController : ControllerBase
    {
        // Isso declara uma variável privada _configuration do tipo IConfiguration.
        // IConfiguration é uma interface fornecida pelo ASP.NET Core para acessar configurações do aplicativo, como aquelas definidas no arquivo appsettings.json.
        private IConfiguration _configuration;


        // Este é o construtor da classe TodoAppController, que recebe uma instância de IConfiguration como parâmetro.
        // Ele é usado para injetar a configuração no controlador, permitindo que o controlador acesse as configurações do aplicativo.
        public TodoAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // ROTA PARA LISTAR TODAS AS NOTAS
        [HttpGet]
        [Route("GetNotes")]
        public JsonResult GetNotes()
        {
            // Define a consulta SQL para selecionar todas as notas na tabela dbo.Notes
            string query = "select * from dbo.Notes";

            // Cria um novo objeto DataTable para armazenar os resultados da consulta SQL
            DataTable table = new DataTable();

            // Obtém a string de conexão do arquivo de configuração(appsettings.json) usando a chave "todoAppDBCon"
            string sqlDataSource = _configuration.GetConnectionString("todoAppDBCon");

            // Declara um objeto SqlDataReader para armazenar os resultados da consulta SQL
            SqlDataReader myReader;

            // Usa a instrução 'using' para garantir que o recurso SqlConnection seja fechado corretamente após o uso
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                // Abre a conexão com o banco de dados
                myCon.Open();

                // Cria um novo objeto SqlCommand com a consulta SQL e a conexão SQL
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    // Executa a consulta SQL e armazena o resultado no SqlDataReader
                    myReader = myCommand.ExecuteReader();

                    // Carrega os dados do SqlDataReader para o DataTable
                    table.Load(myReader);

                    // Fecha o SqlDataReader
                    myReader.Close();

                    // Fecha a conexão com o banco de dados
                    myCon.Close();
                }
            }
            // Retorna um JsonResult contendo os dados do DataTable (que representam todas as notas)
            return new JsonResult(table);
        }

        // ROTA PARA ADICIONAR UMA NOVA NOTA
        [HttpPost]
        [Route("AddNotes")]
        public JsonResult AddNotes([FromForm] string newNotes)
        {
            // Define a consulta SQL para inserir uma nova nota na tabela dbo.Notes
            string query = "insert into dbo.Notes values(@newNotes)";

            // Cria um novo objeto DataTable para armazenar os resultados da consulta SQL
            DataTable table = new DataTable();

            // Obtém a string de conexão do arquivo de configuração (appsettings.json) usando a chave "todoAppDBCon"
            string sqlDataSource = _configuration.GetConnectionString("todoAppDBCon");

            // Declara um objeto SqlDataReader para armazenar os resultados da consulta SQL
            SqlDataReader myReader;

            // Usa a instrução 'using' para garantir que o recurso SqlConnection seja fechado corretamente após o uso
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                // Abre a conexão com o banco de dados
                myCon.Open();

                // Cria um novo objeto SqlCommand com a consulta SQL e a conexão SQL
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    // Adiciona um parâmetro @newNotes à consulta SQL e define seu valor como o valor recebido do formulário
                    myCommand.Parameters.AddWithValue("@newNotes", newNotes);

                    // Executa a consulta SQL e armazena o resultado no SqlDataReader
                    myReader = myCommand.ExecuteReader();

                    // Carrega os dados do SqlDataReader para o DataTable
                    table.Load(myReader);

                    // Fecha o SqlDataReader
                    myReader.Close();

                    // Fecha a conexão com o banco de dados
                    myCon.Close();
                }
            }

            // Retorna um JsonResult indicando que a nota foi adicionada com sucesso
            return new JsonResult("Adicionado com sucesso!");
        }


        // DELETAR UMA NOTA
        [HttpDelete]
        [Route("DeleteNotes")]
        public JsonResult DeleteNotes(int id)
        {
            // Define a consulta SQL para excluir uma nota da tabela dbo.Notes com base no ID fornecido
            string query = "delete from dbo.Notes where id=@id";

            // Cria um novo objeto DataTable para armazenar os resultados da consulta SQL
            DataTable table = new DataTable();

            // Obtém a string de conexão do arquivo de configuração (appsettings.json) usando a chave "todoAppDBCon"
            string sqlDataSource = _configuration.GetConnectionString("todoAppDBCon");

            // Declara um objeto SqlDataReader para armazenar os resultados da consulta SQL
            SqlDataReader myReader;

            // Usa a instrução 'using' para garantir que o recurso SqlConnection seja fechado corretamente após o uso
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                // Abre a conexão com o banco de dados
                myCon.Open();

                // Cria um novo objeto SqlCommand com a consulta SQL e a conexão SQL
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    // Adiciona um parâmetro @id à consulta SQL e define seu valor como o ID recebido como entrada
                    myCommand.Parameters.AddWithValue("@id", id);

                    // Executa a consulta SQL e armazena o resultado no SqlDataReader
                    myReader = myCommand.ExecuteReader();

                    // Carrega os dados do SqlDataReader para o DataTable
                    table.Load(myReader);

                    // Fecha o SqlDataReader
                    myReader.Close();

                    // Fecha a conexão com o banco de dados
                    myCon.Close();
                }
            }

            // Retorna um JsonResult indicando que a nota foi deletada com sucesso
            return new JsonResult("Deletado com sucesso!");
        }


        // ROTA PARA EDITAR UMA NOTA
        [HttpPut]
        [Route("EditNotes/{id}")]
        public JsonResult EditNotes(int id, [FromForm] string updatedNotes)
        {
            // Define a consulta SQL para atualizar uma nota na tabela dbo.Notes com base no ID fornecido
            string query = "update dbo.Notes set description = @updatedNotes where id=@id";

            // Obtém a string de conexão do arquivo de configuração (appsettings.json) usando a chave "todoAppDBCon"
            string sqlDataSource = _configuration.GetConnectionString("todoAppDBCon");

            // Declara um objeto SqlDataReader para armazenar os resultados da consulta SQL
            SqlDataReader myReader;

            // Usa a instrução 'using' para garantir que o recurso SqlConnection seja fechado corretamente após o uso
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                // Abre a conexão com o banco de dados
                myCon.Open();

                // Cria um novo objeto SqlCommand com a consulta SQL e a conexão SQL
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    // Adiciona parâmetros @id e @updatedNotes à consulta SQL e define seus valores
                    myCommand.Parameters.AddWithValue("@id", id);
                    myCommand.Parameters.AddWithValue("@updatedNotes", updatedNotes);

                    // Executa a consulta SQL
                    int rowsAffected = myCommand.ExecuteNonQuery();

                    // Fecha a conexão com o banco de dados
                    myCon.Close();

                    // Se nenhuma linha foi afetada pela atualização, retorna um erro indicando que o registro não foi encontrado
                    if (rowsAffected == 0)
                    {
                        return new JsonResult("Registro não encontrado.");
                    }
                }
            }

            // Retorna um JsonResult indicando que a nota foi atualizada com sucesso
            return new JsonResult("Atualizado com sucesso!");
        }

    }
}
