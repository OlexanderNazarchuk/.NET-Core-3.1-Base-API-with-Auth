using EmptyAuth.Models;
using EmptyAuth.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyAuth.Core.Interfaces
{
	public interface IPlantService
	{
		Task CreateAsync(AuthRequest<PlantDto> requestModel);
		Task<IList<PlantDto>> GetAllAsync(AuthRequestBase requestModel);
		Task DeleteAsync(AuthRequest<PlantDto> requestModel);
		Task UpdateAsync(AuthRequest<PlantDto> requestModel);
	}
}
