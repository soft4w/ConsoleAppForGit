using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace ConsoleAppForGit
{
    class Program
    {



        static void Main(string[] args)
        {
            //string s = "4.1+(3+7.4*2)-6*(8-(9-2))";
            string s = "-11-3-5*23/12+4++3*123*34";



            Console.WriteLine(CalcWithoutBrackets(s));
                Console.ReadLine();
        



        }

        private static double CalcWithoutBrackets(string s)
        {
            List<string> expressionItems = new List<string>();

            Regex regexNumbers = new Regex(@"[0-9|\.]+|\+|\-|\*|\/");
            MatchCollection matchesNums = regexNumbers.Matches(s);
            foreach (Match m in matchesNums)
            {
                expressionItems.Add(m.Value);
            }

            if (expressionItems[0] == "-")
            {
                expressionItems.RemoveAt(0);
                expressionItems[0] = String.Concat("-", expressionItems[0]);
            }

            double tempOperandLeft;
            double tempOperandRight;
            double tempResult = 0;
            while (expressionItems.Any(o => o == "*" || o == "/"))
            {
                string operandStr = expressionItems.Find(o => o == "*" || o == "/");
                int i = expressionItems.IndexOf(operandStr);
                if (/*!startsWithMinus &&*/
                    Double.TryParse(expressionItems[i - 1], out tempOperandLeft) &&
                    Double.TryParse(expressionItems[i + 1], out tempOperandRight))
                {
                    switch (expressionItems[i])
                    {
                        case "*":
                            {
                                tempResult = tempOperandLeft * tempOperandRight;
                                expressionItems.RemoveRange(i - 1, 3);
                                expressionItems.Insert(i - 1, tempResult.ToString());
                            }
                            break;
                        case "/":
                            {
                                tempResult = tempOperandLeft / tempOperandRight;
                                expressionItems.RemoveRange(i - 1, 3);
                                expressionItems.Insert(i - 1, tempResult.ToString());
                            }
                            break;
                    }
                }
            }

            while (expressionItems.Any(o => o == "+" || o == "-"))
            {
                string operandString = expressionItems.Find(o => o == "+" || o == "-");
                int indexOfOperand = expressionItems.IndexOf(operandString);
                if (/*!startsWithMinus &&*/
                    Double.TryParse(expressionItems[indexOfOperand - 1], out tempOperandLeft) &&
                    Double.TryParse(expressionItems[indexOfOperand + 1], out tempOperandRight))
                {
                    switch (expressionItems[indexOfOperand])
                    {
                        case "+":
                            {
                                tempResult = tempOperandLeft + tempOperandRight;
                                expressionItems.RemoveRange(indexOfOperand - 1, 3);
                                expressionItems.Insert(indexOfOperand - 1, tempResult.ToString());
                            }
                            break;
                        case "-":
                            {
                                tempResult = tempOperandLeft - tempOperandRight;
                                expressionItems.RemoveRange(indexOfOperand - 1, 3);
                                expressionItems.Insert(indexOfOperand - 1, tempResult.ToString());
                            }
                            break;
                    }
                }

            }

            return tempResult;
        }

    }
}

#region obsolete variant
//string s = "3+7.4*2";
//List<double> nums = new List<double>();
//List<string> operands = new List<string>();
//bool offsetNeed = nums.Count == operands.Count;

//Regex regexNumbers = new Regex(@"[0-9|\.]+|\+|\-|\*|\/");
//MatchCollection matchesNums = regexNumbers.Matches(s);
//Regex regexOperands = new Regex(@"\+|\-|\*|\/");
//MatchCollection matchesOperands = regexOperands.Matches(s);

//foreach (Match m in matchesNums)
//    Console.WriteLine(m.Value);

//double tempNum;
//foreach (Match m in matchesNums)
//{
//    if (Double.TryParse(m.Value, out tempNum))
//        nums.Add(tempNum);
//}
//foreach (Match m in matchesOperands)
//{
//    operands.Add(m.Value);
//}


//while (operands.Any(o => o == "*" || o == "/"))
//{
//    int indexOfFirstOperation= operands.FindIndex(o => o == "*" || o == "/");
//    if(offsetNeed)
//    {
//        switch (operands[indexOfFirstOperation])
//        {
//            case "*":
//                {

//                }
//                break;
//            case "/":
//                {
//                }
//                break;
//        }
//    }
//    else
//    {

//    }
//}
#endregion