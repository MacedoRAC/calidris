using Interop.StdBE800;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_EXAMPLE_WS.Models
{
    public class IntegrationPri
    {


        public static List<Cliente> ListaClientes()
        {


            StdBELista objList;

            List<Cliente> listClientes = new List<Cliente>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, Fac_Mor AS campo_exemplo FROM  CLIENTES");

                while (!objList.NoFim())
                {
                    listClientes.Add(new Cliente
                    {
                        CodCliente = objList.Valor("Cliente"),
                        NomeCliente = objList.Valor("Nome"),
                        Moeda = objList.Valor("Moeda"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        Morada = objList.Valor("campo_exemplo")
                    });
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }


        public static List<Encomenda> ListaEncomendasPendentes()
        {


            StdBELista objList;

            List<Encomenda> listEncomendas = new List<Encomenda>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Ref, PrazoPicking, DataOrder FROM  ENCOMENDAS WHERE Status = false orderBy Prioridade, PrazoPicking DESC");

                while (!objList.NoFim())
                {
                    listEncomendas.Add(new Encomenda
                    {
                        Ref = objList.Valor("Ref"),
                        PrazoPicking = objList.Valor("PrazoPicking"),
                        DataOrder = objList.Valor("DataOrder")
                    });
                    objList.Seguinte();

                }

                return listEncomendas;
            }
            else
                return null;
        }

        
        public static List<ArtigoPendente> ListaArtigosPendentes()
        {

            StdBELista objList;

            List<ArtigoPendente> listArtigos = new List<ArtigoPendente>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT ArtRef as Artigo, Quantidade, EncRef as Encomenda, PrazoPicking, Fila, Slot, Nivel FROM  ARTIGOS WHERE Pendente = true orderBy Fila, Slot, Nivel DESC");

                while (!objList.NoFim())
                {
                    listArtigos.Add(new ArtigoPendente
                    {
                        ArtRef = objList.Valor("ArtRef"),
                        Quantidade = objList.Valor("Quantidade"),
                        EncRef = objList.Valor("EncRef"),
                        PrazoPicking = objList.Valor("PrazoPicking"),
                        Fila = objList.Valor("Fila"),
                        Slot = objList.Valor("Slot"),
                        Nivel = objList.Valor("Nivel")
                    });
                    objList.Seguinte();

                }

                return listArtigos;
            }
            else
                return null;
        }

        public void UpdateArtigoStatus(string referencia, Boolean status, string excecao)
        {
            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {
               if(status)
               {
                   PriEngine.Engine.Consulta("UPDATE ARTIGOPENDENTE SET Pendente = false, Excecao = '' WHERE ArtRef = referencia");

                   StdBELista encreflist = PriEngine.Engine.Consulta("SELECT EncRef WHERE ArtRef = referencia");
                   string encref;
                   while (!encreflist.NoFim())
                   {
                       encref = encreflist.Valor("Ref");
                       encreflist.Seguinte();
                   }

                   StdBELista artigoslist = PriEngine.Engine.Consulta("SELECT ArtRef FROM ARTIGOPENDENTE WHERE EncRef = encref AND Pendente = true");
                   List<ArtigoPendente> artigos = new List<ArtigoPendente>();
                   while (!artigoslist.NoFim())
                   {
                       artigos.Add(new ArtigoPendente
                       {
                           ArtRef = artigoslist.Valor("ArtRef")
                       });
                       artigoslist.Seguinte();

                   }

                   if(!artigos.Any())
                   {
                       PriEngine.Engine.Consulta("UPDATE ENCOMENDA SET Status = false WHERE Ref  = encref");
                   }
               }
               else
               {
                   PriEngine.Engine.Consulta("UPDATE ARTIGOPENDENTE SET Excecao = excecao WHERE ArtRef = referencia");
               }
                
            }
        }
    
    }
}