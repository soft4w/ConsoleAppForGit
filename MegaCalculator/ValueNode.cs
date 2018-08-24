using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaCalculator
{
    public class ValueNode : INode
    {
        private double _value;

        public ValueNode(double val)
        {
            _value = val;
        }
        public double GetValue()
        {
            return _value;
        }
    }
}