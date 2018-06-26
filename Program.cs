using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xbim.Ifc;
using Xbim.ModelGeometry.Scene;


namespace XbimWebim
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("XbimWebin yourFile.ifc");
                return;
            }
            string fileName = args[0];
            using (var model = IfcStore.Open(fileName))
            {
                var context = new Xbim3DModelContext(model);
                context.CreateContext();

                var wexBimFilename = Path.ChangeExtension(fileName, "wexBIM");
                using (var wexBiMfile = File.Create(wexBimFilename))
                {
                    using (var wexBimBinaryWriter = new BinaryWriter(wexBiMfile))
                    {
                        model.SaveAsWexBim(wexBimBinaryWriter);
                        wexBimBinaryWriter.Close();
                    }
                    wexBiMfile.Close();
                }
            }
        }
    }
}
