using System;
using System.Collections.Generic;
using System.Text;

namespace Composite
{
    class Program
    {
        private abstract class XmlComponent
        {
            public string Name { get; }

            protected XmlComponent(string name)
            {
                Name = name;
            }
            public abstract void Add(XmlComponent xmlComponent);
            public abstract void Remove(XmlComponent xmlComponent);
        }

        private class XmlElement : XmlComponent
        {
            private readonly List<XmlComponent> _xmlComponents = new List<XmlComponent>();
            public XmlElement(string name): base(name) { }

            public override void Add(XmlComponent xmlComponent)
            {
                _xmlComponents.Add(xmlComponent);
            }
            public override void Remove(XmlComponent xmlComponent)
            {
                _xmlComponents.Remove(xmlComponent);
            }

            public override string ToString()
            {
                var xmlBlock = new StringBuilder();
                xmlBlock.Append($"<{Name}>");
                foreach (var xmlComponent in _xmlComponents)
                {
                    xmlBlock.Append(xmlComponent);
                }
                xmlBlock.Append($"</{Name}>");
                return xmlBlock.ToString();
            }
        }

        private class XmlSelfClosingElement : XmlComponent
        {
            public string Value { get; }

            public XmlSelfClosingElement(string name, string value) : base(name)
            {
                Value = value;
            }

            public override void Add(XmlComponent xmlComponent) { }
            public override void Remove(XmlComponent xmlComponent) { }

            public override string ToString()
            {
                return $"<{Name} value={Value}/>";
            }
        }

        static void Main(string[] args)
        {
            var xmlElement = new XmlElement("root");
            var configuration = new XmlElement("configuration");
            var rootPath = new XmlSelfClosingElement("imagesRootPath", "images");
            var storage = new XmlSelfClosingElement("storage", "azure");
            
            configuration.Add(rootPath);
            configuration.Add(storage);

            xmlElement.Add(configuration);
            Console.WriteLine(xmlElement);

            Console.ReadLine();
        }
    }
}
