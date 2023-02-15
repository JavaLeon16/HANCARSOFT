using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTO
{
    public class FCAPRODCAT016Entity
    {
		public int IdDesperdicio { get; set; }
		public string DescripcionDesperdicio { get; set; }
		public bool AplicaImpresora { get; set; }
		public bool AplicaCorrugadora { get; set; }
		public bool AplicaAcabado { get; set; }
		public bool AplicaRecuperacionCaja { get; set; }
		public bool Estatus { get; set; }
		public List<FCAPRODCAT011DTO> datConfiAreaDes { get; set; }


        FCAPRODCAT016Entity()
        {
			datConfiAreaDes = new List<FCAPRODCAT011DTO>();

		}

	}
}
