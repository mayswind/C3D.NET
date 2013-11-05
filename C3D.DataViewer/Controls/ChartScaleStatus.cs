using System;

namespace C3D.DataViewer.Controls
{
    internal class ChartScaleStatus
    {
        private Single[] _mins;
        private Single[] _maxs;
        private Int32[] _scales;

        internal Single[] Mins
        {
            get { return this._mins; }
            set { this._mins = value; }
        }

        internal Single[] Maxs
        {
            get { return this._maxs; }
            set { this._maxs = value; }
        }

        internal Int32[] Scales
        {
            get { return this._scales; }
            set { this._scales = value; }
        }

        internal ChartScaleStatus(Single minX, Single maxX, Single minY, Single maxY)
        {
            this._mins = new Single[2];
            this._maxs = new Single[2];
            this._scales = new Int32[2];

            this._mins[0] = minX;
            this._mins[1] = minY;

            this._maxs[0] = maxX;
            this._maxs[1] = maxY;
        }
    }
}