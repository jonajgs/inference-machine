/*
 * MemoriaTrabajo
 * 
 * Clase esencial de este entorno genérico para sistemas
 * expertos. Responsable de contener los datos concretos
 * del caso de la realidad que actualmente procesa este
 * sistema experto.
 * 
 * En particular, cuando el sistema experto está verificando
 * diferentes hechos de la realidad; almacena en esta 
 * estructura los átomos que se han cotejado por medio de un
 * simple interrogatorio, o bien cuando una regla se dispara
 * y los átomos contenidos en su conclusión se almacenan.
 * 
 * Es capaz de controlar los átomos que se han afirmado y 
 * negado en el proceso, con el fin de lograr un tratamiento
 * preciso de la lógica modelada en la ontología del sistema
 * experto.
 * 
 * Desarrollador: Luis Alberto Casillas Santillán
 * Fecha: 28/10/2006
 * Hora: 05:39 p.m.
 * 
 */

using System;
using System.Collections;

namespace Experto{	
	public class MemoriaTrabajo{
		ArrayList afirmados;
		ArrayList negados;
		internal MemoriaTrabajo(){
			afirmados=new ArrayList();
			negados=new ArrayList();
		}
		internal void guardaAtomo(Atomo aa){
			if (!afirmados.Contains(aa)&&!negados.Contains(aa)){
				if (aa.Estado) afirmados.Add(aa);
				else negados.Add(aa);
			}else{
				throw new AtomoDuplicado(aa.Desc);
			}
		}
		internal bool presente(Atomo aa){
			Atomo aTmp=new Atomo(aa);
			aTmp.Estado=!aTmp.Estado;
			return (afirmados.Contains(aa)||negados.Contains(aa)||
			        afirmados.Contains(aTmp)||negados.Contains(aTmp));
		}
		internal bool fueAfirmado(Atomo aa){
			return afirmados.Contains(aa);
		}
		internal bool fueNegado(Atomo aa){
			return negados.Contains(aa);
		}
		internal Atomo recupera(Atomo aa){
			int pa=afirmados.IndexOf(aa);
			int pn=negados.IndexOf(aa);
			if (pa>-1) return (Atomo)afirmados[pa];
			if (pn>-1) return (Atomo)negados[pn];
			return null;
		}
		public override string ToString(){
			string retorno="\nMemoria de Trabajo\nAfirmados: [ ";
			foreach(Atomo a in afirmados) retorno+=(a+" ");
			retorno+="]\nNegados: [ ";
			foreach(Atomo a in negados) retorno+=(a+" ");
			retorno+="]";
			return retorno;
		}
	}
}
