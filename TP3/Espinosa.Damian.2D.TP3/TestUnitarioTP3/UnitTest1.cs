using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntidadesAbstractas;
using EntidadesInstanciables;
using Excepciones;

namespace TestUnitarioTP3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AtributosNoNulos()
        {
            Alumno a1 = new Alumno();

            Assert.IsNotNull(a1._claseQueToma);
            Assert.IsNotNull(a1._estadoCuenta);

        }

        public void DniNegativo()
        {
            try
            {
                string menosDni = "-29324565";

                Profesor p1 = new Profesor(1, "Carlos", "Lopez", menosDni, Persona.ENacionalidad.Argentino);
            }
            catch
            {
                throw new DniInvalidoException("El dni no puede ser Negativo");
            }
        }

    }
}

