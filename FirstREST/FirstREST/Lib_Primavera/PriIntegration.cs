using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS800;
using Interop.StdPlatBS800;
using Interop.StdBE800;
using Interop.GcpBE800;
using ADODB;
using Interop.IGcpBS800;
//using Interop.StdBESql800;
//using Interop.StdBSSql800;

namespace FirstREST.Lib_Primavera
{
    public class PriIntegration
    {
        # region Funcionario

        public static bool Login(string username, string password)
        {
            StdBELista funcList;

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                funcList = PriEngine.Engine.Consulta("SELECT CDU_Tipo FROM  Funcionarios WHERE Codigo=" + username + " and CDU_Password=" + password);

                if (funcList.NumLinhas() > 0)
                    return true;
                else
                    return false;
            }

            return false;
        }

        # endregion Funcionario
        


        # region Cliente

        public static List<Model.Cliente> ListaClientes()
        {
            
            
            StdBELista objList;

            List<Model.Cliente> listClientes = new List<Model.Cliente>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, Fac_Mor AS campo_exemplo FROM  CLIENTES");

                
                while (!objList.NoFim())
                {
                    listClientes.Add(new Model.Cliente
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

        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {
            

            GcpBECliente objCli = new GcpBECliente();


            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.Moeda = objCli.get_Moeda();
                    myCli.NumContribuinte = objCli.get_NumContribuinte();
                    myCli.Morada = objCli.get_Morada();
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
           

            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);
                        objCli.set_Morada(cliente.Morada);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }



        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            

            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);
                    myCli.set_Morada(cli.Morada);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }

       

        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------



