using EmptyAuth.Common.Enums.Claims;
using EmptyAuth.Core.Interfaces;
using EmptyAuth.Data;
using EmptyAuth.Models;
using EmptyAuth.Models.AuthModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmptyAuth.Core.Services
{
	public class PlantService: IPlantService
	{
		private readonly AppDbContext _context;
		public PlantService(AppDbContext context)
		{
			_context = context;
		}

		public async Task CreateAsync(AuthRequest<PlantDto> requestModel)
		{

			var hadAccessAction = await _context.PlantUserClaims.AnyAsync(x => 
					x.PlantId == requestModel.Model.Id && 
					x.UserId==requestModel.UserId &&
					x.ClaimValue==Plant.Create.ToString());


			//THROW IF DON'T HAVE PERMISSIONS
			if (!hadAccessAction)
				throw new HttpResponseException(HttpStatusCode.Forbidden);

			//HERE CREATING A PLANT
			return;
		}

		public async Task<IList<PlantDto>> GetAllAsync(AuthRequestBase requestModel)
		{
			//GET PLANTS WITH READ PERMISSIONS BY USERID
			var plantIds = requestModel.Permission.Plants.Where(x=>x.Claims.Any(x => x == Plant.Read.ToString())).Select(x => x.Id);
			var plants = await _context.Plants
				.Where(x => 
					plantIds.Contains(x.Id) && 
					x.PlantUserClaims.Any(pl => 
						plantIds.Contains(pl.PlantId) && 
						pl.UserId == requestModel.UserId && 
						pl.ClaimValue == Plant.Read.ToString()))
				.Select(x=> new PlantDto
				{
					Id = x.Id,
					Name = x.Name
				})
				.ToListAsync();

			return plants;
		}

		public async Task DeleteAsync(AuthRequest<PlantDto> requestModel)
		{

			var hadAccessAction = await _context.PlantUserClaims.AnyAsync(x =>
					x.PlantId == requestModel.Model.Id &&
					x.UserId == requestModel.UserId &&
					x.ClaimValue == Plant.Delete.ToString());

			//THROW IF DON'T HAVE PERMISSIONS
			if (!hadAccessAction)
				throw new HttpResponseException(HttpStatusCode.Forbidden);

			//HERE DELETING A PLANT
			return;
		}

		public async Task UpdateAsync(AuthRequest<PlantDto> requestModel)
		{

			var hadAccessAction = await _context.PlantUserClaims.AnyAsync(x =>
					x.PlantId == requestModel.Model.Id &&
					x.UserId == requestModel.UserId &&
					x.ClaimValue == Plant.Update.ToString());

			//THROW IF DON'T HAVE PERMISSIONS
			if (!hadAccessAction)
				throw new HttpResponseException(HttpStatusCode.Forbidden);

			//HERE UPDATING A PLANT
			return;
		}
	}
}
