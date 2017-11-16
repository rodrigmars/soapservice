using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using WebApplication1.Entities;
using WebApplication1.Repositories;

namespace WebApplication1.WebService
{
    /// <summary>
    /// Summary description for validapontos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class validapontos : System.Web.Services.WebService
    {
        public Repository<Produto> repository { get => new Repository<Produto>(); }

        [WebMethod]
        public List<Ponto> Lista()
        {
            return new List<Ponto> {
                   new Ponto{  PontoId = 152, Nome = "Pasta", Valor = 26.50M },
                   new Ponto{  PontoId = 153, Nome = "Enxaguante Bucal", Valor = 45.50M },
                   new Ponto{  PontoId = 154, Nome = "Spray Bucal", Valor = 15.50M },
            };
        }

        [WebMethod]
        public List<Mensagem> Cadastra(Produto produto)
        {
            var mensagens = new List<Mensagem>();
            //Regex.IsMatch(campo, @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,100})");
            try
            {
                if (produto != null)
                {
                    if (produto.Nome.Trim() == "")
                    {
                        mensagens.Add(new Mensagem
                        {
                            Codigo = 999,
                            Descricao = "Campo nome vázio"
                        });
                    }

                    if (!Regex.IsMatch(produto.Cnpj.Trim(), @"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)"))
                    {
                        mensagens.Add(new Mensagem
                        {
                            Codigo = 999,
                            Descricao = "CNPJ INVÁLIDO"
                        });
                    }

                    if (mensagens.Count() == 0)
                    {
                        repository.Insere(produto);

                        mensagens.Add(new Mensagem
                        {
                            Codigo = 0,
                            Descricao = "Produto cadastrado com sucesso."
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                mensagens.Add(new Mensagem
                {
                    Codigo = 991,
                    Descricao = "Erro ao tentar cadastrar produto."
                });

                Log.Logging.Trace(ex);
            }
            return mensagens;
        }
    }
}
