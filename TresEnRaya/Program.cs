using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRaya
{
    internal class Program
    {
        static int[,] tablero;
        static char[] simbolos = { '.', 'O', 'X' };
        static int nPlayerActual = 1;
        static bool bTerminado = false;

        static void Main(string[] args)
        {
            // Inicializamos variables
            
            tablero = new int[3, 3];

            while (!bTerminado)
            {
                GenerarPantalla();
                ComprobarInputUsuario();
                ComprobarEstado();
            }
        }

        private static void GenerarPantalla()
        {
            Console.WriteLine();
            
            // Recorremos todas las filas y por cada fila todas las columnas
            // y mostramos el contenido del tablero

            for (int nFila = 0; nFila < 3; nFila++)
            {             
                Console.WriteLine("-------------");
                Console.Write("|");
                for (int nColumna = 0; nColumna < 3; nColumna++)
                {

                    // De nuestros simbolos mostramos el que esta en la posicion del tablero
                    Console.Write(" " + simbolos[tablero[nFila, nColumna]] + " |");
                }
                Console.WriteLine();
                Console.WriteLine("-------------");
            }
        }

        private static void ComprobarInputUsuario()
        {
            bool bCasillaValida = false;
            int nFila;
            int nColumna;

            do
            {
                Console.Write("Jugador " + nPlayerActual + " - Introduce la fila (1 a 3): ");
                nFila = Convert.ToInt32(Console.ReadLine());
                Console.Write("Jugador " + nPlayerActual + " - Introduce la columna (1 a 3): ");
                nColumna = Convert.ToInt32(Console.ReadLine());

                if ((nFila >= 1) && (nFila <= 3)
                    && (nColumna >= 1) && (nColumna <= 3)
                    && tablero[nFila - 1, nColumna - 1] == 0)
                {
                    bCasillaValida = true;
                }
            }
            while (!bCasillaValida);

            // Colocamos el simbolo del jugador en la columna de la fila seleccionada
            tablero[nFila - 1, nColumna - 1] = nPlayerActual;
        }

        private static void ComprobarEstado()
        {
            bool bPartidaGanada = false;

            // Comprobamos si el jugador actual ha ganado
            // Comprobamos filas
            for (int nFila = 0; nFila < 3; nFila++)
            {
                if ((tablero[nFila, 0] == tablero[nFila, 1])
                        && (tablero[nFila, 0] == tablero[nFila, 2])
                        && (tablero[nFila, 0] == nPlayerActual))
                    bPartidaGanada = true;
            }

            // Comprobamos columnas
            for (int nColumna = 0; nColumna < 3; nColumna++)
            {
                if ((tablero[0, nColumna] == tablero[1, nColumna])
                       && (tablero[0, nColumna] == tablero[2, nColumna])
                       && (tablero[0, nColumna] == nPlayerActual))
                    bPartidaGanada = true;
            }

            // Comprobamos diagonal
            if ((tablero[0, 0] == tablero[1, 1])
                       && (tablero[0, 0] == tablero[2, 2])
                       && (tablero[0, 0] == nPlayerActual))
                bPartidaGanada = true;

            // Comprobamos diagonal opuesta
            if ((tablero[0, 2] == tablero[1, 1])
                       && (tablero[0, 2] == tablero[2, 0])
                       && (tablero[0, 2] == nPlayerActual))
                bPartidaGanada = true;

            if (bPartidaGanada)
            {
                GenerarPantalla();
                Console.WriteLine("Has ganado jugador" + nPlayerActual);
                bTerminado = true;
                Console.ReadLine();
            }

            // Comprobamos si los jugadores han empatado
            if (!bPartidaGanada)
            {
                int nEspaciosLibres = 0;
                for (int nFila = 0; nFila < 3; nFila++)
                {
                    for (int nColumna = 0; nColumna < 3; nColumna++)
                    {
                        // De nuestros simbolos mostramos el que esta en la posicion del tablero
                        if (tablero[nFila, nColumna] == 0)
                            nEspaciosLibres++;
                    }
                }

                if (nEspaciosLibres == 0)
                {
                    GenerarPantalla();
                    Console.WriteLine("Empate");
                    bTerminado = true;
                }
            }

            // Si no se cumple ninguna condicion anterior cambiamos turno            
            if (nPlayerActual == 1)
                nPlayerActual = 2;
            else
                nPlayerActual = 1;
        }
    }
}
