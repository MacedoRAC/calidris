using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{

    public class LinhaDocCompra
    {

        public string CodArtigo
        {
            get;
            set;
        }

        public string IdCabecDoc
        {
            get;
            set;
        }

        public double Quantidade
        {
            get;
            set;
        }

        public string Unidade
        {
            get;
            set;
        }

        public double Desconto
        {
            get;
            set;
        }

        public double PrecoUnitario
        {
            get;
            set;
        }

        public double TotalILiquido
        {
            get;
            set;
        }

        public double TotalLiquido
        {
            get;
            set;
        }

        public string Cor
        {
            get;
            set;
        }

        public string Tamanho
        {
            get;
            set;
        }

        public int Fila
        {
            get;
            set;
        }

        public int Slot
        {
            get;
            set;
        }

        public int Nivel
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }
    }
}