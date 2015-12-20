using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class DocCompra
    {

        public string id
        {
            get;
            set;
        }

        public string NumDocExterno
        {
            get;
            set;
        }

        public int NumDoc
        {
            get;
            set;
        }

        public string HoraDescarga
        {
            get;
            set;
        }

        public string DataDescarga
        {
            get;
            set;
        }

        public List<Model.LinhaDocCompra> LinhasDoc
        {
            get;
            set;
        }

        public int Colocados
        {
            get;
            set;
        }

        public int PorColocar
        {
            get;
            set;
        }
    }
}