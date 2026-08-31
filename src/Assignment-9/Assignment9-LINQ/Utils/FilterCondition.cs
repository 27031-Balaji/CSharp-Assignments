namespace Assignment9.Utils
{
    /// <summary>
    /// Represents the supported filter operations that can be applied
    /// when building dynamic queries.
    /// </summary>
    public enum FilterCondition
    {
        /// <summary>
        /// Checks whether the property value contains the specified text.
        /// </summary>
        Contains,

        /// <summary>
        /// Checks whether the property value starts with the specified text.
        /// </summary>
        StartsWith,

        /// <summary>
        /// Checks whether the property value ends with the specified text.
        /// </summary>
        EndsWith,

        /// <summary>
        /// Checks whether the property value is greater than or equal to
        /// the specified value.
        /// </summary>
        GreaterThanOrEqualTo,

        /// <summary>
        /// Checks whether the property value is less than or equal to
        /// the specified value.
        /// </summary>
        LessThanOrEqualTo,
    }
}