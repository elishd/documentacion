using AutoMapper;
using prueba_Api.Model;
using prueba_Api.Request;
using prueba_Api.Response;

namespace prueba_Api.Mapper
{
    public class webApiMapper: Profile
    {
        public webApiMapper()
        {
            // REQUEST
            CreateMap<CreateProductoRequest, producto>();
            //CreateMap<DeleteClienteRequest, Cliente>();
            //CreateMap<GetByIdClienteRequest, Cliente>();
            //CreateMap<UpdateClienteRequest, Cliente>();

            // RESPONSE
            CreateMap<producto, ProductoResponse>();
        }
    }
}
