using System.Collections;
using System.Xml.Linq;
using XmlMapper.Core.Services;

namespace XmlMapper.Tests;

[TestClass]
public class XpathScalarConverterTest
{
    private readonly XpathScalarConverter _xpathScalarConverter = new();

    private static IEnumerable<object[]> XElementsTestData =>
    [
        [new XElement("BookName", "World War"), "World War"],
        [new XElement("UserAge", 25), "25"],
        [new XElement("HasAccess", false), "false"],
        [new XElement("CreationDate", new DateTime(2023, 5, 22)), "2023-05-22T00:00:00"]
    ];

    [TestMethod]
    [DynamicData(nameof(XElementsTestData), DynamicDataSourceType.Property)]
    public void Test_GetValue_FromXElement(XElement xElement, string exceptedValue)
    {
        string selectedValue = _xpathScalarConverter.GetXpathResultValue(xElement);
        Assert.AreEqual(selectedValue, exceptedValue);
    }

    private static IEnumerable<object[]> XAttributesTestData =>
    [
        [new XAttribute("DisplayName", "Rob White"), "Rob White"],
        [new XAttribute("TryCount", 30), "30"],
        [new XAttribute("UserIsOnline", true), "true"],
        [new XAttribute("UpdateDate", new DateTime(2020, 2, 10)), "2020-02-10T00:00:00"],
    ];

    [TestMethod]
    [DynamicData(nameof(XAttributesTestData), DynamicDataSourceType.Property)]
    public void Test_GetValue_FromXAttribute(XAttribute attribute, string exceptedValue)
    {
        string selectedValue = _xpathScalarConverter.GetXpathResultValue(attribute);
        Assert.AreEqual(selectedValue, exceptedValue);
    }

    [TestMethod]
    [DataRow(127.5, "127,5")]
    [DataRow(0.512341512, "0,512341512")]
    [DataRow(123456789.904, "123456789,904")]
    [DataRow(-15567.13123, "-15567,13123")]
    public void Test_GetValue_FromDouble(double value, string exceptedValue)
    {
        string selectedValue = _xpathScalarConverter.GetXpathResultValue(value);
        Assert.AreEqual(selectedValue, exceptedValue);
    }

    [TestMethod]
    [DataRow(true, "True")]
    [DataRow(false, "False")]
    public void Test_GetValue_FromBoolean(bool value, string exceptedValue)
    {
        string selectedValue = _xpathScalarConverter.GetXpathResultValue(value);
        Assert.AreEqual(selectedValue, exceptedValue);
    }

    private static object[] MakeFirstElementList(object[] dataRow)
    {
      object[] result = [ new List<object> { dataRow.First() }, ..dataRow.Skip(1)];
      return result;
    }
    
    private static IEnumerable<object[]> GetCollectionTestData()
    {
        object[] xElementsFirstRow = MakeFirstElementList(XElementsTestData.First());
        object[] xAttributeFirstRow = MakeFirstElementList(XAttributesTestData.First());
        object[] doubleTestData = [new[] { 1.5d, 1.6d, 789.0d }, "1,5"];
        object[] boolTestDataTrue = [new[] { true, false }, "True"];
        object[] boolTestDataFalse = [new[] { false, true }, "False"];
        
        List<object[]> testData =
        [
            [..xElementsFirstRow],
            [..xAttributeFirstRow],
            doubleTestData,
            boolTestDataTrue,
            boolTestDataFalse
        ];

        return testData;
    }

    [TestMethod]
    [DynamicData(nameof(GetCollectionTestData), DynamicDataSourceType.Method)]
    public void Test_GetOnlyFirstValue_FromIEnumerable(IEnumerable collection, string exceptedValue)
    {
        string selectedValue = _xpathScalarConverter.GetXpathResultValue(collection);
        Assert.AreEqual(selectedValue, exceptedValue);
    }

    [TestMethod]
    public void Test_Incorrect_ObjectType_ThrowsException()
    {
        Assert.ThrowsException<XpathValueConvertException>
            (() => _xpathScalarConverter.GetXpathResultValue(DateTime.Now));
    }
}