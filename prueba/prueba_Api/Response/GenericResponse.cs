namespace prueba_Api.Response
{
        /// <summary>
        /// codigo=0 es exitoso
        /// codigo=1 es error
        /// </summary>
        public class GenericResponse
        {

            public int Codigo { get; set; }
            public string Mensaje { get; set; }

            public object? Dato { get; set; }
            public GenericResponse(int codigo, string mensaje, object? dato)
            {

                Codigo = codigo;
                Mensaje = mensaje;
                Dato = dato;

            }
        }
}
