﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 
namespace rejseplanencore.Schemas
{
    using System.Xml.Serialization;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
     
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class MultiDepartureBoard
    {

        private Departure[] departureField;

        private string errorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Departure")]
        public Departure[] Departure
        {
            get { return this.departureField; }
            set { this.departureField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string error
        {
            get { return this.errorField; }
            set { this.errorField = value; }
        }
    }

}
