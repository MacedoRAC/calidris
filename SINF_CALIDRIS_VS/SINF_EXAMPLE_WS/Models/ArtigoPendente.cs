using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_EXAMPLE_WS.Models
{
    public class ArtigoPendente
    {
        public string EncRef;

        public string ArtRef;

        public string PrazoPicking
        {
            get;
            set;
        }

        public string Fila
        {
            get;
            set;
        }

        public string Slot
        {
            get;
            set;
        }

        public string Nivel
        {
            get;
            set;
        }

        public Boolean Pendente
        {
            get;
            set;
        }

        public int Quantidade
        {
            get;
            set;
        }

        public string Tamanho
        {
            get;
            set;
        }

        public string Cor
        {
            get;
            set;
        }

    }
}
