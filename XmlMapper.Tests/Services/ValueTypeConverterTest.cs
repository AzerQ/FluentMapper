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
    
    [TestMethod]
    [DataRow("Red", Color.Red)]
    [DataRow("white", Color.White)]
    [DataRow("BLACK", Color.Black)]
    [DataRow("GreeN", Color.Green)]
    public void TestConvert_Enum_FromString(string inputStr, Color exceptedValue)
    {
        Color convertedValue = (Color)_valueTypeConverter.ConvertToDestinationType(inputStr, typeof(Color));
        Assert.AreEqual(convertedValue, exceptedValue);
    }

    private static IEnumerable<object[]> DateTimeConvertTestData =>
    [
        ["16.02.2023", new DateTime(2023, 2, 16)],
        ["02-03-2024", new DateTime(2024, 3, 2)],
        ["02-03-2024 18:05:53", new DateTime(2024, 3, 2, 18, 5, 53)],
        ["2024-10-31T17:32:31.0Z", new DateTime(2024, 10, 31, 20,32,31)]
    ];
    
    [TestMethod]
    [DynamicData(nameof(DateTimeConvertTestData), DynamicDataSourceType.Property)]
    public void TestConvert_DateTime_FromString(string inputStr, DateTime exceptedValue)
    {
        DateTime convertedValue = (DateTime)_valueTypeConverter.ConvertToDestinationType(inputStr, typeof(DateTime));
        Assert.AreEqual(convertedValue, exceptedValue);
    }
    
    
    public enum Color
    {
        Red,
        White,
        Black,
        Green
    }
}
