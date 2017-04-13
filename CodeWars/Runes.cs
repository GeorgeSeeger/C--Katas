using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Runes
    {
        //https://www.codewars.com/kata/546d15cebed2e10334000ed9/train/csharp
        public static int solveExpression(string expression)
        {
            var statements = expression.Split('=');
            var sum = statements.First();
            var result = statements.Last();
            var digits = "0123456789".Where(c => !expression.Contains(c)).ToList();
            foreach (var digit in digits)
            {
                if (Evaluate(sum.Replace('?', digit)) == Evaluate(result.Replace('?', digit)))
                {
                    if (!(digit == '0' && result.Replace('?', '0').Contains("00")))
                    {
                        return int.Parse(digit.ToString());
                    }
                }
            }
            return -1;
        }

        private static int Evaluate(string expression)
        {
            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(int), expression);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return (int)(loDataTable.Rows[0]["Eval"]);
        }

        public static void Main(string[] args)
        {
            
        }
    }
}
