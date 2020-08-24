
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public interface  ICakeRepository
{
    // GraphQL Implement all methods required to complete the assignment
    Task<List<Cake>> GetCake();
}
