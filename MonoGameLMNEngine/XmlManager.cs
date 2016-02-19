using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace LemonParticlesSystem
{
    class XmlManager<T>
    {
        public Type type;

        public T Load(string path)
        {
            T instance;

            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(type);
                instance = (T)xml.Deserialize(reader);
            }

            return instance;
        }

        public void Save(string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(obj.GetType());
                xml.Serialize(writer, obj);
            }
        }
    }
}
