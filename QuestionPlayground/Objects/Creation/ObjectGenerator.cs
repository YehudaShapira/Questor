using Bdi.Actions;
using Bdi.Concepts;
using Bdi.Concepts.Statements;
using Bdi.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bdi.Objects.Creations
{
    public static class ObjectGenerator
    {
        private static HashSet<string> names = new HashSet<string>();

        public static Person GeneratePerson(string name, BeliefSet beliefs, List<StaticStatement> goals,
            List<SingleAction> availableActions, List<string> familiarObjects)
        {
            CheckNameAvailability(name);

            return new Person(name, beliefs, goals, availableActions, familiarObjects);
        }

        public static Person GeneratePersonFromXml(string name, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                return (Person)serializer.Deserialize(fileStream);
            }
        }

        public static Place GeneratePlace(string name)
        {
            CheckNameAvailability(name);

            return new Place(name);
        }

        public static Thing GenerateThing(string name)
        {
            CheckNameAvailability(name);

            return new Thing(name);
        }

        private static void CheckNameAvailability(string name)
        {
            if (names.Contains(name))
            {
                throw new Exception("An object with the name " + name + "already exists");
            }

            names.Add(name);
        }
    }
}
