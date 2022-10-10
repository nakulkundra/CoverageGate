using System;
using System.Collections.Generic;
using System.IO;

namespace CoverageGate
{
    class Program
    {
        static double gatePassStatements;
        static double gatePassBranches;
        static double gatePassFunctions;
        static double gatePassLines;
        static void Main(string[] args)
        {
            double statements = 0;
            double branches = 0;
            double functions = 0;
            double lines = 0;

            try
            {
                gatePassStatements = Convert.ToDouble(args[1]);
                gatePassBranches = Convert.ToDouble(args[2]);
                gatePassFunctions = Convert.ToDouble(args[3]);
                gatePassLines = Convert.ToDouble(args[4]);
            }
            catch
            {
                Console.WriteLine("Invalid Arguments");
                throw new ArgumentNullException();
            }

            try
            {
                foreach (string line in File.ReadLines(args[0]))
                {
                    if (line.Contains("Statements   : ") & line.Contains("%"))
                    {
                       // Console.WriteLine(line);
                        statements = Convert.ToDouble((line.Split(":")[1].Trim().Split("%")[0].Trim()));
                    }
                    if (line.Contains("Branches     : ") & line.Contains("%"))
                    {
                       // Console.WriteLine(line);
                        branches = Convert.ToDouble((line.Split(":")[1].Trim().Split("%")[0].Trim()));
                    }
                    if (line.Contains("Functions    : ") & line.Contains("%"))
                    {
                       // Console.WriteLine(line);
                        functions = Convert.ToDouble((line.Split(":")[1].Trim().Split("%")[0].Trim()));
                    }
                    if (line.Contains("Lines        : ") & line.Contains("%"))
                    {
                      //  Console.WriteLine(line);
                        lines = Convert.ToDouble((line.Split(":")[1].Trim().Split("%")[0].Trim()));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Looks like wrong file");
                Console.WriteLine(e.Message);
                throw e;
            }

            if (!CheckGate(statements, branches, functions, lines))
            {
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Coverage Dip Detected !!");
                Console.ResetColor();
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                throw new SystemException();
            }
            else
            {
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Coverage Passed !!");
                Console.ResetColor();
                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }
        }

        private static bool CheckGate(double statements, double branches, double functions, double lines)
        {
            bool gatePass = true;

            if (statements <= gatePassStatements)
            {
                gatePass = false;
            }
            if (gatePass && branches <= gatePassBranches)
            {
                gatePass = false;
            }
            if (gatePass && functions <= gatePassFunctions)
            {
                gatePass = false;
            }
            if (gatePass && lines <= gatePassLines)
            {
                gatePass = false;
            }

            return gatePass;
        }
    }
}
