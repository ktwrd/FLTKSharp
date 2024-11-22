using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FLTKSharp.Generator
{
    [XmlRoot("Bindings")]
    public class RootBindingElement
    {
        [XmlElement("Group")]
        public List<BindingGroupElement> Groups { get; set; } = [];
    }

    public class BindingGroupElement
    {
        [XmlAttribute("Name")]
        public string Name { get; set; } = "";

        [XmlElement("Function")]
        public List<BindingFunctionElement> Functions { get; set; } = [];

        [XmlElement("InheritFrom", IsNullable = true)]
        public List<BindingGroupInheritFromElement>? InheritFrom { get; set; }
    }
    public class BindingGroupInheritFromElement
    {
        [Required]
        [XmlAttribute("Name")]
        public string Name { get; set; } = "";
    }

    public class InheritFromElement
    {
        [Required]
        [XmlAttribute("cref")]
        public string CodeReference { get; set; }
        [XmlAttribute("path")]
        public string? Path { get; set; } = "";

        public string? ToXmlString()
        {
            if (string.IsNullOrEmpty(CodeReference))
                return null;
            if (string.IsNullOrEmpty(Path))
            {
                return $"<inheritdoc cref=\"{CodeReference}\" />";
            }
            else
            {
                return $"<inheritdoc cref=\"{CodeReference}\" path=\"{Path}\" />";
            }
        }
    }

    public class BindingFunctionElement
    {
        [XmlAttribute("Name")] public string Name { get; set; } = "";
        [XmlElement] public string? Summary { get; set; }

        [XmlElement("Parameter")]
        public List<BindingFunctionParameterElement> Parameters { get; set; } = [];

        [XmlElement("Returns")]
        public BindingFunctionReturnsElement? Returns { get; set; }

        [XmlAttribute("InheritBlacklist")]
        [DefaultValue(false)]
        public bool InheritBlacklist { get; set; } = false;
    }
    public class BindingFunctionParameterElement
    {
        [XmlAttribute("Name")] public string Name { get; set; } = "";
        [XmlAttribute("Type")] public string Type { get; set; } = "";
        [XmlAttribute("Summary")] public string? Summary { get; set; }
        [XmlElement("InheritFrom")] public InheritFromElement? InheritFrom { get; set; }
    }
    public class BindingFunctionReturnsElement
    {
        [XmlAttribute("Type")] public string Type { get; set; } = "";

        [XmlAttribute("Summary")] public string? Summary { get; set; }

        [XmlElement("InheritFrom")] public InheritFromElement? InheritFrom { get; set; }
    }
}
