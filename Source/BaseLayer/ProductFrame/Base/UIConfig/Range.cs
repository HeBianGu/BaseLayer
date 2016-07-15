using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxRange : IBxRange
    {
        double? _min = 0;
        double? _max = 0;
        bool _minValid = false;
        bool _maxValid = false;

        public double? Min { get { return _min; } }
        public double? Max { get { return _max; } }
        public bool MinValid { get { return _minValid; } }
        public bool MaxValid { get { return _maxValid; } }

        public BxRange() { }
        public BxRange(double min, double max) { _min = min; _max = max; }
        public BxRange(double? min, bool minValid, double? max, bool maxValid) { _min = min; _minValid = minValid; _max = max; _maxValid = maxValid; }

        public bool IsValid(double val)
        {
            if (_min.HasValue)
            {
                if (_minValid && (val < _min.Value))
                    return false;
                else if (!_minValid && (val <= _min.Value))
                    return false;
            }

            if (_max.HasValue)
            {
                if (_maxValid && (val > _max.Value))
                    return false;
                else if (!_maxValid && (val >= _max.Value))
                    return false;
            }

            return true;
        }
    }

}
