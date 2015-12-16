using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Encomenda
    {

        public String CDU_Ref
        {
            get;
            set;
        }


        public DateTime CDU_Data
        {
            get;
            set;
        }

        public List<Artigo> Artigos
        {
            get;
            set;
        }

    }
}