using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp
{
    public class Thickness
    {
        private double _left;
        private double _top;
        private double _right;
        private double _bottom;

        public double Left { get { return _left; } }
        public double Top { get { return _top; } }
        public double Right { get { return _right; } }
        public double Bottom { get { return _bottom; } }

        public Thickness(double uniformsThickness)
        {
            _left = _top = _right = _bottom = uniformsThickness;
        }

        public Thickness(double left, double top, double right, double bottom)
        {
            _left = left;
            _top = top;
            _right = right;
            _bottom = bottom;
        }

    }
}
