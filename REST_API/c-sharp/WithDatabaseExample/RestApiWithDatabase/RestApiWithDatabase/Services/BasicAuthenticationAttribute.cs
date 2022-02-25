using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using RestApiWithDatabase.Repositories;
using System.Net.Http.Headers;
using System.Text;
using RestApiWithDatabase.Models;
using System.Security.Claims;

namespace RestApiWithDatabase.Services
{
    public class BasicAuthenticationAttribute : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IUserRepository repository;
        public BasicAuthenticationAttribute(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserRepository repository
            ):base(options,logger, encoder,clock)
        {
            this.repository = repository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //exclude anonymous endpoints from the authentication rule
            Endpoint endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            //Authorization: <auth scheme> : <auth string>
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header required!");

            AuthenticationHeaderValue authorizationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            //pulling out the username and password, recalled that the username and password are passed as base64(username:password)
            byte[] credentialBytes = Convert.FromBase64String(authorizationHeader.Parameter);
            string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

     

            string username = credentials[0];
            string password = credentials[1];

            User user = await this.repository.GetUserByUsernameAndPassword(username, password);
            if (user == null)
                return AuthenticateResult.Fail("Incorrect username or password!");


            //read more on claim
            //https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-6.0
            /**
             * A claims-based identity is the set of claims. A claim is a statement that an entity 
             * (a user or another application) makes about itself, it's just a claim. For example a 
             * claim list can have the user’s name, user’s e-mail, user’s age, user's authorization for an action
             */

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }

        /**
         * This method creates the auth challenge 
         * */
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
            return base.HandleChallengeAsync(properties);
        }
    }
}
