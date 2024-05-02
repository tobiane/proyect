using System;
using System.Collections.Generic;
using System.Threading;

namespace copos
{
    public class Copo
    {
        public int col = 0;
        public int fila = 0;
        public bool derecha = true;

        public Copo(int col, int fila)
        {
            this.col = col;
            this.fila = fila;
        }
    }

    internal class Program
    {
        static TimeSpan transcurso;
        static DateTime h1 = DateTime.Now;
        static List<Copo> copos = new List<Copo>();
        static Random r = new Random();

        static bool Bajar(Copo c)
        {
            bool estado = false;
            if (c.fila < Console.WindowHeight - 1) // Consideramos el tamaño de la ventana de la consola
                estado = true;

            foreach (Copo co in copos)
            {
                if (c.col == co.col && c.fila + 1 == co.fila)
                {
                    estado = false;
                    break; // Salir del bucle una vez que se encuentre un obstáculo
                }
            }
            return estado;
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            while (true)
            {
                DateTime h2 = DateTime.Now;
                transcurso = h2 - h1;

                if (transcurso.Milliseconds > 10)
                {
                    // Creamos un nuevo copo aleatorio
                    Copo nuevoCopo = new Copo(r.Next(1, Console.WindowWidth - 1), 1);
                    if (Bajar(nuevoCopo))
                        copos.Add(nuevoCopo);

                    // Limpiamos la pantalla
                    Console.Clear();

                    // Dibujamos todos los copos
                    foreach (Copo copo in copos)
                    {
                        Console.SetCursorPosition(copo.col, copo.fila);
                        Console.Write("*");
                    }

                    // Esperamos un poco antes de la siguiente iteración
                    Thread.Sleep(100);

                    // Actualizamos la posición de los copos
                    foreach (Copo copo in copos)
                    {
                        if (Bajar(copo))
                        {
                            Console.SetCursorPosition(copo.col, copo.fila);
                            Console.Write(" ");
                            copo.fila++;
                        }
                    }
                    h1 = h2;
                }
            }
        }
    }
}
