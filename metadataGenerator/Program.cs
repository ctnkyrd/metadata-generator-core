﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace metadataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                createMetaData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Search the existing xml file
            //XmlNodeList getAllNodes(XmlDocument xmlDoc, string tagName)
            //{
            //    XmlNodeList list =  xmlDoc.GetElementsByTagName(tagName);
            //    return list;
            //}

            //create new xml file from scratch
            void createMetaData()
            {
                try
                {
                    XNamespace gmd = "http://www.isotc211.org/2005/gmd";
                    XDocument xdoc = new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement(gmd+"MD_Metadata",
                            new XAttribute(XNamespace.Xmlns+"gmd", gmd),
                            new XElement(gmd+"fileIdentifier",
                                new XElement(gmd+"CharacterString", "12346.xml")
                                )   
                            )
                        );
                    
                    //string nameOfTheFile = name;
                    xdoc.Save("GENERATEDXML\\deneme.xml");

                    //do stuff

                    //save the file to folder

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

    }
}