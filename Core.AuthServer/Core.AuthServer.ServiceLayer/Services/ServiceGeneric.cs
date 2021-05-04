using Core.AuthServer.CoreLayer.Repositories;
using Core.AuthServer.CoreLayer.Services;
using Core.AuthServer.CoreLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.AuthServer.ServiceLayer.Services
{
    public class ServiceGeneric<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;

        public ServiceGeneric(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            await _repository.AddAsync(newEntity);

            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);

            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<TDto>>(await _repository.GetAllAsync());

            return Response<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return Response<TDto>.Fail("Id not found", 404, true);

            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(product), 200);
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var isExistEntity = await _repository.GetByIdAsync(id);

            if (isExistEntity == null)
                return Response<NoDataDto>.Fail("Id not found", 404, true);

            _repository.Remove(isExistEntity);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204); //204 No Content
        }

        public async Task<Response<NoDataDto>> Update(int id, TDto entity)
        {
            var isExistEntity = await _repository.GetByIdAsync(id);

            if (isExistEntity == null)
                return Response<NoDataDto>.Fail("Id not found", 404, true);

            var updateEntity = _repository.Update(ObjectMapper.Mapper.Map<TEntity>(entity));

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204); //204 No Content
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var products = _repository.Where(predicate);

            var tDto = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await products.ToListAsync());

            return Response<IEnumerable<TDto>>.Success(tDto, 200);
        }
    }
}
