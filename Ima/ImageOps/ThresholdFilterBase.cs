namespace Ima.ImageOps
{
    public abstract class ThresholdFilterBase : FilterBase
    {
        private int _threshold;

        public ThresholdFilterBase(string name)
            : base(name)
        {
        }

        public virtual int Threshold
        {
            get
            {
                return this._threshold;
            }

            set
            {
                this._threshold = (value < this.Minimum)
                    ? this.Minimum
                    : (value > this.Maximum)
                    ? this.Maximum
                    : value;
            }
        }

        public int Minimum
        {
            get; protected set;
        }

        public int Maximum
        {
            get; protected set;
        }

        public string Property
        {
            get; protected set;
        }
    }
}
