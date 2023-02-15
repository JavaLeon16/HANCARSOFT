using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class FCAPRODCAT011Entity
    {
		public string ClaveArea { get; set; }
		public string ClaveCargo { get; set; }
		public string ClaveDesperdicio { get; set; }
		public string NombreCargo { get; set; }
		public string NombreDesperdicio { get; set; }
		public string NombreArea { get; set; }
		public bool Balance { get; set; }
		public bool Estatus { get; set; }
		public int ObjetivoEst { get; set; }
		public int ObjetivoMax { get; set; }
	}

	public class FCAPRODCAT011DTO
	{
		public string ClaveArea { get; set; }
		public string ClaveCargo { get; set; }
		public bool Balance { get; set; }
		public int ObjetivoEst { get; set; }
		public int ObjetivoMax { get; set; }
	}

}
