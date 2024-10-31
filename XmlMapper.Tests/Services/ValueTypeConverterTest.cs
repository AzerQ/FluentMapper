using JetBrains.Annotations;
using XmlMapper.Core.Services;

namespace XmlMapper.Tests.Services;

[TestClass]
[TestSubject(typeof(ValueTypeConverter))]
public class ValueTypeConverterTest
{
    private readonly ValueTypeConverter _valueTypeConverter = new();

    [TestMethod]
    [DataRow("true", true)]
    [DataRow("false", false)]
    [DataRow("TRUE", true)]
    [DataRow("FALSE", false)]
    public void TestConvert_Boolean_FromString(string inputStr, bool exceptedValue)
    {
       bool convertedValue = (bool)_valueTypeConverter.ConvertToDestinationType(inputStr, typeof(bool));
       Assert.AreEqual(convertedValue, exceptedValue);
    }
    
    [TestMethod]
    [DataRow("128", 128)]
    [DataRow("15", 15)]
    [DataRow("-3000", -3000)]
    [DataRow("128000000", 128_000_000)]
    public void TestConvert_Int_FromString(string inputStr, int exceptedValue)
    {
        int convertedValue = (int)_valueTypeConverter.ConvertToDestinationType(inputStr, typeof(int));
        Assert.AreEqual(convertedValue, exceptedValue);
    }
    
    [TestMethod]
    [DataRow("128,934", 128.934)]
    [DataRow("15,9", 15.9)]
    [DataRow("-3000,1278", -3000.1278)]
    [DataRow("0,1413154151446146", 0.1413154151446146)]
    public void TestConvert_Double_FromString(string inputStr, double exceptedValue)
    {
        double convertedValue = (double)_valueTypeConverter.ConvertToDestinationType(inputStr, typeof(double));
        Assert.AreEqual(convertedValue, exceptedValue);
    }
    
}