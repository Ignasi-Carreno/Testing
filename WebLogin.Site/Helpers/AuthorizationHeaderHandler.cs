using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebLogin.IBLL;
using WebLogin.Site.Resources.Constants;

namespace WebLogin.Site.Helpers
{
    /// <summary>   
    /// Authorization for web API.   
    /// </summary>   
    public class AuthorizationHeaderHandler : DelegatingHandler
    {
        /// <summary>   
        /// Send method.   
        /// </summary>   
        /// <param name="request">Request parameter</param>   
        /// <param name="cancellationToken">Cancellation token parameter</param>   
        /// <returns>Return HTTP response.</returns>   
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Initialization.
            IEnumerable<string> apiKeyHeaderValues = null;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            string userName = null;
            string password = null;
            
            // Verification.   
            if (request.Headers.TryGetValues(ApiInfo.API_KEY_HEADER, out apiKeyHeaderValues) && !string.IsNullOrEmpty(authorization.Parameter))
            {
                var apiKeyHeaderValue = apiKeyHeaderValues.First();
                
                // Get the auth token   
                string authToken = authorization.Parameter;
                
                // Decode the token from BASE64   
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                // Extract username and password from decoded token   
                userName = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

                //Reolve userModel object
                var dependencyScope = request.GetDependencyScope();
                var userModel = dependencyScope.GetService(typeof(IUserModel)) as IUserModel;

                // Verification.   
                if (apiKeyHeaderValue.Equals(ApiInfo.API_KEY_VALUE) && userModel.IsValidUser(userName, password))
                {
                    //Get user roles
                    var userRoles = userModel.GetUser(userName).Roles.Select(role => role.ToString());

                    // Set identity
                    var identity = new GenericIdentity(userName);
                    SetPrincipal(new GenericPrincipal(identity, userRoles.ToArray()));
                }
            }
            // Info.
            return base.SendAsync(request, cancellationToken);
        }

        /// <summary>   
        /// Set principal method.   
        /// </summary>   
        /// <param name="principal">Principal parameter</param>   
        private static void SetPrincipal(IPrincipal principal)
        {
            // setting.   
            Thread.CurrentPrincipal = principal;
            // Verification.   
            if (HttpContext.Current != null)
            {
                // Setting.   
                HttpContext.Current.User = principal;
            }
        }
    }
}