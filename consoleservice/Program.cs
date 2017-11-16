using System;

namespace consoleservice
{
    class Program
    {
        static void Main(string[] args)
        {
            var servico = new ServiceReference1.validapontosSoapClient();

            var mensagem = servico.Cadastra(new ServiceReference1.Produto
            {
                ProdutoId = 152,
                Nome = "Produto teste",
                Nota = "ASDSAD21512212",
                Cnpj = "77.598.545/0001-76",
            });

            foreach (var item in mensagem)
            {
                Console.WriteLine("RETORNO DO SERVIÇO");
                Console.WriteLine($"Codigo: {item.Codigo}");
                Console.WriteLine($"Descrição: {item.Descricao}");
                Console.WriteLine("----------------------------------");
            }

            Console.ReadKey();
        }
    }
}
