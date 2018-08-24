using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaCalculator
{
    public class CalculatedNode : INode
    {
        public INode LeftSubNode
        {
            get; set;
        }

        public INode RightSubNode
        {
            get; set;
        }

        public OperationType Operation
        {
            get; set;
        }

        public double GetValue()
        {
            double result=0;
            switch(this.Operation)
            {
                case OperationType.Plus:
                    result = LeftSubNode.GetValue() + RightSubNode.GetValue();
                    break;
                case OperationType.Minus:
                    result = LeftSubNode.GetValue() - RightSubNode.GetValue();
                    break;
                case OperationType.Multiple:
                    result = LeftSubNode.GetValue() * RightSubNode.GetValue();
                    break;
                case OperationType.Divide:
                    if (RightSubNode.GetValue() != 0)
                        result = LeftSubNode.GetValue() / RightSubNode.GetValue();
                    break;
            }

            return result;
        }
    }
}