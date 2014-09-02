using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
namespace TowerDefence
{


   public class ModuleHandler 
    {
       Dictionary<string,IModule> Modules = new Dictionary<string,IModule>();

        /// <summary>
        /// well adding stuff hlp alot:3 
        /// </summary>
        /// <param name="name">yes this is actually the name</param>
        /// <param name="module">the module yuo wanna add</param>
        /// 

       public void Add(string name,IModule module)
       {
           Modules.Add(name, module);
       }

       public bool Drop(string name) 
       { 
           IModule m;

           if (Modules.TryGetValue(name, out m)) 
           {
                m.Drop();
                Modules.Remove(name);
                return true;
           }
           return false;
       }
       public IModule Get(string name) 
       {
           return Modules[name];
       }


       public void Update(GameTime gametime)
       {
           
           foreach (IModule m in Modules.Values) 
           {
               m.Update(gametime);
           } 

       }

       public void Draw(SpriteBatch spriteBatch) 
       {
           foreach (IModule m in Modules.Values)
           {
               m.Draw(spriteBatch);
           } 
       }
    }

   public interface IModule 
    {
        void Load();
        void Update(GameTime gametime);
        void Draw(SpriteBatch spriteBatch);
        void Drop();
    }
}
