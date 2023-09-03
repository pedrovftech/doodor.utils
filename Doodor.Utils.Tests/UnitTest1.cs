using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace Doodor.Utils.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {            
            var stringTeste = "";
            //string text = "Controle de PS2/PC PRO (com gamepad)%";
            string text = "Vendo jogo de lençol do Ben 10, ideal para crianças de até 12 anos.\n\nConteúdo da embalagem:\n1 Lençol de cima 2,25m x 1,50m\n1 Lençol de baixo com elástico 1,88m x 88cm x 30cm alt.\n1 Fronha 70cm x 50cm\n\nComposição:\nTecidos: 80% Algodão / 20% Poliéster";
            string pat = @"^[a-zA-Z0-9 à-úÀ-Ú-_'.!?,:;\n/()%]*$";

            Regex r = new Regex(pat);
            Match m = r.Match(text);

            Assert.IsTrue(m.Success);        
        }        
    }
}
