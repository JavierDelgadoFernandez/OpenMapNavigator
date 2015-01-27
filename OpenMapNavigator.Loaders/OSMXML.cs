using OpenMapNavigator.Elements;
using OpenMapNavigator.Elements.Attrbitutes;
using OpenMapNavigator.Elements.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace OpenMapNavigator.Loaders
{
    public static class OSMXML
    {
        #region · Load ·
        public static Map Load(Stream stream)
        {
            var result = new Map();

            XmlReader reader;

            var t0 = DateTime.Now;
            Debug.WriteLine("Has started a new load of OSM XML");

            stream.Position = 0;
            reader = XmlReader.Create(stream);
            while (reader.ReadToFollowing("node"))
            {
                Node node = ParseNode(result, reader.ReadOuterXml());
                result.Add(node);
            }
            reader.Dispose();

            Debug.WriteLine("The load of nodes has spend: {0}", DateTime.Now - t0);

            stream.Position = 0;
            reader = XmlReader.Create(stream);
            while (reader.ReadToFollowing("way"))
            {
                Way way = ParseWay(result, reader.ReadOuterXml());
                result.Add(way);
            }
            reader.Dispose();

            Debug.WriteLine("The load of ways has spend: {0}", DateTime.Now - t0);

            stream.Position = 0;
            reader = XmlReader.Create(stream);
            while (reader.ReadToFollowing("relation"))
            {
                Relation relation = ParseRelation(result, reader.ReadOuterXml());
                result.Add(relation);
            }
            reader.Dispose();

            Debug.WriteLine("The load of OSM XML has spend: {0}", DateTime.Now - t0);

            return result;
        }

        /*public static Map Load(string filename)
        {
            return Load(new FileStream(filename, FileMode.Open));
        }*/
        #endregion

        #region · Parsers ·
        private static Node ParseNode(Map map, string outerxml)
        {
            XElement xml = XElement.Parse(outerxml);

            long id = ParseID(xml);
            ICollection<IAttribute> attributes = ParseAttributes(xml);
            Coordinate coordinate = ParseCoordinate(xml);

            return new Node(id, attributes, coordinate);
        }

        private static Coordinate ParseCoordinate(XElement xml)
        {
            XAttribute latitudex = xml.Attribute("lat");
            XAttribute longitudex = xml.Attribute("lon");

            float latitude = float.Parse(latitudex.Value, CultureInfo.InvariantCulture);
            float longitude = float.Parse(longitudex.Value, CultureInfo.InvariantCulture);

            return new Coordinate(latitude, longitude);
        }

        private static Way ParseWay(Map map, string outerxml)
        {
            XElement xml = XElement.Parse(outerxml);

            long id = ParseID(xml);
            ICollection<IAttribute> attributes = ParseAttributes(xml);
            IList<Node> nodes = ParseNds(map, xml);

            return new Way(id, attributes, nodes);
        }

        private static IList<Node> ParseNds(Map map, XElement xml)
        {
            // TODO: May not work properly, aggregate is not a list...
            return xml.Elements("nd").Aggregate<XElement, IList<Node>>(new List<Node>(), (l, x) =>
                {
                    long nodeid = ParseNd(x);
                    l.Add(map.Nodes[nodeid]);
                    return l;
                });

        }
        private static long ParseNd(XElement xml)
        {
            return long.Parse(xml.Attribute("ref").Value);
        }

        private static Relation ParseRelation(Map map, string outerxml)
        {
            XElement xml = XElement.Parse(outerxml);

            long id = ParseID(xml);
            ICollection<IAttribute> attributes = ParseAttributes(xml);
            IList<MapElementBase> members = ParseMembers(map, xml);

            return new Relation(id, attributes, members);
        }

        private static IList<MapElementBase> ParseMembers(Map map, XElement xml)
        {
            return xml.Elements("member").Aggregate<XElement, IList<MapElementBase>>(new List<MapElementBase>(), (l, x) =>
                {
                    var member = ParseMember(map, x);
                    if (member == null)
                        Debug.WriteLine("Key not found on OSM XML");
                    else
                        l.Add(member);
                    
                    return l;
                });
        }

        private static MapElementBase ParseMember(Map map, XElement xml)
        {
            XAttribute typex = xml.Attribute("type");
            XAttribute refx = xml.Attribute("ref");

            long idref = long.Parse(refx.Value);

            switch(typex.Value)
            {
                case "node":
                    if (!map.Nodes.ContainsKey(idref))
                        return null;
                    return map.Nodes[idref];
                case "way":
                    if (!map.Ways.ContainsKey(idref))
                        return null;
                    return map.Ways[idref];
                case "relation":
                    if (!map.Relations.ContainsKey(idref))
                        return null;
                    return map.Relations[idref];
                default:
                    throw new ArgumentException("There is a type in OSM XML with errors");

            }
        }

        private static long ParseID(XElement xml)
        {
            return long.Parse(xml.Attribute("id").Value);
        }

        private static ICollection<IAttribute> ParseAttributes(XElement xml)
        {
            return xml.Elements("tag").Select(x => ParseAttribute(x)).ToList();
            /*return xml.Elements("tag").Aggregate<XElement, ICollection<IAttribute>>(new List<IAttribute>(), (c, a) =>
                {
                    c.Add(ParseAttribute(a));
                    return c;
                });*/
        }

        private static IAttribute ParseAttribute(XElement xml)
        {
            XAttribute key = xml.Attribute("k");
            XAttribute value = xml.Attribute("v");

            IAttribute result = new GenericAttribute(key.Value, value.Value);
            return result;
        }
        #endregion
    }
}