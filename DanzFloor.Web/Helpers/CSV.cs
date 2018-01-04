using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace DanzFloor.Web.Helpers
{
    public static class CSV
    {
        public static void CreateCSVFromGenericList<T>(List<T> list, Stream stream)
        {

            //get type from 0th member
            Type t = list[0].GetType();
            string newLine = Environment.NewLine;

            using (var sw = new StreamWriter(stream))
            {
                //make a new instance of the class name we figured out to get its props
                object o = Activator.CreateInstance(t);
                //gets all properties
                PropertyInfo[] props = o.GetType().GetProperties().Where(p => !p.PropertyType.AssemblyQualifiedName.Contains("System.Collections.Generic")).ToArray();

                //foreach of the properties in class above, write out properties
                //this is the header row
                sw.Write(string.Join(";", props.Select(d => d.Name).ToArray()) + newLine);

                //this acts as datarow
                foreach (T item in list)
                {
                    //this acts as datacolumn
                    var row = string.Join(";", props.Select(d => item.GetType()
                                                                    .GetProperty(d.Name)
                                                                    .GetValue(item, null)??"")
                                                            .ToArray());
                    byte[] bytes = Encoding.Default.GetBytes(row);
                    string rowEncoded = Encoding.ASCII.GetString(bytes);
                    sw.Write(rowEncoded + newLine);

                }
            }
        }

    }
}