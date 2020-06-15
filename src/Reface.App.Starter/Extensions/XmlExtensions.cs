using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Reface.AppStarter
{
    public static class XmlExtensions
    {
        public static string ToXml<T>(this T value)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, value);
                byte[] buffer = new byte[memoryStream.Length];
                memoryStream.Position = 0;
                memoryStream.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            }
        }

        public static T ToObjectAsXml<T>(this string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(xml);
                memoryStream.Write(buffer, 0, buffer.Length);
                memoryStream.Position = 0;
                return (T)xmlSerializer.Deserialize(memoryStream);
            }
        }
    }
}
