using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Application.Services.Image
{
    public interface IImageService
    {
        Task<string> SaveProductImageAsync(
            IFormFile file, 
            CancellationToken cancellationToken = default);
    }
}
