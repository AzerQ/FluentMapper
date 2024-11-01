using JetBrains.Annotations;
using XmlMapper.Core.Services;

namespace XmlMapper.Tests;

[TestClass]
public class ValueTypeConverterTest
{
    private readonly ValueTypeConverter _valueTypeConverter = new();

    private T GetConvertedValue<T>(string inputStr) => (T)_valueTypeConverter.ConvertToDestinationType(inputStr, typeof(T));

    [TestMethod]
    [DataRow("true", true)]
    [DataRow("false", false)]
    [DataRow("TRUE", true)]
    [DataRow("FALSE", false)]
    public void TestConvert_Boolean_FromString(string inputStr, bool exceptedValue)
    {
        var convertedValue = GetConvertedValue<bool>(inputStr);
        Assert.AreEqual(convertedValue, exceptedValue);
    }

    [TestMethod]
    [DataRow("128", 128)]
    [DataRow("15", 15)]
    [DataRow("-3000", -3000)]
    [DataRow("128000000", 128_000_000)]
    public void TestConvert_Int_FromString(string inputStr, int exceptedValue)
    {
        var convertedValue = GetConvertedValue<int>(inputStr);
        Assert.AreEqual(convertedValue, exceptedValue);
    }

    [TestMethod]
    [DataRow("128,934", 128.934)]
    [DataRow("15,9", 15.9)]
    [DataRow("-3000,1278", -3000.1278)]
    [DataRow("0,1413154151446146", 0.1413154151446146)]
    public void TestConvert_Double_FromString(string inputStr, double exceptedValue)
    {
        var convertedValue = GetConvertedValue<double>(inputStr);
        Assert.AreEqual(convertedValue, exceptedValue);
    }

    [TestMethod]
    [DataRow("Red", Color.Red)]
    [DataRow("white", Color.White)]
    [DataRow("BLACK", Color.Black)]
    [DataRow("GreeN", Color.Green)]
    public void TestConvert_Enum_FromString(string inputStr, Color exceptedValue)
    {
        var convertedValue = GetConvertedValue<Color>(inputStr);
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
        var convertedValue = GetConvertedValue<DateTime>(inputStr);
        Assert.AreEqual(convertedValue, exceptedValue);
    }

    private static IEnumerable<object[]> GuidConvertTestData =>
    [
            ["04606e81-a4ab-4378-8cc7-09edbb5e902d", new Guid("04606e81-a4ab-4378-8cc7-09edbb5e902d")],
            ["{43472548-441a-40ec-bf8a-29566395341c}", new Guid("43472548-441a-40ec-bf8a-29566395341c")],

    ];

    [TestMethod]
    [DynamicData(nameof(GuidConvertTestData), DynamicDataSourceType.Property)]
    public void TestConvert_Guid_FromString(string inputStrnig, Guid exceptedValue)
    {
        var convertedValue = GetConvertedValue<Guid>(inputStrnig);
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
