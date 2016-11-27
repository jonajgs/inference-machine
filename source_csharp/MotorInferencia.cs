/*
 * MotorInferencia
 * 
 * El motor de inferencia es una clase esencial para este gestor
 * genérico de sistemas expertos. Eje de la inferencia y del 
 * proceso de confrontación de datos (almacenados en la memoria
 * de trabajo) con el conocimiento ontológico almacenado en la base 
 * de conocimiento, para producir el resultado meta.
 * 
 * Específicamente este motor de inferencia cuenta con los
 * operadores responsables de realizar encadenamiento hacia 
 * adelante (modus ponens) y hacia atrás (modus ponens invertido), 
 * así como los operadores auxiliares para cumplir estas tareas:
 * concatenar y esElegible.
 * 
 * 
 * Desarrollador: Luis Alberto Casillas Santillán
 * Fecha: 29/10/2006
 * Hora: 07:17 a.m.
 * 
 */

using System;
using System.Collections;

namespace Experto{	
	
	public class MotorInferencia{
		bool backward=false;
		internal ArrayList encadenarAdelante(ModuloConocimiento mc,
		                                     MemoriaTrabajo mt){
			Regla ra=null;
			Atomo aa=null;
			bool resConsulta=false;
			bool resCondicion=false;
			foreach(object elemento in mc.bc){
				ra=(Regla)elemento;
				foreach(object elemCond in ra.partesCond){
					if (elemCond is Atomo){
						aa=(Atomo)elemCond;
						aa=new Atomo(aa.Desc,aa.Estado,aa.Objetivo);
						if (!mt.presente(aa)){							
							resConsulta=Consultar.porAtomo(aa,ra);
							// Verificación de certidumbre: [0,1] elem R			
							aa.Estado=resConsulta;							
							try{
								//Console.WriteLine("Guardando {0}",aa);
								// El átomo clonado debería llevar 
								// la certidumbre.
								mt.guardaAtomo(new Atomo(aa));
								Console.WriteLine("MT {0}",mt);
							}catch(AtomoDuplicado ad){
								Console.WriteLine("Se duplico el atomo: {0}",aa);
								// Hacer nada...								
							}
						}
					}
				}
				resCondicion=ra.probarCondicion(mt);
				if (resCondicion){
					Console.WriteLine("Se disparo: "+ra);
					// Antes de llamar a ra.dispara(mt)
					// calcular certidumbre del resultado.
					// Debería enviarse como otro parámetro.
					ra.dispara(mt);
					//Console.WriteLine(mt);
					resCondicion=false;
					if (ra.esObjetivo()) return ra.partesConc;
				}
				else{
					// Este es uno de los interruptores del SE
					// Si se comenta, el experto consulta toda
					// la BC' aunque una regla falle.
					if (backward) return null;
					Console.WriteLine("No se disparo...");
					// si la condicion no se cumple, se almacenan
					// los atomos de la conclusion negados
					// esta seccion es opcional y debatible.
					// Esta sección sólo funciona si la línea: if (backward) return null;
					// está comentada...
					/*Atomo ac=null;
					ArrayList atomos=new ArrayList();
					foreach(object pc in ra.partesConc){
						if (pc is Atomo){
							atomos.Add(ac=new Atomo((Atomo)pc));
						}
						if (pc is Negacion){
							ac.Estado=!ac.Estado;
						}
					}
					Console.WriteLine("Hay {0} atomos...",atomos.Count);
					for(int i=0;i<atomos.Count;i++){						
						Console.WriteLine("Por negar: ",atomos[i]);
						ac=new Atomo((Atomo)atomos[i]);
						ac.Estado=!ac.Estado;
						Console.WriteLine("Negado: ",ac.Desc);
						try{
							mt.guardaAtomo(ac);
							Console.WriteLine("Guardado: ",ac);
						}catch(AtomoDuplicado ad){
							Console.WriteLine("Se duplico el atomo: {0}",ac);
							// Hacer nada...
						}
						
					} */
				  }
				}
			return null;
		}
		
		private void concatena(ArrayList destino,ArrayList fuente){
			Atomo aTmp=null;
			//Negacion nTmp=null;
			foreach(object aAgregar in fuente){
				if (aAgregar is Atomo){
					aTmp=new Atomo((Atomo)aAgregar);
					destino.Add(aTmp);
				}
				// Ojo, aquí es donde se niega la BC. Debes arreglarlo
				// Parece que ya...
				if (aAgregar is Negacion)
					aTmp.Estado=!aTmp.Estado;
			}
		}
		
		private bool esElegible(Regla r,ArrayList porSatisfacer){
			ArrayList atomosConc=new ArrayList();
			Atomo aTmp=null;
			foreach(object aa in r.partesConc){
				if (aa is Atomo){
					aTmp=new Atomo((Atomo)aa);
					atomosConc.Add(aTmp);
				}
				if (aa is Negacion) aTmp.Estado=!aTmp.Estado;
			}
			foreach(Atomo aa in atomosConc){
				if (porSatisfacer.Contains(aa)) return true;
			}
			return false;
		}
		
		internal ArrayList encadenarAtras(ModuloConocimiento mc,
		                                     MemoriaTrabajo mt){
			ArrayList reglasObj=mc.filtrarObjs();
			ArrayList aSatisfacer=new ArrayList();
			ArrayList bcPrima=new ArrayList();
			ArrayList resultado=null;
			string nomBCPrima=null;
			bool [] usadas=new bool[reglasObj.Count];
			bool salir=false;
			int pos=-1,veces=0,total=reglasObj.Count;
			Random r=new Random();
			backward=true;
			ModuloConocimiento mcTmp=null;
			do{
				pos=r.Next(total); //En lugar de esto, podría ir un
				                   //heurístico que elija un objetivo
				                   //con base en la evaluación del 
				                   //contexto situacional.
				if (!usadas[pos]){					
					veces++;
					usadas[pos]=true;
					aSatisfacer.Clear();
					bcPrima.Clear();
					mc.desmarcar();
					mc.quitarDisparos();
					foreach(ParteRegla pConc in ((Regla)reglasObj[pos]).partesConc){
						if (pConc is Atomo){
							Atomo aObj=(Atomo)pConc;
							if (aObj.Objetivo){
								nomBCPrima=aObj.Desc.ToUpper()+"";
							}
						}
					}
					mcTmp=new ModuloConocimiento(nomBCPrima);
					// Bootstrap!!!
					concatena(aSatisfacer,((Regla)reglasObj[pos]).partesConc);
					do{
						salir=true;
						foreach(Regla ra in mc.bc){
							if (!ra.marca&&esElegible(ra,aSatisfacer)){
								salir=false;
								Console.WriteLine("Elegida: {0}",ra);
								ra.marca=true;
								concatena(aSatisfacer,ra.partesCond);
								bcPrima.Insert(0,ra);
							}
						}
					}while(!salir);
					mcTmp.bc=bcPrima;
					Console.WriteLine("Intentando con:\n{0}",mcTmp);				
					resultado=encadenarAdelante(mcTmp,mt);
					if (resultado!=null){
						backward=false;
						return resultado;
					}
				}
			}while(veces<total);
			backward=false;
			return null;
		}
	}
}
