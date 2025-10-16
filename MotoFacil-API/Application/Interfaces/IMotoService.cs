﻿using MotoFacilAPI.Application.Dtos;

namespace MotoFacilAPI.Application.Interfaces
{
    public interface IMotoService
    {
        Task<List<MotoDto>> ListAsync();
        Task<MotoDto?> GetByIdAsync(string id);
        Task<MotoDto> CreateAsync(MotoDto dto);
        Task<bool> UpdateAsync(string id, MotoDto dto);
        Task<bool> DeleteAsync(string id);
    }
}