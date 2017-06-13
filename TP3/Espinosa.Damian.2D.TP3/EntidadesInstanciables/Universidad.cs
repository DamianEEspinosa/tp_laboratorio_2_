using Archivos;
using Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EntidadesInstanciables
{
    [Serializable]
    [XmlInclude(typeof(Alumno))]
    [XmlInclude(typeof(Profesor))]
    [XmlInclude(typeof(Jornada))]
    public class Universidad
    {
        #region ---------------ATRIBBUTOS--------------
        public List<Alumno> _alumnos;
        public List<Profesor> _profesores;
        public List<Jornada> _jornada;
        #endregion

        #region ---------------PROPIEDADES-------------
        public Jornada this[int i]
        {
            get { return this._jornada[i]; }
        }
        #endregion

        #region --------------CONSTRUCTORES------------
        public Universidad()
        {
            this._alumnos = new List<Alumno>();
            this._profesores = new List<Profesor>();
            this._jornada = new List<Jornada>();
        }

        #endregion

        #region -----------------METODOS---------------
        /// <summary>
        /// Guarda los datos del objeto en un archivo XML
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static bool Guardar(Universidad datos)
        {
            Xml<Universidad> export = new Xml<Universidad>();
            return export.Guardar("Universidad.xml", datos);
        }

        /// <summary>
        /// Devuelve un objeto Universidad con datos obtenidos de un archivo XML
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static Universidad Leer(Universidad datos)
        {
            Xml<Universidad> import = new Xml<Universidad>();
            Universidad aux;
            import.Leer("Universidad.xml", out aux);
            return aux;
        }
        #endregion

        #region ----------SOBRECARGA DE METODOS--------
        /// <summary>
        /// Devuelve una cadena con los datos del objeto
        /// </summary>
        /// <returns></returns>
        private static string MostrarDatos(Universidad gim)
        {
            StringBuilder cadena = new StringBuilder();
            foreach (Jornada t in gim._jornada)
            {
                cadena.Append(t);
            }
            return cadena.ToString();
        }

        /// <summary>
        /// Devuelve una cadena con los datos del objeto
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Universidad.MostrarDatos(this);
        }
        #endregion

        #region ---------SOBRECARGA DE OPERADORES------

        /// <summary>
        /// Devuelve true si el Alumno pertenece al Universidad
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool operator ==(Universidad g, Alumno a)
        {
            foreach (Alumno t in g._alumnos)
            {
                if (t == a)
                    return true;
            }
            return false;
        }

        public static bool operator !=(Universidad g, Alumno a)
        {
            return !(g == a);
        }

        /// <summary>
        /// Devuelve un Profesor disponible para dar la clase, caso contrario retorna una Excepcion
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static Profesor operator ==(Universidad g, Universidad.EClases clase)
        {
            foreach (Profesor t in g._profesores)
            {
                if (t == clase)
                {
                    if (!Object.Equals(t, null))
                        return t;
                }
            }
            throw new SinProfesorException();
        }

        /// <summary>
        /// Devuelve al primer Profesor encontrado que no da la clase indicada.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static Profesor operator !=(Universidad g, Universidad.EClases clase)
        {
            foreach (Profesor t in g._profesores)
            {
                if (t != clase)
                {
                    return t;
                }
            }
            return null;

        }

        /// <summary>
        /// Retorna True si el profesor pertenece al Universidad
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool operator ==(Universidad g, Profesor i)
        {
            foreach (Profesor t in g._profesores)
            {
                if (t == i)
                    return true;
            }
            return false;
        }

        public static bool operator !=(Universidad g, Profesor i)
        {
            return !(g == i);
        }

        /// <summary>
        /// Agrega un alumno a la universidad verificando antes que no exista.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Universidad operator +(Universidad g, Alumno a)
        {
            if (g != a)
            {
                g._alumnos.Add(a);
            }
            else
            {
                throw new Excepciones.AlumnoRepetidoException();
            }

            return g;
        }

        /// <summary>
        /// Agrega una jornada a la univeridad setando un profesor y sus respectivos Alumnos
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static Universidad operator +(Universidad g, Universidad.EClases clase)
        {
            Jornada auxJornada = new Jornada(clase, (g == clase));

            foreach (Alumno t in g._alumnos)
            {
                if (t == clase)
                {
                    auxJornada._alumno.Add(t);
                }

            }
            g._jornada.Add(auxJornada);

            return g;
        }

        /// <summary>
        /// Agrega un Profesor a la Universidad verificando antes que no exista.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Universidad operator +(Universidad g, Profesor i)
        {
            if (g != i)
            {
                g._profesores.Add(i);
            }
            return g;
        }

        #endregion

        #region ----------------ENUMERADOS-------------
        public enum EClases
        {
            Laboratorio = 0,
            Legislacion = 1,
            Programacion = 2,
            SPD = 3
        }

        #endregion
    }
}