        #region Artigo

        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {
            
            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();

                    return myArt;
                }
                
            }
            else
            {
                return null;
            }

        }

        public static List<Model.Artigo> ListaArtigos()
        {
                        
            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("artigo");
                    art.DescArtigo = objList.Valor("descricao");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        #endregion Artigo

   

        #region DocCompra
        
        public static List<Model.DocCompra> putaway_list()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dv = new Model.DocCompra();
            List<Model.DocCompra> listdv = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindv = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindv = new
            List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, NumDoc, HoraDescarga, DataDescarga From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocCompra();
                    dv.Colocados = 0;
                    dv.PorColocar = 0;
                    dv.id = objListCab.Valor("id");
                    dv.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.HoraDescarga = objListCab.Valor("HoraDescarga");
                    dv.DataDescarga = objListCab.Valor("DataDescarga");
                    objListLin = PriEngine.Engine.Consulta("SELECT CDU_idCabecDoc, CDU_CodArtigo, CDU_Quantidade, CDU_Estado from TDU_Putaway where CDU_idCabecDoc='" + dv.id + "'");
                    listlindv = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocCompra();
                        lindv.IdCabecDoc = objListLin.Valor("CDU_idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("CDU_Artigo");
                        lindv.Quantidade = objListLin.Valor("CDU_Quantidade");
                        lindv.Status = objListLin.Valor("CDU_Estado");

                        if (lindv.Status == 0)
                            dv.PorColocar++;
                        else
                            dv.Colocados++;

                        StdBELista objArmazLista;
                        objArmazLista = PriEngine.Engine.Consulta("SELECT Fila, Slot, Nivel FROM ArtigoArmazem WHERE Artigo=" + objListLin.Valor("CDU_Artigo"));
                        lindv.Fila = objArmazLista.Valor("Fila");
                        lindv.Slot = objArmazLista.Valor("Slot");
                        lindv.Nivel = objArmazLista.Valor("Nivel");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }

            return listdv;
        }


        public static Model.DocCompra putaway_get(string numDoc)
        {
            return new Model.DocCompra();
        }

        #endregion DocCompra



        #region DocsVenda

        public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();
             
            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();
             
            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();
            
            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                   // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static List<Model.DocVenda> Encomendas_List()
        {
            
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='FA'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.Aprovados = 0;
                    dv.Pendentes = 0;
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, CDU_Status from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindv.Status = objListLin.Valor("CDU_Status");

                        if (lindv.Status == "0")
                            dv.Aprovados++;
                        else
                            dv.Pendentes++;

                        StdBELista objArtList;
                        objArtList = PriEngine.Engine.Consulta("SELECT CDU_Cor, CDU_Tamanho FROM Artigo WHERE Artigo=" + objListLin.Valor("Artigo"));
                        lindv.Cor = objArtList.Valor("CDU_Cor"); ;
                        lindv.Tamanho = objArtList.Valor("CDU_Tamanho");

                        StdBELista objArmazLista;
                        objArmazLista = PriEngine.Engine.Consulta("SELECT Fila, Slot, Nivel FROM ArtigoArmazem WHERE Artigo=" + objListLin.Valor("Artigo"));
                        lindv.Fila = objArmazLista.Valor("Fila");
                        lindv.Slot = objArmazLista.Valor("Slot");
                        lindv.Nivel = objArmazLista.Valor("Nivel");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
                       
            return listdv;
        }

        public static Model.DocVenda Encomenda_Get(string numdoc)
        {
            
            
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='FA' and NumDoc='" + numdoc + "'";

               
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();



                if (objListCab.NumLinhas() == 0)
                {
                    return null;
                }

                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, CDU_Status from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();
                string objCor = "cenas", objTamanho = "36";
               

                while (!objListLin.NoFim())
                {                    
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    lindv.Status = objListLin.Valor("CDU_Status");

                    StdBELista objArtList;
                    objArtList = PriEngine.Engine.Consulta("SELECT CDU_Cor, CDU_Tamanho FROM Artigo WHERE Artigo=" + objListLin.Valor("Artigo"));
                    lindv.Cor = objArtList.Valor("CDU_Cor"); ;
                    lindv.Tamanho = objArtList.Valor("CDU_Tamanho");

                    StdBELista objArmazLista;
                    objArmazLista = PriEngine.Engine.Consulta("SELECT Fila, Slot, Nivel FROM ArtigoArmazem WHERE Artigo=" + objListLin.Valor("Artigo"));
                    lindv.Fila = objArmazLista.Valor("Fila");
                    lindv.Slot = objArmazLista.Valor("Slot");
                    lindv.Nivel = objArmazLista.Valor("Nivel");

                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }

        internal static bool Update_Encomenda(string Ref, string RefArt)
        {
            throw new NotImplementedException();
        }

        internal static IEnumerable<Model.DocVenda> EncomendasPendentes_List()
        {
            throw new NotImplementedException();
        }

        public static Boolean UpdateLinhaDocEstado(string codArtigo, int estado, string numDoc, string descricao)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();

            StdBELista LinhasDoc;
            StdBELista IDcabecDoc;

            var dv = new GcpBEDocumentoVenda();


            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                IDcabecDoc = PriEngine.Engine.Consulta("SELECT id from CabecDoc where TipoDoc='FA' AND NumDoc='" + numDoc + "'");

                string idCabecDoc = IDcabecDoc.Valor("id");

                GcpBELinhaDocumentoVenda ld = new GcpBELinhaDocumentoVenda();

                LinhasDoc = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + idCabecDoc + "'" + "AND Artigo=" + codArtigo);

                ld.set_Artigo(LinhasDoc.Valor("Arigo"));
                ld.set_Descricao(LinhasDoc.Valor("Descricao"));
                ld.set_Quantidade(LinhasDoc.Valor("Quantidade"));
                ld.set_Unidade(LinhasDoc.Valor("Unidade"));
                ld.set_PrecUnit(LinhasDoc.Valor("PrecUnit"));
                ld.set_Desconto1(LinhasDoc.Valor("Desconto1"));
                ld.set_TotalIliquido(LinhasDoc.Valor("TotalIliquido"));
                ld.set_PrecoLiquido(LinhasDoc.Valor("PrecoLiquido"));


                StdBECampos campos = new StdBECampos();
                StdBECampo camp_status = new StdBECampo();
                camp_status.Nome = "CDU_Status";
                camp_status.Valor = estado;
                campos.Insere(camp_status);
                StdBECampo camp_desc = new StdBECampo();
                camp_desc.Nome = ("CDU_Descricao");
                camp_desc.Valor = descricao;
                campos.Insere(camp_desc);

                ld.set_CamposUtil(campos);

                // PriEngine.Engine.Comercial.Vendas.


                return true;
            }

            return false;
        }

        #endregion DocsVenda
    }
}