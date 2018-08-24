using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MegaCalculator
{
    class Program
    {
        // --------------------------------MAIN------------------
        static void Main(string[] args)
        {
            //Getting input string
            string _inputStr = GetUserString();

            //Separation string on members of expression
            //List<string> _parsedInput = ParseInputString(_inputStr);

            INode tree = GetTree(_inputStr);

           // Console.WriteLine(tree.GetValue());
            
            Console.ReadLine();
        }

        private static string GetUserString()
        {
            return "4+(-3.33+7*2)-6*(8-(9-2))";
            //return "4*(-2)";
            //return "3+4*2";
        }

        private static INode GetTree(string inputStr)
        {
            CalculatedNode tree = new CalculatedNode();

            //brackets block

            int brkCount = inputStr.Count((char c) => { return c == ')'; });
            for (int i = 0; i < brkCount; i++)
            {
                int openBrktInd = 0;
                int closeBrktInd = inputStr.IndexOf(')');
                for (int j = closeBrktInd; j > 0; j--)
                {
                    if (inputStr[j] != '(')
                        continue;
                    else
                    {
                        openBrktInd = j;
                        break;
                    }
                }
                string subExpression = inputStr.Substring(openBrktInd, closeBrktInd - openBrktInd + 1);
            }



            return tree;
        }
        

        private static double CalcWithoutBrackets(string inputStr)
        {
            string s = inputStr.Trim('(', ')');
            //Members of expression
            List<string> expressionItems = new List<string>();

            //Parsing string to members
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


            if (expressionItems.Count == 1 && Double.TryParse(expressionItems[0], out tempResult))
                return tempResult;

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


#region obsolete

//private static List<string> ParseInputString(string inputStr)
//{
//    List<string> result = new List<string>();
//    string _inputStrWithCalculatedBrackets = inputStr;
//    int brkCount = inputStr.Count((char c) => { return c == ')'; });

//    for (int i = 0; i < brkCount; i++)
//    {
//        int openBrktInd = 0;
//        int closeBrktInd = inputStr.IndexOf(')');
//        for (int j = closeBrktInd; j > 0; j--)
//        {
//            if (inputStr[j] != '(')
//                continue;
//            else
//            {
//                openBrktInd = j;
//                break;
//            }
//        }
//        string subExpression = inputStr.Substring(openBrktInd, closeBrktInd - openBrktInd + 1);
//        _inputStrWithCalculatedBrackets = _inputStrWithCalculatedBrackets.Replace(subExpression, CalcWithoutBrackets(subExpression).ToString());
//    }
//    return result;
//}

//private static double CalcWithoutBrackets(string inputStr)
//{
//    string s = inputStr.Trim('(', ')');
//    //Members of expression
//    List<string> expressionItems = new List<string>();

//    //Parsing string to members
//    Regex regexNumbers = new Regex(@"[0-9|\.]+|\+|\-|\*|\/");
//    MatchCollection matchesNums = regexNumbers.Matches(s);
//    foreach (Match m in matchesNums)
//    {
//        expressionItems.Add(m.Value);
//    }

//    if (expressionItems[0] == "-")
//    {
//        expressionItems.RemoveAt(0);
//        expressionItems[0] = String.Concat("-", expressionItems[0]);
//    }

//    double tempOperandLeft;
//    double tempOperandRight;
//    double tempResult = 0;


//    if (expressionItems.Count == 1 && Double.TryParse(expressionItems[0], out tempResult))
//        return tempResult;

//    while (expressionItems.Any(o => o == "*" || o == "/"))
//    {
//        string operandStr = expressionItems.Find(o => o == "*" || o == "/");
//        int i = expressionItems.IndexOf(operandStr);
//        if (/*!startsWithMinus &&*/
//            Double.TryParse(expressionItems[i - 1], out tempOperandLeft) &&
//            Double.TryParse(expressionItems[i + 1], out tempOperandRight))
//        {
//            switch (expressionItems[i])
//            {
//                case "*":
//                    {
//                        tempResult = tempOperandLeft * tempOperandRight;
//                        expressionItems.RemoveRange(i - 1, 3);
//                        expressionItems.Insert(i - 1, tempResult.ToString());
//                    }
//                    break;
//                case "/":
//                    {
//                        tempResult = tempOperandLeft / tempOperandRight;
//                        expressionItems.RemoveRange(i - 1, 3);
//                        expressionItems.Insert(i - 1, tempResult.ToString());
//                    }
//                    break;
//            }
//        }
//    }

//    while (expressionItems.Any(o => o == "+" || o == "-"))
//    {
//        string operandString = expressionItems.Find(o => o == "+" || o == "-");
//        int indexOfOperand = expressionItems.IndexOf(operandString);
//        if (/*!startsWithMinus &&*/
//            Double.TryParse(expressionItems[indexOfOperand - 1], out tempOperandLeft) &&
//            Double.TryParse(expressionItems[indexOfOperand + 1], out tempOperandRight))
//        {
//            switch (expressionItems[indexOfOperand])
//            {
//                case "+":
//                    {
//                        tempResult = tempOperandLeft + tempOperandRight;
//                        expressionItems.RemoveRange(indexOfOperand - 1, 3);
//                        expressionItems.Insert(indexOfOperand - 1, tempResult.ToString());
//                    }
//                    break;
//                case "-":
//                    {
//                        tempResult = tempOperandLeft - tempOperandRight;
//                        expressionItems.RemoveRange(indexOfOperand - 1, 3);
//                        expressionItems.Insert(indexOfOperand - 1, tempResult.ToString());
//                    }
//                    break;
//            }
//        }
//    }
//    return tempResult;
//}

#endregion
