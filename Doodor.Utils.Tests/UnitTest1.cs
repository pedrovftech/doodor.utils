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
            string text = "Vendo jogo de len�ol do Ben 10, ideal para crian�as de at� 12 anos.\n\nConte�do da embalagem:\n1 Len�ol de cima 2,25m x 1,50m\n1 Len�ol de baixo com el�stico 1,88m x 88cm x 30cm alt.\n1 Fronha 70cm x 50cm\n\nComposi��o:\nTecidos: 80% Algod�o / 20% Poli�ster";
            string pat = @"^[a-zA-Z0-9 �-��-�-_'.!?,:;\n/()%]*$";

            Regex r = new Regex(pat);
            Match m = r.Match(text);

            Assert.IsTrue(m.Success);        
        }        
    }
}
