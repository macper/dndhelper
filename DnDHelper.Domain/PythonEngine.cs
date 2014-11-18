using System;
using System.Collections.Generic;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Linq;

namespace DnDHelper.Domain
{
    public class PythonEngine : IPythonEngine
    {
        public const string MethodName = "Calculate";
        private readonly List<CompiledScript> _compiledScripts = new List<CompiledScript>();
        private readonly ScriptScope _scope;
        private readonly ScriptEngine _pythonEngine;
        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>("loggerName", "PythonEngine"));

        public PythonEngine()
        {
            _pythonEngine = Python.CreateEngine();
            _scope = _pythonEngine.CreateScope();
        }

        private void RegisterInternal(string name, string content)
        {
            Logger.Debug("Rejestracja skryptu: " + name);
            Logger.Debug("Zawartość skryptu: " + content);

            var source = _pythonEngine.CreateScriptSourceFromString(content);
            source.Execute(_scope);
        }

        void IPythonEngine.ReloadScript( Script script )
        {
            var s = _compiledScripts.FirstOrDefault( f => f.Script == script );
            if( s != null )
            {
                _compiledScripts.Remove( s );
                Logger.Debug( "Skrypt usunięty (do przeładowania): " + script.Name );
            }
        }

        T IPythonEngine.GetMethod<T>( ScriptContext scriptContext, string name )
        {
            var compiled = _compiledScripts.FirstOrDefault( f => f.Script.ScriptContext == scriptContext && f.Script.Name == name );
            if( compiled != null )
            {
                return (T)compiled.CompiledMethod;
            }
            var script = ServiceContainer.GetInstance<RepositorySet>().Get<Script>().Elements.FirstOrDefault( f => f.ScriptContext == scriptContext && f.Name == name );
            if( script == null )
                throw new ApplicationException( "Script not found: " + name );

            RegisterInternal( name, script.Content );

            var action = _scope.GetVariable<T>( MethodName );
            _compiledScripts.Add( new CompiledScript
            {
                Script = script,
                CompiledMethod = action
            } );

            Logger.Info( "Skrypt zarejestrowany: " + name );
            return action;
        }

    }

    internal class CompiledScript
    {
        public object CompiledMethod { get; set; }

        public Script Script { get; set; }
    }
}