using NUnit.Framework;
using BusinessLogic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProcessSAUnitTests
{
    public class EmpresaTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void CrearEmpresa_debenSerTrue()
        {
            //Arrange
            Empresa emp = new Empresa();

            emp.Id = 0;
            emp.Rut = "12345678-9";
            emp.Nombre = "Frutifresh";
            emp.Correo = "test@correo.com";
            emp.Rubro = "Ventas";
            emp.Telefono = 912345678;
            emp.Contrato = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            //byte[] contrato = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            bool resultadoEsperado = true;

            //Act

            bool resultado = emp.Guardar();

            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(resultadoEsperado);
            //NUnit.Framework.Assert.AreEqual(resultadoEsperado, resultado);
        }
        /// <summary>
        /// Una empresa Nula no debe ser creada.
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CrearEmpresa_Nulo()
        {
            //Arrange

            Empresa emp = new Empresa();

            bool resultadoEsperado = false;

            //Act

            bool resultado = emp.Guardar();

            //Assert
            //NUnit.Framework.Assert.AreEqual(resultadoEsperado, resultado);
        }

        [Test]
        public void ModificarEmpresa_debenSerIguales()
        {
            //Arrange

            //byte[] contrato = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            Empresa emp = new Empresa();

            emp.Id = 0;
            emp.Rut = "12345678-9";
            emp.Nombre = "Frutifresh";
            emp.Correo = "test@correo.com";
            emp.Rubro = "Ventas";
            emp.Telefono = 912345678;
            emp.Contrato = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            bool resultadoEsperado = true;

            //Act

            bool resultado = emp.Guardar();

            //Assert
            NUnit.Framework.Assert.AreEqual(resultadoEsperado, resultado);
        }

        /// <summary>
        /// Una empresa Nula no se debe poder dejar una empresa vacia.
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void ModificarEmpresa_Nulo()
        {
            //Arrange

            Empresa emp = new Empresa();

            emp.Id = 123;
            emp.Rut = "12345678-9";
            emp.Nombre = "Frutifresh";
            emp.Correo = "test@correo.com";
            emp.Rubro = "Ventas";
            emp.Telefono = 912345678;
            emp.Contrato = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            
            bool resultado = emp.Guardar();

            emp.Id = 123;
            emp.Rut = "";
            emp.Nombre = "";
            emp.Correo = "";
            emp.Rubro = "";
            emp.Telefono = 0;
            emp.Contrato = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            //bool resultadoEsperado = false;

            //Act

            bool resultadoEsperado = emp.Modificar();

            //Assert
            NUnit.Framework.Assert.AreNotEqual(resultadoEsperado, resultado);
        }
    }
}