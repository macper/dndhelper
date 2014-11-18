using System;

namespace DnDHelper.Domain
{
    public interface IPythonEngine
    {
        void ReloadScript(Script script);
        T GetMethod<T>( ScriptContext scriptContext, string name);
    }
}