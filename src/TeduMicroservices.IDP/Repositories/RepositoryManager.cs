using AutoMapper;
using LearnMicroservice.IDP.Common.Domain;
using LearnMicroservice.IDP.Entities;
using LearnMicroservice.IDP.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace LearnMicroservice.IDP.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TeduIdentityContext _dbContext;
    private readonly IMapper _mapper;
    public RepositoryManager(TeduIdentityContext dbContext, IUnitOfWork unitOfWork, UserManager<User> userManager,
    RoleManager<User> roleManager, IMapper mapper)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        UserManager = userManager;
        RoleManager = roleManager;
        _mapper = mapper;
    }
    public UserManager<User> UserManager { get; }

    public RoleManager<User> RoleManager { get; }

    //public IPermissionRepository Permission => _permissionRepository.Value;

    public Task<int> SaveAsync()
        => _unitOfWork.CommitAsync();

    public Task<IDbContextTransaction> BeginTransactionAsync()
        => _dbContext.Database.BeginTransactionAsync();

    public Task EndTransactionAsync()
        => _dbContext.Database.CommitTransactionAsync();

    public void RollbackTransaction()
        => _dbContext.Database.RollbackTransactionAsync();
}
