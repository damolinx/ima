using Ima.Utilites;

namespace Ima.ImageOps
{
    public abstract class ThresholdFilterBase : FilterBase
    {
        private int _threshold;

        protected ThresholdFilterBase(string name, bool inPlace = true)
            : base(name, inPlace)
        {
        }

        public virtual int Threshold
        {
            get
            {
                return _threshold;
            }

            set
            {
                _threshold = MathEx.Clamp(value, Minimum, Maximum);
            }
        }

        public int Maximum
        {
            get; protected set;
        }

        public int Minimum
        {
            get; protected set;
        }

        //TODO: Convert to DescriptionAttribute
        public string PropertyName
        {
            get; protected set;
        }
    }
}
