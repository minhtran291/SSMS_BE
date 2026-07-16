using AutoMapper;
using SSMS.Domain;

namespace SSMS.Application.Services.Base
{
    public abstract class Service(IUnitOfWork unitOfWork, IMapper mapper)
    {
            protected readonly IUnitOfWork _unitOfWork = unitOfWork;
            protected readonly IMapper _mapper = mapper;
    }
}
