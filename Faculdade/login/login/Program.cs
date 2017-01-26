﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace login
{
    class Program
    {
        public struct tipo_usuario
        {
            public string usuario;
            public string senha;
        }

        static string localDados = @"C:/ProjetoIntegrador/Prog_Cond/";

        static string arquivoDadosUsuarios = @"Usuario.txt";

        static void Main(string[] args)
        {
            string pass = "", user = "", senha = "";

            do
            {
                pass = "";
                user = "";
                senha = "";



                Console.Write("Enter your username: ");
                user = Console.ReadLine();
                Console.Write("Enter your password: ");
                ConsoleKeyInfo key;

                do
                {
                    key = Console.ReadKey(true);

                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        pass += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                        {
                            pass = pass.Substring(0, (pass.Length - 1));
                            Console.Write("\b \b");
                        }

                    }
                }
                // Stops Receving Keys Once Enter is Pressed
                while (key.Key != ConsoleKey.Enter);

             senha = criptografia(pass);

                if (testaLogin(user, senha))
                {
                    Console.WriteLine("\nLogin Correct!");
                }
                else
                {
                    Console.WriteLine("\nLogin Incorrect");
                    Console.WriteLine("ACCESS DENIED");
                }
            } while (testaLogin(user, senha) != true);

            Console.WriteLine();

            Console.WriteLine("The Username You entered is : " + user);
            Console.WriteLine("The Password You entered is : " + pass);



            Console.Read();
            Console.ReadKey();


        }

        static bool testaLogin(string user, string pass)
        {
            StreamReader reader = new StreamReader(localDados + arquivoDadosUsuarios);
            int j = 0;
            while (reader.ReadLine() != null)
            {
                j++;
            }

            tipo_usuario[] usuario = new tipo_usuario[j/2];

            reader.Close();

            j = 0;

            reader = new StreamReader(localDados + arquivoDadosUsuarios);

            while (!reader.EndOfStream)
            {
                usuario[j].usuario = reader.ReadLine();
                usuario[j].senha = reader.ReadLine();
                j++;
            }



            for (int i = 0; i < usuario.Count(); i++)
            {
                if (usuario[i].usuario == user && usuario[i].senha == pass)
                {
                    return true;
                }
            }

            return false;
        }
        static string criptografia(string pass)
        {
            string texto_cript = "", texto_tabela = "";
            int chave = 13;



            //Pega o texto digitado pelo usuario e converto os espaços e pontuação para as letras da tabela
            texto_tabela = pass.ToUpper().Replace(" ", "WBRW").Replace(",", "WVRW").Replace(".", "WPTW").Replace(";", "WPVW").Replace(":", "WDPW").Replace("!", "WEXW").Replace("?", "WINW").Replace("-", "WHFW");


            for (int i = 0; i < texto_tabela.Length; i++)
            {
                //Devolve o codigo ASCII da letra
                int ASCII = (int)texto_tabela[i];

                //Coloca a chave fixa adicionando n posições no numero da tabela ASCII
                int ASCIIC = ASCII + chave;
                //subtrai 65 do numero obtido para que possa ser feito o MOD 26
                int texto_menos_65 = ASCIIC - 65;
                // faz o resto da divisao por 26 e obtem um numero de 0 a 25
                int mod = (texto_menos_65 % 26);
                //depois de obter um numero de 0 a 25, somamos 65 para obter o numero da letra em codigo ascii
                int texto_mais_65 = mod + 65;


                //Concatena o texto e o coloca na variável sem mod
                texto_cript += Char.ConvertFromUtf32(texto_mais_65);
            }


            return (texto_cript);





        }
    }

    }