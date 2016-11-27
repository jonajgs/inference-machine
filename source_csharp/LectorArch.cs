/*
 * LectorArch
 * 
 * Clase auxiliar responsable de recuperar información
 * contenida en un archivo específico. 
 * 
 * En particular, el módulo de conocimiento de este
 * gestor genérico para sistema experto se apoya en los
 * servicios de esta clase para recuperar la base de
 * conocimiento del sistema de archivos.
 * 
 * Desarrollador: Luis Alberto Casillas Santillán
 * Fecha: 29/10/2006
 * Hora: 07:01 a.m.
 * 
 */

using System;
using System.IO;

namespace Experto{	
	public class LectorArch{
		StreamReader sr;
		internal LectorArch(string nomArch){
			try{
				sr=new StreamReader(nomArch);
			}catch(FileNotFoundException e){
				Console.WriteLine("Archivo {0} no encontrado",nomArch);
				Console.WriteLine("Sucedio {0}",e);
			 }
			 catch(IOException e){
				Console.WriteLine("Problema de Entrada Salida");
				Console.WriteLine("Sucedio {0}",e);
			 }
			 catch(Exception e){
				Console.WriteLine("Problemas varios");
				Console.WriteLine("Sucedio {0}",e);
			 }
		}
		internal string leeCad(){
			try{
				return sr.ReadLine();
			}catch(IOException e){
				Console.WriteLine("Problema de Entrada Salida");
				Console.WriteLine("Sucedio {0}",e);
			 }
			 catch(Exception e){
				Console.WriteLine("Problemas varios");
				Console.WriteLine("Sucedio {0}",e);
			 }
			 return null;
		}
		internal void cierra(){
			sr.Close();
		}		
	}
}
