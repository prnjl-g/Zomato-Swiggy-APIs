using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Graphql;

namespace graphql_create.Controllers
{
  [Route("graphql")]
  [ApiController]
  public class GraphqlController: ControllerBase 
  {
        private IHttpContextAccessor _httpContextAccessor;
        private IValidationRule _validationRule;
        private ISchema _newschema;

        public GraphqlController(
        IHttpContextAccessor httpContextAccessor,
        IValidationRule validationRule,
        ISchema schema) 
    {
      _httpContextAccessor = httpContextAccessor;
      _validationRule = validationRule;
      _newschema = schema;
    }
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] GraphQLQuery query) 
    {
      var schema = new MySchema();
      var inputs = query.Variables.ToInputs();


      // 2 way to use graphql - 
      // proper dotnet way, which gives you the authentication and authorization,
      // graphqL way.
      // 1. graphql way implemented in the service/database/graphql easy to implement - to run comment 
      // ValidationRules, _.Schema = _newschema; 
      // 2. .NET way, GraphQLModel folder - gives auhtentication and authorization implement - 
      // comment  _.Schema = schema.GraphQLSchema; and uncomment rest  
      var result = await new DocumentExecuter().ExecuteAsync(_ =>
      {
        _.Schema = schema.GraphQLSchema;
       // _.Schema = _newschema;
        _.Query = query.Query;
        _.OperationName = query.OperationName;
        _.Inputs = inputs;
        _.ExposeExceptions = true;
        //_.ValidationRules = new List<IValidationRule> { _validationRule };
        _.UserContext = _httpContextAccessor.HttpContext.User;
      });

        if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
      }
    }
}