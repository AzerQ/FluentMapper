namespace XmlMapper.Core.Services
{
    /// <summary>
    /// Defines a contract for converting XPath results to a scalar value.
    /// </summary>
    public interface IXpathScalarConverter
    {
        /// <summary>
        /// Retrieves the scalar value from the XPath result object.
        /// </summary>
        /// <param name="xpathResult">The XPath result object.</param>
        /// <returns>The scalar value extracted from the XPath result.</returns>
        string GetXpathResultValue(object xpathResult);
    }
}
