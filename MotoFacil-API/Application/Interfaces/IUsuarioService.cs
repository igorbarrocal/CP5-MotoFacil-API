﻿using MotoFacilAPI.Application.Dtos;

namespace MotoFacilAPI.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> ListAsync();
        Task<UsuarioDto?> GetByIdAsync(string id);
        Task<UsuarioDto> CreateAsync(UsuarioDto dto);
        Task<bool> UpdateAsync(string id, UsuarioDto dto);
        Task<bool> DeleteAsync(string id);
    }
}