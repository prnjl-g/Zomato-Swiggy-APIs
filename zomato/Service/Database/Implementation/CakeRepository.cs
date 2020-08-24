
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Database;

public class CakeRepository : ICakeRepository
{
    private StoreContext this_dataBaseContext;

    public CakeRepository(StoreContext context){
       this_dataBaseContext = context;
    }
    
    public async Task<List<Cake>> GetCake()
    {
        return await Task.FromResult(new List<Cake> { new Cake {Name = "Abc", Cost = 12}});
    }
}