using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_EXAMPLE_WS.Models
{
    public class Encomenda
    {
        public string Ref;

        public string PrazoPicking
        {
            get;
            set;
        }

        public string DataOrder
        {
            get;
            set;
        }

        public string NumContribuinte
        {
            get;
            set;
        }

        public Boolean Status
        {
            get;
            set;
        }

        public int Prioridade
        {
            get;
            set;
        }

    }
}