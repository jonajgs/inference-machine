/*
 * Operador
 * 
 * Clase auxiliar que representa a cualquiera de los operadores
 * lógicos que pueden utilizarse en las expresiones lógicas
 * utilizadas tanto en las condiciones compuestas como en las
 * conclusiones múltiples soportadas por la representación
 * de conocimiento LM-Regla.
 * 
 * Específicamente representa en un primer acercamiento una
 * negación (operador unario) y un operador binario, que a su 
 * vez puede ser de dos naturalezas: conjunción o disyunción.
 * 
 * Es utilizado en las reglas y es congruente con un enfoque
 * polimorfo, por tal motivo esta sola clase representa diferentes
 * tipos de operadores.
 * 
 * Desarrollador: Luis Alberto Casillas Santillán
 * Fecha: 26/10/2006
 * Hora: 07:53 a.m.
 * 
 */

using System;

namespace Experto{	
	public class Operador:ParteRegla{
		public Operador(){
		}
	}
	public class Negacion:Operador{
		public Negacion(){			
		}
		public override string ToString(){
			return "!";
		}
	}
	public class Binario:Operador{
		internal bool conj;
		// Las instancias en creadas con "true" representan una
		// conjunción, con "false" representan una disyunción.
		public Binario(bool conj){
			this.conj=conj;
		}
		public override string ToString(){
			return conj?"&":"|";
		}
	}
}
