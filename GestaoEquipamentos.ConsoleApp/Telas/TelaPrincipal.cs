﻿using GestaoEquipamentos.ConsoleApp.Controladores;
using System;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaPrincipal
    {
        private readonly ControladorChamado controladorChamado;
        ControladorEquipamento controladorEquipamento;
        ControladorSolicitante controladorSolicitante;
        private readonly TelaEquipamento telaEquipamento;
        private readonly TelaSolicitante telaSolicitante;

        public TelaPrincipal(ControladorEquipamento ctlrEquipamento,
            ControladorSolicitante ctlrSolicitante, TelaEquipamento tlEquipamento, TelaSolicitante tlSolicitante,
            ControladorChamado ctlrChamado)
        {
            controladorEquipamento = ctlrEquipamento;
            controladorSolicitante = ctlrSolicitante;
            telaEquipamento = tlEquipamento;
            telaSolicitante = tlSolicitante;
            controladorChamado = ctlrChamado;
        }

        public TelaBase ObterOpcao()
        {
            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.Clear();

                Console.WriteLine("Digite 1 para o Cadastro de Equipamentos");
                Console.WriteLine("Digite 2 para o Cadastro de Solicitante");
                Console.WriteLine("Digite 3 para o Controle de Chamados");

                Console.WriteLine("Digite S para Sair");

                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = new TelaEquipamento(controladorEquipamento);

                else if (opcao == "2")
                    telaSelecionada = new TelaSolicitante(controladorSolicitante);

                else if (opcao == "3")
                    telaSelecionada = new TelaChamado(telaEquipamento, telaSolicitante, controladorChamado);


                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "S" && opcao != "s")
            {
                Console.WriteLine("Opção inválida");
                Console.ReadLine();
                return true;
            }
            else
                return false;
        }
    }
}
