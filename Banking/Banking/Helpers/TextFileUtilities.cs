using Banking.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;

namespace Banking.Helpers
{
    class TextFileUtilities
    {
        public string[] TextFileSerialize<T>(IEnumerable<T> dataToSerialize)
        {
            string[] content;
            bool isTypePrimitive = typeof(T).IsPrimitive;
            bool isTypeString = typeof(T).Equals(typeof(string));
            if (!isTypePrimitive && !isTypeString)
            {
                content = new string[dataToSerialize.Count()];
                for (int i = 0; i < content.Length; i++)
                {
                    var props = dataToSerialize.ElementAt(i).GetType().GetProperties();
                    for (int x = 0; x < props.Length; x++)
                    {
                        Type test = props[x].PropertyType;
                        if (test.IsGenericType || test.IsArray)
                            content[i] += ConvertCollectionContentToString((IEnumerable)props[x].GetValue(dataToSerialize.ElementAt(i))
                                , props[x].Name);
                        else
                            content[i] += $"{props[x].Name}:{props[x].GetValue(dataToSerialize.ElementAt(i)).ToString()}";
                        content[i] += "|";
                    }
                }
            }
            else
                content = dataToSerialize.Select(d => d.ToString()).ToArray();
            return content;
        }

        string ConvertCollectionContentToString(IEnumerable collection, string propertyName)
        {
            string content = "";

            bool isPrimitiveType = (collection.GetType().GetGenericArguments().Length != 0)
                ? collection.GetType().GetGenericArguments().Single().IsPrimitive
                : collection.GetType().GetElementType().IsPrimitive;

            bool isStringType = (collection.GetType().GetGenericArguments().Length != 0)
                ? collection.GetType().GetGenericArguments().Single().Equals(typeof(string))
                : collection.GetType().GetElementType().Equals(typeof(string));


            if (!isPrimitiveType && !isStringType)
                foreach (var item in collection)
                {
                    var props = item.GetType().GetProperties();
                    for (int i = 0; i < props.Length; i++)
                    {
                        var type = props[i].GetValue(item).GetType();
                        if (typeof(ICollection).IsAssignableFrom(type))
                            content += ConvertCollectionContentToString((IEnumerable)props[i].GetValue(item), props[i].Name);
                        else
                            content += $"{props[i].Name}:{props[i].GetValue(item)}";
                        if (i != props.Length - 1)
                            content += ",";
                    }
                }
            else
            {
                foreach (var item in collection)
                {
                    content += $"{item},";
                }
                content = content.Remove(content.Length - 1);
            }
            string result = $"{propertyName}:[{content}]";
            return result;
        }

        public void WriteToTextFile(string directory, string fileName, IEnumerable<string> content)
        {
            Directory.CreateDirectory(directory);
            string fullPath = $"{directory}/{fileName}.txt";
            File.AppendAllLines(fullPath, content);
        }

        public List<TResult> TextFileDeserialize<TResult>() where TResult : new()
        {
            List<TResult> deserializedCollection = new List<TResult>();
            string fullPath = $"{Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\netcoreapp2.0", "")}\\TextFiles\\accounts.txt";
            List<string> fileResult = File.ReadAllLines(fullPath).ToList();
            for (int i = 0; i < fileResult.Count; i++)
            {
                TResult deserializedObject = new TResult();
                int currentIndex = 0;
                string currentLine = fileResult[i];
                bool isLoopEnd = false;
                while (!isLoopEnd)
                {
                    int stopIndex = currentLine.IndexOf('|', 0);
                    string tempObject = currentLine.Substring(0, stopIndex);
                    stopIndex = tempObject.IndexOfAny(new char[] { ':', '[' });
                    //if(tempObject[stopIndex] == '[')
                    string propName = tempObject.Substring(0, stopIndex);
                    string propValue = tempObject.Substring(stopIndex + 1);
                    DeserializeCollection(ref deserializedObject, propName, propValue);
                    PropertyInfo prop = deserializedObject.GetType().GetProperty(propName);
                    var convertedValue = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(propValue);
                    prop.SetValue(deserializedObject, convertedValue);
                    currentLine = currentLine.Remove(0, tempObject.Length + 1);
                    if (currentLine.Length == 0)
                        isLoopEnd = true;
                }
            }
            return deserializedCollection;
        }

        void DeserializeCollection<T>(ref T deserializedObject, string propName, string propValue)
        {
            propValue = propValue.Remove(0, 1);
            propValue = propValue.Remove(propValue.Length - 1, 1);
            PropertyInfo propInfo = deserializedObject.GetType().GetProperty(propName);
            var property = TypeDescriptor.CreateInstance(null, propInfo.PropertyType, null, null);
            var type = property.GetType();

            bool isPrimitiveType = (property.GetType().GetGenericArguments().Length != 0)
                ? property.GetType().GetGenericArguments().Single().IsPrimitive
                : property.GetType().GetElementType().IsPrimitive;

            bool isStringType = (property.GetType().GetGenericArguments().Length != 0)
                ? property.GetType().GetGenericArguments().Single().Equals(typeof(string))
                : property.GetType().GetElementType().Equals(typeof(string));

            if (!isPrimitiveType && !isStringType)
            {

            }
            else
            {
                List<string> splitCollectionSingleProps = propValue.Split(',').ToList();
                

            }
            //List<string> splitCollectionCollectionProps;
            //while (propName.Contains('['))
            //{
            //}
        }
    }
}
