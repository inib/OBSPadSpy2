using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLROBS;

namespace OBSPadSpy2
{
    class Plugin : AbstractPlugin
    {
        public Plugin()
        {
            // Setup the default properties
            Name = "PadSpy 2";
            Description = "Draws Gamepad in your Scene";
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        public override bool LoadPlugin()
        {
            API.Instance.AddImageSourceFactory(new Factory.ImageSourceFactory());
            return true;
        }

        public override void OnStartStream()
        {
            base.OnStartStream();
        }

        public override void OnStopStream()
        {
            base.OnStopStream();
        }

        public override void UnloadPlugin()
        {
            base.UnloadPlugin();
        }

        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(',') ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");

            dllName = dllName.Replace(".", "_");

            if (dllName.EndsWith("_resources")) return null;

            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());

            byte[] bytes = (byte[])rm.GetObject(dllName);

            return System.Reflection.Assembly.Load(bytes);
        }

    }
}
