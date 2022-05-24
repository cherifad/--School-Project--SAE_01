using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Animaux
{
    public interface Crud<T>
    {
        void Create();
        void Read();
        void Update();
        void Delete();
        List<T> FindAll();
        List<T> FindBySelection(string criteres);
    }
}
