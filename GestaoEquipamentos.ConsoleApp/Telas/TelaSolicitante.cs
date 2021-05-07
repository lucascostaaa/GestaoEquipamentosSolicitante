using System;
using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaSolicitante : TelaBase
    {
        private ControladorSolicitante controladorSolicitante;
        public TelaSolicitante(ControladorSolicitante controlador)
            : base("Cadastro de Solicitante")
        {
            controladorSolicitante = controlador;
        }

        public override void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo solicitante...");

            bool conseguiuGravar = GravarSolicitante(0);

            if (conseguiuGravar)
                ApresentarMensagem("Solicitante inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o solicitante", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public override void EditarRegistro()
        {
            ConfigurarTela("Editando um Solicitante...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o id do solicitante que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarSolicitante(id);

            if (conseguiuGravar)
                ApresentarMensagem("Solcitante editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o Solicitante", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public override void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um Solicitante...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o ID do Solicitante que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorSolicitante.ExcluirSolicitante(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Solicitante excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o Solicitante", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public override void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando Solicitantes...");

            string configuracaColunasTabela = "{0,-10} | {1,-55} | {2,-35} | {3,-25}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Solicitante[] solicitante = controladorSolicitante.SelecionarTodosSolcitante();

            if (solicitante.Length == 0)
            {
                ApresentarMensagem("Nenhum SOLICITANTE cadastrado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < solicitante.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   solicitante[i].id, solicitante[i].nome, solicitante[i].email, solicitante[i].numeroTelefone);
            }
        }
        #region
        private bool GravarSolicitante(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            Console.Write("Digite o nome do Solicitante: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o E-mail do Solicitante: ");
            string email = (Console.ReadLine());

            Console.Write("Digite o número de Telefone do Solicitante: ");
            string numeroTelefone = Console.ReadLine();

            resultadoValidacao = controladorSolicitante.RegistrarSolicitante(
                id, nome, email, numeroTelefone);

            if (resultadoValidacao != "SOLICITANTE_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }
        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Nome", "E-mail", "Numero de Telefone");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
        #endregion
    }
}
