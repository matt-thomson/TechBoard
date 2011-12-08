namespace SoundBoard
{
    [BlockPropertyEditor(typeof(RangeBlockPropertyEditor))]
    public class RangeBlockPropertyAttribute : BlockPropertyAttribute
    {
        #region Public properties
        public double Minimum { get; private set; }
        public double Maximum { get; private set; }
        public double Step { get; private set; }
        #endregion

        #region Constructor
        public RangeBlockPropertyAttribute(double xiMinimum,
                                           double xiMaximum,
                                           double xiStep)
        {
            Minimum = xiMinimum;
            Maximum = xiMaximum;
            Step = xiStep;
        }
        #endregion

        #region Public methods
        public override object FromString(string xiValue)
        {
            return double.Parse(xiValue);
        }
        #endregion
    }
}
