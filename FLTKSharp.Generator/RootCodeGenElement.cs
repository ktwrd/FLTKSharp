using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FLTKSharp.Generator
{
    [XmlRoot("Root")]
    public class RootCodeGenElement
    {
        [XmlElement("Template")]
        public List<CodeGenTemplateElement> Templates { get; set; } = [];
    }

    public class CodeGenTemplateElement
    {
        [Required]
        [XmlAttribute("Name")]
        public string Name { get; set; } = "";
        [XmlText]
        public string Text { get; set; } = "";
    }

    public class CodeGenClassElement
    {
        [Required]
        [XmlAttribute("Name")]
        public string Name { get; set; } = "";
        [Required]
        [XmlAttribute("Prefix")]
        public string Prefix { get; set; } = "";

        [XmlElement("Extends")]
        public List<CodeGenClassExtendsElement> Extends { get; set; } = [];

        [XmlElement("Template")]
        public List<CodeGenInsertTemplateElement> Templates { get; set; } = [];
    }
    public class CodeGenClassExtendsElement
    {
        [Required]
        [XmlAttribute("Name")]
        public string Name { get; set; } = "";
    }

    public class CodeGenInsertTemplateElement
    {
        [Required]
        [XmlAttribute("ResourceName")]
        public string ResourceName { get; set; } = "";
    }
}
