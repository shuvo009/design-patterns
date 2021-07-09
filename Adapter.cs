using Xunit;

namespace design_patterns
{
    public class Adapter
    {
        [Fact]
        public void Test()
        {
            var xmlData = new XmlData();
            IAdapter jsonAdapter = new XmlToJsonAdapter(xmlData);
            var json = jsonAdapter.GetData();
            Assert.Equal("{\"data\"=\"Test Data\"}", json);
        }
    }

    public interface IAdapter
    {
        string GetData();
    }

    public class XmlData
    {
        public string GetData()
        {
            return "<xml>Test Data</xml>";
        }
    }

    public class XmlToJsonAdapter : IAdapter
    {
        private readonly XmlData _xmlData;

        public XmlToJsonAdapter(XmlData xmlData)
        {
            _xmlData = xmlData;
        }

        public string GetData()
        {
            var xml = _xmlData.GetData();
            var data = xml.Replace("<xml>", "").Replace("</xml>", "");
            var jsonData = $"{{\"data\"=\"{data}\"}}";
            return jsonData;
        }
    }
}