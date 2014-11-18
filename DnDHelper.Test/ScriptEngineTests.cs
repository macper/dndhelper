using System;
using System.IO;
using DnDHelper.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DnDHelper.Test
{
    [TestClass]
    public class ScriptEngineTests : BaseTest
    {
        [TestMethod]
        public void ScriptTest()
        {
            var scriptContent = GetScriptContent("raceBoost.py");
            var python = ServiceContainer.GetInstance<IPythonEngine>();
            var repoSet = ServiceContainer.GetInstance<RepositorySet>();
            var scriptRepo = repoSet.Get<Script>();

            scriptRepo.Elements.Add( new Script( ScriptContext.Custom )
            {
                Content = scriptContent,
                Name = "RaceBoost"
            } );

            var testCharacter = new Character();
            python.GetMethod<Action<Character>>( ScriptContext.Custom, "RaceBoost")(testCharacter);
            Assert.IsTrue(testCharacter.OriginalStats.Strength == 12);
        }

        public static string GetScriptContent(string scriptName)
        {
            var d1 = AppDomain.CurrentDomain.BaseDirectory;
            return File.ReadAllText(Path.Combine(d1, "SampleScripts", scriptName));
        }
    }
}
